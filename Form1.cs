using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace createdb
{


        public partial class Form1 : Form
    {
        private workerThread worker;
        private Thread myThread;
        private dbset myparams;
        private string devhome;

        private struct Project
        {
            public Project(string NickName, string SQLFolder, string DatabaseName)
            {
                this.NickName = NickName;
                this.SQLRootFolder = SQLFolder;
                this.DatabaseName = DatabaseName;
                this.Versions = new List<Version>();
            }

            public override string ToString()
            {
                return NickName;
            }

            public string NickName;
            public string SQLRootFolder;
            public string DatabaseName;
            public List<Version> Versions;
        }

        private struct Server
        {
            public Server(string NickName, string ServerName, bool IsPro)
            {
                this.NickName = NickName;
                this.ServerName = ServerName;
                this.IsPro = IsPro;
            }

            public string NickName;
            public string ServerName;
            public bool IsPro;

            public override string ToString(){
                return NickName;
            }
            
        }

        private struct Version
        {
            public string Number;
            public string SourceFolder;

            public override string ToString()
            {
                return String.Format("{0:0.0}", Number);
            }
        }

        private List<Project> Projects;
        private List<Server> Servers;

        public Form1()
        {
            InitializeComponent();
            worker = new workerThread();

            myPbarCB pbarDelegate = new myPbarCB(setPBar);
            myListCB listDelegate = new myListCB(setList);
            myStatusCB statusDelegate = new myStatusCB(setStatus);
            doneCB endThread = new doneCB(doneThread);
            myErrorBox errBoxDelegate = new myErrorBox(setError);

            worker.pbar = pbarDelegate;
            worker.mylist = listDelegate;
            worker.mystatus = statusDelegate;
            worker.myDoneCB = endThread;
            worker.myErrorCB = errBoxDelegate;

            // Load project and server data from the config file
            LoadProjects();

            // Configure the Server combo
            cboServers.DisplayMember = "NickName";
            cboServers.ValueMember = "ServerName";
            cboServers.DataSource = Servers; 
            cboServers.SelectedIndex = 0;

            // Configure the Project combo
            cboProjects.DisplayMember = "NickName";
            cboProjects.ValueMember = "NickName";
            cboProjects.DataSource = Projects;
            cboProjects.SelectedIndex = 0;
            RefreshVersionCombo();

            myparams = new dbset();

            devhome = System.Environment.GetEnvironmentVariable("devhome");
        }

        private void dbChanged(object sender, EventArgs e)
        {
            RefreshVersionCombo();
            listBox1.Items.Clear();
            cmdUpdate.Enabled = true;
            progressBar1.Value = 0;
            status.Text = "";
        }

        private void RefreshVersionCombo()
        {
            // Refresh the version combo;
            if (cboProjects.SelectedItem != null)
            {
                Project P = (Project)cboProjects.SelectedItem;
                cboVersion.DataSource = new BindingSource(P.Versions, null).DataSource;
                cboVersion.SelectedIndex = cboVersion.Items.Count - 1;
            }
        }

        private void getParams()
        {
            // Store user-selected values from the combos
            Project P = (Project)cboProjects.SelectedItem;
            Version V = (Version)cboVersion.SelectedItem;

            string Msg = "";

            // Set server and database parameters
            myparams.server = ((Server)cboServers.SelectedItem).ServerName;
            myparams.database = P.DatabaseName;
            myparams.silent = false;

            // Get schema folder and schema file name
            string SchemaFolder = P.SQLRootFolder + V.SourceFolder;
            string[] FileList = Directory.GetFiles(devhome + SchemaFolder, "*.schema");

            // Set schema parameter
            myparams.schemaPath = SchemaFolder;

            // Ensure that the folder contains a single .schema file
            switch (FileList.Length)
            {
                case 0:
                    Msg = "The schema file could not be found!";
                    throw new Exception(Msg);
                case 1:
                    string SchemaFilePath = FileList[0];

                    if (!File.Exists(SchemaFilePath))
                    {
                        Msg = "The schema file could not be found!";
                        throw new Exception(Msg);
                    }
                    else
                    {
                        // Set schema parameter
                        FileInfo fi = new FileInfo(SchemaFilePath);
                        myparams.schema = SchemaFolder + fi.Name;
                    }
                    break;
                default:
                    Msg = "Multiple schema files exist in the specified location!";
                    throw new Exception(Msg);
            }
        }

        public void listText(string txt)
        {
            listBox1.Items.Add(txt);
            listBox1.Refresh();
            listBox1.SelectedIndex += 1;
        }

        public void errorText(string err)
        {
            MessageBox.Show(err);
        }

        public void updateStatus(string txt)
        {
            status.Text = txt;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            bool IsPro = ((Server)cboServers.SelectedItem).IsPro;
            string DB = ((Project)cboProjects.SelectedItem).DatabaseName;
            byte level = 0;
            string Msg = "";
            MessageBoxIcon icon = MessageBoxIcon.Question;

            // Warn user 3 times if updating a production server
            if (IsPro)
            {
                while (level < 3)
                {
                    switch (level)
                    {
                        case 0:
                            Msg = "Are you sure you want to update '" + DB + "' Production?";
                            icon = MessageBoxIcon.Question;
                            break;
                        case 1:
                            Msg = "No kidding man!!!  You are about to update " + DB.ToUpper() + " Pro!!!";
                            icon = MessageBoxIcon.Warning;
                            break;

                        case 2:
                            Msg = "Last chance amigo!  Are you sure?";
                            icon = MessageBoxIcon.Exclamation;
                            break;
                    }

                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show(Msg, "Production Update", MessageBoxButtons.OKCancel, icon);

                    if (dr == DialogResult.Cancel)
                        return;

                    level++;
                }
            }

            cmdUpdate.Enabled = false;

            getParams();

            myThread = new Thread(new ParameterizedThreadStart(worker.startWork));

            myThread.Name = "databaseThread";

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            myThread.Start(myparams);
            Thread.Sleep(0);
        }

        public void updatePbar(int update)
        {
            if (update > 1)
                progressBar1.Maximum = update;
            else
                progressBar1.Value += 1;
        }

        public void endState()
        {
            //worker.Abort();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void doneThread()
        {
            doneCB b = new doneCB(endState);
            Invoke(b);
        }

        public void setPBar(int update)
        {
            if (progressBar1.InvokeRequired)
            {
                myPbarCB b = new myPbarCB(updatePbar);
                Invoke(b, update);
            }
            else
                updatePbar(update);
        }

        private void setList(string txt)
        {
            if (listBox1.InvokeRequired)
            {
                myListCB b = new myListCB(listText);
                Invoke(b, txt);
            }
            else
                listText(txt);
        }

        private void setError(string txt)
        {
            if (listBox1.InvokeRequired)
            {
                myListCB b = new myListCB(errorText);
                Invoke(b, txt);
            }
            else
                errorText(txt);
        }

        private void setStatus(string txt)
        {
            if (status.InvokeRequired)
            {
                myStatusCB b = new myStatusCB(updateStatus);
                Invoke(b, txt);
            }
            else
                updateStatus(txt);
        }

        // Read Project and Server info from XML config file
        private void LoadProjects()
        {
            Servers = new List<Server>();
            Projects = new List<Project>();

            string strPath = Application.StartupPath + @"/Config.xml";
            XElement ConfigFile;

            // Ensure that the config file can be found
            if (!File.Exists(strPath))
            {
                IOException ex = new IOException("Can't locate the CreateDB configuration file!");
                throw ex;
            }
            else
            {
                // Load the configuration file
                ConfigFile = XElement.Load(strPath);
            }

            XElement doc = XElement.Parse(ConfigFile.ToString());

            // Iterate the servers in the XML tree, populating the list
            IEnumerable<XElement> XlServer =
                                            from el in doc.Elements("Servers").Elements("Server")
                                            select el;
            foreach (var ServerNode in XlServer)
            {
                string ServerName = ServerNode.Attribute("Name").Value;
                string NickName = ServerNode.Attribute("NickName").Value;
                bool IsPro = Boolean.Parse(ServerNode.Attribute("IsPro").Value);

                Servers.Add(new Server(NickName, ServerName, IsPro));
            }

            // Iterate the projects in the XML tree, populating the project list
            IEnumerable<XElement> XlProject =
                                            from el in doc.Elements("Projects").Elements("Project")
                                            select el;
            foreach (var ProjectNode in XlProject)
            {
                string NickName = ProjectNode.Attribute("NickName").Value;
                string SQLRootFolder = ProjectNode.Attribute("SQLRootFolder").Value;
                string DatabaseName = ProjectNode.Attribute("DatabaseName").Value;

                Project proj = new Project(NickName, SQLRootFolder, DatabaseName);

                // Iterate the versions of the current project, populating the version list
                IEnumerable<XElement> XlVersion =
                                                from el in ProjectNode.Elements("Version")
                                                select el;
                foreach (var VersionNode in XlVersion)
                {
                    Version v = new Version();
                    v.Number = (VersionNode.Attribute("Number").Value).ToString();
                    v.SourceFolder = VersionNode.Attribute("SrcFolder").Value;
                    proj.Versions.Add(v);
                }

                // Add the current project to the project list
                Projects.Add(proj);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}