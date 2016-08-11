using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TechRadar.DataLayer.Interfaces;

namespace TechRadar.DataLayer.Repositories
{
    public class DBContext : IDBContext
    {
        private string _connectionString; //=  ConfigurationManager.ConnectionStrings[DBConstants.ConnectionStringName].ConnectionString;
        private IDbConnection _connection;
        
        public DBContext(string connectionStringName)
        {
            _connectionString = connectionStringName;
        }
        public IDbConnection DB
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
