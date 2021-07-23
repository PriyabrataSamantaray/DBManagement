using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace createdb
{
    public delegate void myPbarCB(int update);
    public delegate void myListCB(string list);
    public delegate void myStatusCB(string status);
    public delegate void myErrorBox(string err);
    public delegate void doneCB();


    public class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] myParams) //server, app, db, version
        {//test source control
            if (myParams.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {

                string devhome = System.Environment.GetEnvironmentVariable("devhome");
                string server = myParams[0];
                string app = myParams[1];
                string db = myParams[2];
                string xversion = myParams[3];
                string schemaFileName = myParams[4];
                string sPath = "\\" + app + "\\sql\\" + xversion + "\\";

                string[] FileList = Directory.GetFiles(devhome + sPath, schemaFileName);
                FileInfo fi = new FileInfo(FileList[0]);

                dbset passedParams = new dbset();

                passedParams.server = server;

                passedParams.database = db;
                passedParams.schema = sPath + fi.Name;
                passedParams.schemaPath = sPath;
                passedParams.silent = true;

                


                workerThread w = new workerThread();
                w.startWork(passedParams);

            }
        }





    }
}
