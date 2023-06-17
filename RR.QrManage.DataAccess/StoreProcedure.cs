using RR.QrManage.Domain.Models;
using System.Data;
using System.Data.SqlClient;

namespace RR.QrManage.DataAccess
{
    public class Parameters
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Parameters(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
    public class StoreProcedure
    {
        private readonly List<Parameters> ParameterList = new();//Se agrego readonly
        public string Name { get; set; }



        public StoreProcedure(string name) => Name = name;



        public void AddParameter(string name, object value)
        {
            ParameterList.Add(new Parameters(name, value));
        }

        public Response<StoreProcedureSelect> Select(string connectionString, int timeOut)
        {
            try
            {
                using SqlConnection sqlConnection = new(connectionString);
                StoreProcedureSelect procedureInsertResult = new();
                SqlDataAdapter sqlDataAdapter = new(Name, sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandTimeout = timeOut;
                SqlParameter resultParameter = new("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(resultParameter);
                SqlParameter messageParameter = new("@Message", SqlDbType.NVarChar, 150)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(messageParameter);
                foreach (var param in ParameterList)
                {
                    if (param.Value == null)
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, DBNull.Value);
                    else
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, param.Value);
                }
                sqlConnection.Open();
                sqlDataAdapter.Fill(procedureInsertResult.DataTable);
                sqlConnection.Dispose();
                sqlConnection.Close();
                procedureInsertResult.Result = Convert.ToInt32(resultParameter.Value);
                procedureInsertResult.Message = messageParameter.Value.ToString()!;
                return Response<StoreProcedureSelect>.Success(procedureInsertResult);
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
                return Response<StoreProcedureSelect>.Error(ex.Message);
            }
        }

        public Response<StoreProcedureInsert> Insert(string connectionString, int timeOut)
        {
            try
            {
                using SqlConnection sqlConnection = new(connectionString);
                StoreProcedureInsert procedureInsertResult = new();
                SqlDataAdapter sqlDataAdapter = new(Name, sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandTimeout = timeOut;



                SqlParameter resultParameter = new("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(resultParameter);



                SqlParameter messageParameter = new("@Message", SqlDbType.NVarChar, 150)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(messageParameter);



                SqlParameter identityParameter = new("@Identity", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(identityParameter);
                foreach (var param in ParameterList)
                {
                    if (param.Value == null)
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, DBNull.Value);
                    else
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, param.Value);
                }
                sqlConnection.Open();
                sqlDataAdapter.Fill(procedureInsertResult.DataTable);
                sqlConnection.Dispose();
                sqlConnection.Close();
                procedureInsertResult.Result = Convert.ToInt32(resultParameter.Value);
                procedureInsertResult.Message = messageParameter.Value.ToString()!;
                procedureInsertResult.Identity = Convert.ToInt32(identityParameter.Value);
                return Response<StoreProcedureInsert>.Success(procedureInsertResult);
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
                return Response<StoreProcedureInsert>.Error(ex.Message);
            }
        }

        public int Update(string connectionString, int timeOut)
        {
            try
            {
                using SqlConnection sqlConnection = new(connectionString);
                DataTable dataTable = new();
                SqlDataAdapter sqlDataAdapter = new(Name, sqlConnection);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandTimeout = timeOut;



                SqlParameter resultParameter = new("@Result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(resultParameter);



                SqlParameter messageParameter = new("@Message", SqlDbType.NVarChar, 150)
                {
                    Direction = ParameterDirection.Output
                };
                sqlDataAdapter.SelectCommand.Parameters.Add(messageParameter);



                foreach (var param in ParameterList)
                {
                    if (param.Value == null)
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, DBNull.Value);
                    else
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue(param.Name, param.Value);
                }
                sqlConnection.Open();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Dispose();
                sqlConnection.Close();
                return Convert.ToInt32(resultParameter.Value);
            }
            catch (SqlException ex)
            {
                //Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
                return 0;
            }
        }
    }
}
