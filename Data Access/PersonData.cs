using Data_Access.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data_Access
{
    public class PersonData
    {
        public static int AddNewPerson(PersonDto personInfo, PersonContactDto personalContact)
        {
            int personId = -1;

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = personInfo.Firstname;
                    command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = personInfo.Lastname;
                    command.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = personInfo.Gender;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 30).Value = personalContact.PhoneNumber;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = personalContact.Email;
                    command.Parameters.Add("@ContactTypeId", SqlDbType.Int).Value = personalContact.ContactTypeId;

                    SqlParameter outputParam = new SqlParameter("@personId", SqlDbType.Int);
                    outputParam.Direction = ParameterDirection.Output;

                    command.Parameters.Add(outputParam);

                    command.ExecuteNonQuery();

                    personId = (int)outputParam.Value;
                }
            }

            return personId;
        }

        public static bool UpdatePerson(PersonDto personInfo, PersonContactDto personalContact)
        {
            int affactedRows = -1;

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personInfo.PersonId;
                    command.Parameters.Add("@ContactId", SqlDbType.Int).Value = personalContact.ContactId;
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = personInfo.Firstname;
                    command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = personInfo.Lastname;
                    command.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = personInfo.Gender;
                    command.Parameters.Add("@ContactTypeId", SqlDbType.Int).Value = personalContact.ContactTypeId;

                    if (personalContact.Email == null)
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = personalContact.Email;

                    if (personalContact.PhoneNumber == null)
                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = personalContact.PhoneNumber;


                    affactedRows = command.ExecuteNonQuery();
                }
            }

            return affactedRows > 0;
        }

        public static PersonDto FindPersonById(int personId)
        {
            string query = @"select Firstname, Lastname, Gender from People where PersonID = @PersonId";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new PersonDto()
                            {
                                Firstname = (string)reader["Firstname"],
                                Lastname = (string)reader["Lastname"],
                                Gender = Convert.ToChar(reader["Gender"]),
                                PersonId = personId
                            };
                        }
                    }

                }
            }

            return null;
        }
    }
}