using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PlariumThreadsTest
{
    public partial class MainForm : Form
    {
        private TreeNode m_lastDirNode;
        private readonly Action<Info> m_addInfoToTreeDelegate;
        private readonly Action m_finishScanDelegate;
        private Int32 m_finishedCount;
        
        public MainForm()
        {
            InitializeComponent();
            m_finishedCount = 0;
            m_addInfoToTreeDelegate += AddInfoToTree;
            m_finishScanDelegate += FinishScan;
        }

        private void btnDirSelect_Click(object sender, EventArgs e)
        {
            if (fbdDir.ShowDialog() == DialogResult.OK)
                tbDir.Text = fbdDir.SelectedPath;
        }

        private void btnResFileSave_Click(object sender, EventArgs e)
        {
            if (sfdResFile.ShowDialog() == DialogResult.OK)
                tbResFile.Text = sfdResFile.FileName;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbDir.Text)) 
            {
                MessageBox.Show("Select directory for scan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(tbResFile.Text))
            {
                MessageBox.Show("Enter file name for scan results saving", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tvRes.BeginUpdate();
            tvRes.Nodes.Clear();
            tvRes.EndUpdate();

            SaveToFileThread fileT = new SaveToFileThread(tbResFile.Text);
            SaveToTreeThread treeT = new SaveToTreeThread();
            ScanThread scanT = new ScanThread(tbDir.Text);

            treeT.FileScanned += OnFileScanned;
            fileT.Finished += OnFinished;
            treeT.Finished += OnFinished;
            scanT.Finished += OnFinished;
            
            btnScan.Enabled = false;
            btnDirSelect.Enabled = false;
            btnResFileSave.Enabled = false;
            m_finishedCount = 0;

            fileT.Start();
            treeT.Start();
            scanT.Start();
        }

        public void OnFileScanned(object sender, InfoEventArgs e)
        {
            tvRes.Invoke(m_addInfoToTreeDelegate, e.ItemInfo);
        }

        private void AddInfoToTree(Info fi)
        {
            try
            {
                tvRes.BeginUpdate();
                if (tvRes.Nodes.Count == 0)
                {
                    TreeNode node = new TreeNode {Text = fi.Name};
                    tvRes.Nodes.Add(node);
                    m_lastDirNode = node;
                    node.Tag = fi;
                    return;
                }

                if (!fi.IsDirectory)
                {
                    TreeNode node = new TreeNode {Text = fi.Name};
                    m_lastDirNode.Nodes.Add(node);
                    node.Tag = fi;
                    node.Parent.Expand();
                    return;
                }

                List<String> list = new List<String>(fi.FullName.Split(Path.DirectorySeparatorChar));
                String parentDirName = list[list.Count - 2];

                while (m_lastDirNode.Text != parentDirName)
                    m_lastDirNode = m_lastDirNode.Parent;

                TreeNode nodeD = new TreeNode {Text = fi.Name};
                m_lastDirNode.Nodes.Add(nodeD);
                nodeD.Tag = fi;
                nodeD.Parent.Expand();
                m_lastDirNode = nodeD;
            }
            finally
            {
                tvRes.EndUpdate();
            }
        }

        public void OnFinished(object sender, EventArgs e)
        {
            Invoke(m_finishScanDelegate);
        }

        private void FinishScan()
        {
            lock (this)
            {
                m_finishedCount++;
                if (m_finishedCount == 3)
                {
                    btnScan.Enabled = true;
                    btnDirSelect.Enabled = true;
                    btnResFileSave.Enabled = true;
                    MessageBox.Show("Scaning of directory is finished", "Attention", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tvRes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                Info i = (Info)e.Node.Tag;
                tbName.Text = i.Name;
                tbFullName.Text = i.FullName;
                tbDateOfCreate.Text = i.DateOfCreate.ToString(CultureInfo.InvariantCulture);
                tbDateOfModification.Text = i.DateOfModification.ToString(CultureInfo.InvariantCulture);
                tbDateOfLastAccess.Text = i.DateOfLastAccess.ToString(CultureInfo.InvariantCulture);
                tbSizeInBytes.Text = i.SizeInBytes.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                tbName.Text = "";
                tbFullName.Text = "";
                tbDateOfCreate.Text = "";
                tbDateOfModification.Text = "";
                tbDateOfLastAccess.Text = "";
                tbSizeInBytes.Text = "";
            }
        }
    }
}
