using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class DataAccessHelper
    {
        public static object ExecuteScalar(string query, List<SqlParameter> paramters)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (paramters != null)
                        command.Parameters.AddRange(paramters.ToArray());

                    try
                    {
                        connection.Open();
                        result = command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }

            return result;
        }

        public static int ExecuteNonQuery(string query, List<SqlParameter> paramters)
        {
            int affactedRows = -1;

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (paramters != null)
                        command.Parameters.AddRange(paramters.ToArray());

                    try
                    {
                        connection.Open();
                        affactedRows = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }

            return affactedRows;
        }

        public static T ExecuteOneObject<T>(string query, List<SqlParameter> paramters, Func<SqlDataReader, T> map)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (paramters != null)
                            command.Parameters.AddRange(paramters.ToArray());

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                return map(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return default(T);
        }

        public static async Task<BindingList<T>> ExecuteObjectList<T>(string query, List<SqlParameter> paramters, Func<SqlDataReader, T> map)
        {
            BindingList<T> objs = new BindingList<T>();

            try
            {
                using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (paramters != null)
                            command.Parameters.AddRange(paramters.ToArray());

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                objs.Add(map(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return objs;
        }

        public static bool ExecuteNonQueryStoredProcedure<T>(string storedProcedureName, List<SqlParameter> paramters, List<SqlParameter> outputParamters)
        {
            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (paramters != null)
                        command.Parameters.AddRange(paramters.ToArray());

                    if (outputParamters != null)
                        command.Parameters.AddRange(outputParamters.ToArray());

                    try
                    {
                        connection.Open();
                        var affectedRows = command.ExecuteNonQuery();

                        if (affectedRows > 0)
                            return true;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }

            return false;
        }

        public static int ExecuteNonQueryStoredProcedure<T>(string storedProcedureName, List<SqlParameter> paramters)
        {
            int affectedRows = 0;

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (paramters != null)
                        command.Parameters.AddRange(paramters.ToArray());

                    try
                    {
                        connection.Open();
                        affectedRows = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }

            return affectedRows;
        }

    }
}
