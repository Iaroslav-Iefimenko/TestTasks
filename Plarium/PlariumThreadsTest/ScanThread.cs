using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PlariumThreadsTest
{
    class ScanThread : ThreadClass
    {
        readonly String m_dirPath;
        public event EventHandler<EventArgs> Finished;

        public ScanThread(String dirPath) 
        {
            m_dirPath = dirPath;
        }

        override protected void ThreadFunc()
        {
            NamedPipeClientStream pipeTree =
                    new NamedPipeClientStream(".", Settings.TreePipeName,
                        PipeDirection.Out, PipeOptions.None);
            NamedPipeClientStream pipeFile =
                    new NamedPipeClientStream(".", Settings.FilePipeName,
                        PipeDirection.Out, PipeOptions.None);
            try
            {
                pipeTree.Connect();
                pipeFile.Connect();
                BinaryFormatter binFormat = new BinaryFormatter();
                ScanDir(m_dirPath, pipeTree, pipeFile, binFormat);
            }
            finally
            {
                pipeTree.WaitForPipeDrain();
                pipeFile.WaitForPipeDrain();
                pipeTree.Close();
                pipeFile.Close();
            }

            if (Finished != null)
                Finished.Invoke(this, new EventArgs());
        }

        private void ScanDir(String dirPath, NamedPipeClientStream pipeTree,
            NamedPipeClientStream pipeFile, BinaryFormatter binFormat)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            Info i = CreateInfoFromFileSystemInfo(di);
            binFormat.Serialize(pipeFile, i);
            binFormat.Serialize(pipeTree, i);
            pipeFile.Flush();
            pipeTree.Flush();

            foreach (FileInfo fi in di.EnumerateFiles())
            {
                Info i2 = CreateInfoFromFileSystemInfo(fi);
                binFormat.Serialize(pipeFile, i2);
                binFormat.Serialize(pipeTree, i2);
                pipeFile.Flush();
                pipeTree.Flush();
            }

            foreach (DirectoryInfo d in di.EnumerateDirectories())
                ScanDir(d.FullName, pipeTree, pipeFile, binFormat);
        }

        private Info CreateInfoFromFileSystemInfo(FileSystemInfo fi)
        {
            Info i = new Info
            {
                Name = fi.Name,
                FullName = fi.FullName,
                IsDirectory = fi is DirectoryInfo,
                DateOfCreate = fi.CreationTime,
                DateOfModification = fi.LastWriteTime,
                DateOfLastAccess = fi.LastAccessTime,
                SizeInBytes = (fi is DirectoryInfo ? 0 : (fi as FileInfo).Length),
                IsSystem = (fi.Attributes & FileAttributes.System) == FileAttributes.System,
                IsReadOnly = (fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly,
                IsArchive = (fi.Attributes & FileAttributes.Archive) == FileAttributes.Archive,
                IsCompressed = (fi.Attributes & FileAttributes.Compressed) == FileAttributes.Compressed,
                IsHidden = (fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden,
                IsTemporary = (fi.Attributes & FileAttributes.Temporary) == FileAttributes.Temporary
            };

            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            WindowsPrincipal wp = new WindowsPrincipal(wi);
            AuthorizationRuleCollection arc = null;
            if (fi is DirectoryInfo)
            {
                DirectorySecurity ds = (fi as DirectoryInfo).GetAccessControl(AccessControlSections.Access);
                IdentityReference ir = ds.GetOwner(typeof(NTAccount));
                if (ir != null)
                    i.Owner = ir.Value;
                arc = ds.GetAccessRules(true, true, typeof (NTAccount));
            }
            else
            {
                FileSecurity fs = (fi as FileInfo).GetAccessControl(AccessControlSections.Access);
                IdentityReference ir = fs.GetOwner(typeof(NTAccount));
                if (ir != null)
                    i.Owner = ir.Value;
                arc = fs.GetAccessRules(true, true, typeof (NTAccount));
            }

            foreach (FileSystemAccessRule fsar in arc)
            {
                if (wi.Name == fsar.IdentityReference.Value || wp.IsInRole(fsar.IdentityReference.Value))
                {
                    if (!i.IsCurrentUserCanRead)
                        i.IsCurrentUserCanRead =
                            (fsar.FileSystemRights & FileSystemRights.Read) == FileSystemRights.Read;
                    if (!i.IsCurrentUserCanModify)
                        i.IsCurrentUserCanModify =
                            (fsar.FileSystemRights & FileSystemRights.Modify) == FileSystemRights.Modify;
                    if (!i.IsCurrentUserCanChangePermissions)
                        i.IsCurrentUserCanChangePermissions =
                            (fsar.FileSystemRights & FileSystemRights.ChangePermissions) == FileSystemRights.ChangePermissions;
                }
            }

            return i;
        }
    }
}
