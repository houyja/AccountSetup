using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace UserLibrary.BLL
{
    class DataAccess
    {
        SqlConnection CON;
        SqlCommand CMD;

        public DataAccess(string con, string proc)
        {
            CON = new SqlConnection(con);
            CMD = new SqlCommand(proc, CON);
            CMD.CommandType = CommandType.StoredProcedure;
        }

        public void SetProc(string proc, CommandType cmdType)
        {
            CMD = new SqlCommand(proc, CON);
            CMD.CommandType = cmdType;
        }

        public void SetParamater_Input(string paramName, object value, SqlDbType type)
        {
            CMD.Parameters.Add(paramName, type, -1).Value = value;
        }

        public void SetParamater_Input(string paramName, object value, SqlDbType type, int size)
        {
            CMD.Parameters.Add(paramName, type, size).Value = value;
        }

        public void SetParamater_InputOutput(string paramName, object value, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(paramName, type, -1);
            param.Value = value;
            param.Direction = ParameterDirection.InputOutput;

            CMD.Parameters.Add(param);
        }

        public void SetParamater_InputOutput(string paramName, object value, SqlDbType type, int size)
        {
            SqlParameter param = new SqlParameter(paramName, type, size);
            param.Value = value;
            param.Direction = ParameterDirection.InputOutput;

            CMD.Parameters.Add(param);
        }

        public void SetParamater_Output(string paramName, SqlDbType type)
        {
            CMD.Parameters.Add(paramName, type, -1).Direction = ParameterDirection.Output;
        }

        public void SetParamater_Output(string paramName, SqlDbType type, int size)
        {
            CMD.Parameters.Add(paramName, type, size).Direction = ParameterDirection.Output;
        }

        public Boolean ExecuteNonQuery()
        {
            using (CON)
            {
                using (CMD)
                {
                    CMD.CommandType = CommandType.StoredProcedure;
                    CON.Open();
                    CMD.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public DataTable ExcuteQuery()
        {
            DataTable table = new DataTable();
            using (CON)
            {
                using (CMD)
                {
                    using (var da = new SqlDataAdapter(CMD))
                    {
                        CMD.CommandType = CommandType.StoredProcedure;
                        da.Fill(table);
                    }
                }
            }
            return table;
        }


        public List<object[]> GetParamaters()
        {
            List<object[]> results = new List<object[]>();

            List<SqlDbType> nullableTypes = new List<SqlDbType>();
            nullableTypes.Add(SqlDbType.Char);
            nullableTypes.Add(SqlDbType.NChar);
            nullableTypes.Add(SqlDbType.NText);
            nullableTypes.Add(SqlDbType.NVarChar);
            nullableTypes.Add(SqlDbType.Text);
            nullableTypes.Add(SqlDbType.VarChar);

            foreach (SqlParameter paramater in CMD.Parameters)
            {
                if (paramater.Direction == ParameterDirection.Output || paramater.Direction == ParameterDirection.InputOutput)
                {
                    results.Add(new object[] { paramater.ParameterName, CheckDBValue(paramater.Value, nullableTypes.Contains(paramater.SqlDbType)) });
                }
            }
            return results;
        }

        public object GetParamater(string name)
        {
            try
            {
                return CheckDBValue(CMD.Parameters[name].Value);
            }
            catch
            {
                return null;
            }
        }

        private object CheckDBValue(object obj, Boolean AllowEmptyString = true)
        {
            try
            {
                if (obj == DBNull.Value)
                {
                    return null;
                }
                if (obj == null)
                {
                    return null;
                }
                else if (AllowEmptyString == false && obj.ToString() == "")
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
