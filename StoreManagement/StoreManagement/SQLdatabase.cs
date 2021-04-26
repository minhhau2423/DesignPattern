using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement
{
    class SQLdatabase:DBfactory
    {
        //Singleton Pattern
        private SQLdatabase() { }
        private static SQLdatabase SQL;
        public static SQLdatabase getInstanceSQL()
        {
            if (SQL == null)
            {
                SQL = new SQLdatabase();
            }
            return SQL;
        }
        //
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbCommand CreateCommand(string cmdText)
        {
            throw new NotImplementedException();
        }

        public override DbCommand CreateCommand(string cmdText, DbConnection cn)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = cmdText;
            command.Connection = (SqlConnection)cn;
            command.CommandType = CommandType.Text;
            return command;
        }

        public override DbConnection CreateConnection()
        {
            string strconn = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            return new SqlConnection(strconn);
        }

        public override DbConnection CreateConnection(string cnString)
        {
            return new SqlConnection(cnString);
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            throw new NotImplementedException();
        }

        public override DbDataAdapter CreateDataAdapter(DbCommand selectCmd)
        {
            throw new NotImplementedException();
        }

        public override DbDataReader CreateDataReader(DbCommand dbCmd)
        {
            throw new NotImplementedException();
        }

        public override DbParameter CreateParameter(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
