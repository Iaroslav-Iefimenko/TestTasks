using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace PlariumThreadsTest
{
    class SaveToFileThread : ThreadClass
    {
        readonly String m_fileName;
        XmlElement m_lastDirElem;
        public event EventHandler<EventArgs> Finished;

        public SaveToFileThread(String fileName) 
        {
            m_fileName = fileName;
        }

        override protected void ThreadFunc()
        {
            NamedPipeServerStream pipeServer =
                new NamedPipeServerStream(Settings.FilePipeName, PipeDirection.In);
            pipeServer.WaitForConnection();
            BinaryFormatter binFormat = new BinaryFormatter();
            XmlDocument doc = new XmlDocument();

            while (pipeServer.IsConnected)
            {
                Info fi = null;
                try
                {
                    fi = (Info)binFormat.Deserialize(pipeServer);
                }
                catch (SerializationException) 
                {
                    //гасим ошибку десериализации последнего элемента из-за ошибки позиционирования в пайпе
                    //гашение исключения принято из-за невозможности установить позицию в пайпе (Position = 0)
                    //сама десериализация проходит успешно
                    //в реальном продукте возможно использование более сложной схемы получения данных до десериализации
                }

                if (fi == null)
                    continue;

                if (doc.ChildNodes.Count == 0)
                {
                    XmlElement elem = doc.CreateElement("dir");
                    FillXmlElementFromInfo(elem, fi);
                    doc.AppendChild(elem);
                    m_lastDirElem = elem;
                    continue;
                }

                if (!fi.IsDirectory)
                {
                    XmlElement elem = doc.CreateElement("file");
                    FillXmlElementFromInfo(elem, fi);
                    m_lastDirElem.AppendChild(elem);
                    continue;
                }

                List<String> list = new List<String>(fi.FullName.Split(Path.DirectorySeparatorChar));
                String parentDirName = list[list.Count - 2];

                while (m_lastDirElem.GetAttribute("Name") != parentDirName)
                {
                    m_lastDirElem = (XmlElement) m_lastDirElem.ParentNode;
                    if (m_lastDirElem == null)
                        break;
                }

                if (m_lastDirElem == null)
                    m_lastDirElem = doc.DocumentElement;

                XmlElement el = doc.CreateElement("dir");
                FillXmlElementFromInfo(el, fi);
                m_lastDirElem.AppendChild(el);
                m_lastDirElem = el;    
            }

            pipeServer.Close();
            doc.Save(m_fileName);

            if (Finished != null)
                Finished.Invoke(this, new EventArgs());
        }

        private void FillXmlElementFromInfo(XmlElement el, Info fi)
        {
            el.SetAttribute("Name", fi.Name);
            el.SetAttribute("FullName", fi.FullName);
            el.SetAttribute("IsDirectory", fi.IsDirectory.ToString());
            el.SetAttribute("DateOfCreate", fi.DateOfCreate.ToString(CultureInfo.InvariantCulture));
            el.SetAttribute("DateOfModification", fi.DateOfModification.ToString(CultureInfo.InvariantCulture));
            el.SetAttribute("DateOfLastAccess", fi.DateOfLastAccess.ToString(CultureInfo.InvariantCulture));
            el.SetAttribute("SizeInBytes", fi.SizeInBytes.ToString(CultureInfo.InvariantCulture));

            el.SetAttribute("IsSystem", fi.IsSystem.ToString());
            el.SetAttribute("IsReadOnly", fi.IsReadOnly.ToString());
            el.SetAttribute("IsArchive", fi.IsArchive.ToString());
            el.SetAttribute("IsCompressed", fi.IsCompressed.ToString());
            el.SetAttribute("IsHidden", fi.IsHidden.ToString());
            el.SetAttribute("IsTemporary", fi.IsTemporary.ToString());

            el.SetAttribute("Owner", fi.Owner);
            el.SetAttribute("IsCurrentUserCanRead", fi.IsCurrentUserCanRead.ToString());
            el.SetAttribute("IsCurrentUserCanModify", fi.IsCurrentUserCanModify.ToString());
            el.SetAttribute("IsCurrentUserCanChangePermissions", fi.IsCurrentUserCanChangePermissions.ToString());
        }
    }
}
