using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ST10104487_ModuleApp
{
    public class Module
    {
        SqlConnection conn = Connection.getConnection();

        public string Module_Code { get; set; }

        public string Module_Name { get; set; }

        public double NumCredits { get; set; }

        public double ClassHour { get; set; }

        public double NumWeeks { get; set; }


        public Module(string module_Code, string module_Name, double numCredits, double classHour, double numWeeks)
        {
            Module_Code = module_Code;
            Module_Name = module_Name;
            NumCredits = numCredits;
            ClassHour = classHour;
            NumWeeks = numWeeks;


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
            if(myTable.Rows.Count >0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    Module_Code = (string)myRow[1];
                    Module_Name = (string)myRow[2];
                    NumCredits = (double)myRow[3];
                    ClassHour = (double)myRow[4];
                    NumWeeks = (double)myRow[5];

                    Module mo = new Module(Module_Code, Module_Name, NumCredits, ClassHour, NumWeeks);
                    mlist.Add(mo);
                }
            }      
            return mlist;
        }

        public string getModuleDetails()
        {
            double self_study_per_week;
            self_study_per_week = ((NumCredits * 10) / NumWeeks) - ClassHour;
            return "Module Code : "+ Module_Code +"\n"+
                "Module Name : " + Module_Name + "\n" +
                "Number of credits : " + NumCredits + "\n" + 
                "Class hours per week : " + ClassHour + "\n" +
                "Number of weeks : " + NumWeeks +
                 "\n"+ "Self Study Per Week = " + self_study_per_week + " hours needed for self studying per week" + "\n";
        } 

        public void AddData()
        { 
            string insertLine = "INSERT INTO tblModule(Module_Code, Module_Name, NumCredits, ClassHour, NumWeeks) " +
                "VALUES ('" +Module_Code+"','"+ Module_Name+"','"+NumCredits+"','"+ClassHour+"','"+NumWeeks+"')";
            SqlCommand cmd = new SqlCommand(insertLine,conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void delete()
        {
            string strDelete = $"DELETE FROM tblModule WHERE Module_Code = '{Module_Code}'";
            SqlCommand cmdDelete = new SqlCommand(strDelete, conn);
            conn.Open();
            cmdDelete.ExecuteNonQuery();
            conn.Close();
        }

        public Module getModule(string moduleID)
        {
            string strSelect = $"SELECT * FROM tblModule WHERE Module_Code = '{moduleID}'";
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
                } 
                return new Module(Module_Code, Module_Name, NumCredits, ClassHour, NumWeeks);
            }
        }

        public bool verifyLoginDetails(string username, string password)
        {
            int count = 0;
            string strSelect = $"SELECT Username, Password FROM tblLoginDetails WHERE Username = '{username}' +" +
                $"AND Password = '{password}'";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);

            using (conn)
            {
                conn.Open();
                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }
            return count == 1;
        }

    }
}
