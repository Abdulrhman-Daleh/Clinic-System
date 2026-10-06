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

            string query = @"insert into People
            (
            Firstname,
            Lastname,
            Gender
            )
        values
        (@Firstname, @Lastname, @Gender); select scope_identity()";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = personInfo.Firstname;
                        command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = personInfo.Lastname;
                        command.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = personInfo.Gender;

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out personId)) ;
                    }
                }
                catch (Exception ex)
                {
                    //log later
                }
            }

            return personId;
        }

        public static bool UpdatePerson(PersonDto personInfo, PersonContactDto personalContact)
        {
            int affactedRows = -1;

            string query = @" update People
        set Firstname = @Firstname, Lastname = @Lastname, Gender = @Gender
        where PersonID = @personId";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@PersonId", SqlDbType.Int).Value = personInfo.PersonId;
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = personInfo.Firstname;
                    command.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = personInfo.Lastname;
                    command.Parameters.Add("@Gender", SqlDbType.VarChar, 1).Value = personInfo.Gender;

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

        public static PersonDto FindPersonByFirstname(string Firstname)
        {
            string query = @"select PersonID, Lastname, Gender from People where Firstname = @Firstname";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Firstname", SqlDbType.VarChar, 30).Value = Firstname;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new PersonDto()
                            {
                                Firstname = Firstname,
                                Lastname = (string)reader["Lastname"],
                                Gender = Convert.ToChar(reader["Gender"]),
                                PersonId = (int)reader["PersonID"]
                            };
                        }
                    }

                }
            }

            return null;
        }

    }
}