using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Microsoft.Adapter.SAP;


namespace createdb
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        //    SAPConnection con = new SAPConnection("ASHOST=sgqasd02;SYSNR=2;CLIENT=102;USER=ssis_rfc;LANG=en;passwd=bicomm");

        //    con.Open();
        //    SAPCommand cmd = new SAPCommand(con);
        //    //cmd.CommandText = "EXEC Z_RFC_EQUIPMENT_MASTER_DATA @I_EQUNR=@param";
        //    //SAPParameter param = new SAPParameter("@param", ParameterDirection.Input);
        //    //param.Value = "VIISTA80-137220";
        //    cmd.CommandText = "EXEC Z_RFC_EQUIPMENT_MASTER_DATA @I_EQUNR=@param";

        //    SAPParameter param = new SAPParameter("@param", ParameterDirection.Input);

        //    DataTable dt = new DataTable();
        //    //dt.Columns.Add("SIGN");
        //    //dt.Columns.Add("OPTION");
        //    //dt.Columns.Add("LOW");
        //    //dt.Columns.Add("HIGH");
        //    param.Value = "VIISTA80-137220";

        //    cmd.Parameters.Add(param);



        //    SAPDataReader dr = cmd.ExecuteReader();
        //       do
        //         {
        //          Console.WriteLine("value of returned datareader: " +  dr.GetSchemaTable().TableName);
        //            while (dr.Read())
        //              {
        //                string line = "";
        //                for (int i = 0; i < dr.FieldCount; i++)
        //                line = line + "| " + dr.GetValue(i).ToString();
        //                Console.WriteLine(line);
        //               }
        //           }
        //       while (dr.NextResult());
        //    con.Close();
        }
    }
}
