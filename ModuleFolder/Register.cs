using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ST10104487_POE_2022.ModuleFolder
{
    public class Register
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

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public Register()
        {

        }

        public Register(string username, string password, string conpassword)
        {

            Username = username;
            Password = password;
            ConfirmPassword = conpassword;
        }

        public List<Register> GetRegister()
        {
            string strSelect = "SELECT * FROM tblUserRegister";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
            DataTable myTable = new DataTable();
            DataRow myRow;
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            List<Register> registerlist = new List<Register>();
            conn.Open();
            myAdapter.Fill(myTable);
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    Username = (string)myRow[1];
                    Password = (string)myRow[2];
                    ConfirmPassword = (string)myRow[3];

                    Register reg = new Register(Username, Password, ConfirmPassword);
                    registerlist.Add(reg);
                }
            }
            return registerlist;
        }


        public void AddRegisterData()
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO tblUserRegister VALUES ('{Username}' +'{Password}' + '{ConfirmPassword})", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public Register getRegister(string username)
        {
            string strSelect = $"SELECT * FROM tblUserRegister WHERE Username = '{username}";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
            conn.Open();

            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    Username = (string)reader[0];
                    Password = (string)reader[1];
                    ConfirmPassword = (string)reader[2];

                }
                return new Register(Username, Password, ConfirmPassword);
            }
        }


    }
}