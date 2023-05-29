using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.Common;

    namespace ST10104487_POE_2022.ModuleFolder
    {
        public class Module
        {
        public static SqlConnection getConnection()
        {
            string fileName = "ModuleDatabase.mdf";
            string filePath = Path.GetFullPath(fileName).Replace(@"\bin\Debug", @"\ModuleDatabase");
            string strConn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={filePath};Integrated Security=True";
            return new SqlConnection(strConn);
        }

        //SqlConnection conn = new SqlConnection(@"C:\USERS\RCAZURELABS\SOURCE\REPOS\ST10104487_POE_2022\ST10104487_POE_2022\DATABASE\MODULEDATABASE.MDF");
        SqlConnection conn = getConnection();

        public string Module_Code { get; set; }

            public string Module_Name { get; set; }

            public double NumCredits { get; set; }

            public double ClassHour { get; set; }

            public double NumWeeks { get; set; }

            public double SelfStudy { get; set; }


            public Module()
            {

            }

            public Module(string module_Code, string module_Name, double numCredits, double classHour, double numWeeks)
            {
                Module_Code = module_Code;
                Module_Name = module_Name;
                NumCredits = numCredits;
                ClassHour = classHour;
                NumWeeks = numWeeks;
                SelfStudy = CalcStudy();
            }

            public List<Module> allModule()
            {
                string strSelect = "SELECT * FROM tblModule";
                SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
                DataTable myTable = new DataTable();
                DataRow myRow;
                SqlDataAdapter myAdapter = new SqlDataAdapter();
                List<Module> mlist = new List<Module>();
                conn.Open();
                myAdapter.Fill(myTable);
                if (myTable.Rows.Count > 0)
                {
                    for (int i = 0; i < myTable.Rows.Count; i++)
                    {
                        myRow = myTable.Rows[i];
                        Module_Code = (string)myRow[1];
                        Module_Name = (string)myRow[2];
                        NumCredits = (double)myRow[3];
                        ClassHour = (double)myRow[4];
                        NumWeeks = (double)myRow[5];
                        SelfStudy = (double)myRow[6];

                        Module mo = new Module(Module_Code, Module_Name, NumCredits, ClassHour, NumWeeks);
                        mlist.Add(mo);
                    }
                }
                return mlist;
            }


            public double CalcStudy()
            {
                double study;
                study = ((NumCredits * 10) / NumWeeks) - ClassHour;
                return study;
            }

            public void AddData()
            {
            SqlCommand cmd = new SqlCommand($"INSERT INTO tblModule VALUES ('{Module_Code} + '{Module_Name}' + '{NumCredits}' + '{ClassHour} + '{NumWeeks}', '{SelfStudy}')",conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            public void delete(string moduleID)
            {
                string strDelete = $"DELETE FROM tblModule WHERE Module_Name = '{moduleID}'";
                SqlCommand cmdDelete = new SqlCommand(strDelete, conn);
                conn.Open();
                cmdDelete.ExecuteNonQuery();
                conn.Close();
            }

            public void update(string moduleID)
            {
                string strUpdate = $"UPDATE FROM tblModule SET Module_Name = '{Module_Name}', NumCredits = '{NumCredits}', ClassHour = '{ClassHour}',NumWeeks = '{NumWeeks}'";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, conn);
                conn.Open();
                cmdUpdate.ExecuteNonQuery();
                conn.Close();
            }

            public Module getModule(string moduleID)
            {
                string strSelect = $"SELECT * FROM tblUserRegister WHERE Module_Code = '{moduleID}'";
                SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
                conn.Open();

                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Module_Code = (string)reader[0];
                        Module_Name = (string)reader[1];
                        NumCredits = Convert.ToDouble(reader[2]);
                        ClassHour = Convert.ToDouble(reader[3]);
                        NumWeeks = Convert.ToDouble(reader[4]);
                        SelfStudy = Convert.ToDouble(reader[5]);
                    }
                    return new Module(Module_Code, Module_Name, NumCredits, ClassHour, NumWeeks);
                }
            }
        }
    }