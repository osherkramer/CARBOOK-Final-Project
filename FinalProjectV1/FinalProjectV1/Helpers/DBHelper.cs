using System;
using System.Data;
using System.Data.SqlClient;


namespace FinalProjectV1.Helpers
{
    public class DBHelper
    {
        private string connectionString = @"Server=db.cs.colman.ac.il;Database=CarBook;User Id=carbook;password=Car@Book";
        private SqlConnection sqlConnection { get; set; }

        public DBHelper()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        ~DBHelper()
        {
            if(sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }
    }
}