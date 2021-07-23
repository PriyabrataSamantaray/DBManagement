using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;


namespace createdb
{
    class workerThread
    {
        private string schema;
        private string path;
        private string constr;
        private string constrQ;
        private string devhome;
        public myPbarCB pbar;  //progress bars delegate
        public myStatusCB mystatus;  //status box delegate
        public myListCB mylist; //list box delegate
        public doneCB myDoneCB;  //all done delegate
        public myErrorBox myErrorCB; //alert errors
        private bool windowsApp = true;
        private bool hadError = false;

        public workerThread()
        {
            devhome = System.Environment.GetEnvironmentVariable("devhome");


        }
        public void startWork(object param)
        {
            dbset passedParams = (dbset)param;
            constr = "-S " + passedParams.server + " -E -d " + passedParams.database + " -m1 -i ";
            constrQ ="-S " + passedParams.server + " -E -d " + passedParams.database + " -m1 -Q ";

            if (passedParams.server == "DCA-QA-247" || passedParams.server == "DCA-QA-86" || passedParams.server == "PRMPRDLSNR")
                constr = "-S " + passedParams.server + " -U PRMUSER -P 2#prmlogin! -d " + passedParams.database + " -m1 -i ";
            else
                constr = "-S " + passedParams.server + " -E -d " + passedParams.database + " -m1 -i ";

            string line;

            this.schema = devhome + passedParams.schema;
            this.path = devhome + passedParams.schemaPath;

            if (passedParams.silent)
                windowsApp = false;

            if(windowsApp)
                pbar(sqlCount());  //initialize progress bar



            using (StreamReader sr = new StreamReader(schema))
            {
                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine();
                    if (line.Length > 1)
                    {
                        if (line.Substring(0, 1) != "#")
                        {
                            execute(line);
                            if (windowsApp)
                            {
                                pbar(1);
                                mylist(line);
                            }
                        }
                    }
                }
            }
            if (windowsApp)
            {
                mystatus("Done");
                myDoneCB();
            }
            else
            {
                Console.WriteLine("Done!");

                if (hadError == false)
                {
                    string CMDPath = Convert.ToString(ConfigurationManager.AppSettings["SQLCMDPath"]);
                    Process proc = new Process();
                    proc.StartInfo.FileName = CMDPath;
                    proc.StartInfo.Arguments = constrQ + "\"insert into buildscript (scriptName, errorString, tstamp) values('" + passedParams.database + "', 'clean run', getdate())\"";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();

                    proc.Close();
                }
            }

        }
        private void execute(string line)
        {
            string error;
            Process proc = new Process();
            string CMDPath = Convert.ToString(ConfigurationManager.AppSettings["SQLCMDPath"]);
            proc.StartInfo.FileName = CMDPath;
            proc.StartInfo.Arguments = constr + path + line;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
           
            proc.WaitForExit();
            error = proc.StandardOutput.ReadToEnd();

            if (proc.ExitCode == 1)
            {
                if(windowsApp)
                    myErrorCB("Problem with file " + line);
                else
                {
                    proc.StartInfo.Arguments = constrQ + "\"insert into buildscript (scriptName, errorString, tstamp) values('" + line + "', 'bad schema file', getdate())\"";
                    proc.Start();

                }
                hadError = true;

            }
            if (error != "")
            {
                if(windowsApp)
                    myErrorCB(line + "\n" + error);
                else
                {
                    proc.StartInfo.Arguments = constrQ + "\"insert into buildscript (scriptName, errorString, tstamp) values('" + line + "', 'bad SQL', getdate())\"";
                    proc.Start();
                }
                hadError = true;

            }
            proc.Close();

        }
        private int sqlCount()
        {
            int count = 0;
            string line;
            using (StreamReader sr = new StreamReader(schema))
            {
                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine();
                    if (line.Length > 1)
                    {
                        if (line.Substring(0, 1) != "#")
                            count++;
                    }
                }
            }
            return count;
        }


    }
    public class dbset
    {
        public string server;
        public string database;
        public string schema;
        public string schemaPath;
        public bool silent;
    }
}
