using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
