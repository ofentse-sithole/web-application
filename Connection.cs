using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ST10104487_ModuleApp
{
    public static class Connection
    {
        public static SqlConnection getConnection()
        {
            string fileName = "ModuleDatabase.mdf";
            string filePath = Path.GetFullPath(fileName).Replace(@"\bin\Debug\Data");
            string strConn = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=True";
            return new SqlConnection(filePath); 
        }

    }
}
