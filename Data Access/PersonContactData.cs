using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PersonContactData
    {
        public static int AddNewContact(PersonContactDto contactDto)
        {
            int contactId = -1;

            string query = @"insert into ContactInformations (Email, PhoneNumber, PersonID, ContactTypeID) values
            (@Email, @PhoneNumber, @PersonID, @ContactTypeID); select scope_identity()";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (string.IsNullOrEmpty(contactDto.Email))
                            command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                        else
                            command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = contactDto.Email;

                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = contactDto.PhoneNumber;
                        command.Parameters.Add("@PersonID", SqlDbType.Int).Value = contactDto.PersonId;
                        command.Parameters.Add("@ContactTypeID", SqlDbType.Int).Value = contactDto.ContactTypeId;

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out contactId)) ;
                    }
                }
                catch (Exception ex)
                {
                    // log it later
                }
            }

            return contactId;
        }

        public static bool UpdateContact(PersonContactDto contactDto)
        {
            int affectedRows = -1;

            string query = @"update ContactInformations set Email = @Email, PhoneNumber = @PhoneNumber, PersonID = @PersonID,
            ContactTypeID = @ContactTypeID where ContactID = @ContactId";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (string.IsNullOrEmpty(contactDto.Email))
                            command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                        else
                            command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = contactDto.Email;

                        command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 20).Value = contactDto.PhoneNumber;
                        command.Parameters.Add("@PersonID", SqlDbType.Int).Value = contactDto.PersonId;
                        command.Parameters.Add("@ContactTypeID", SqlDbType.Int).Value = contactDto.ContactTypeId;
                        command.Parameters.Add("@ContactId", SqlDbType.Int).Value = contactDto.ContactId;

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // log it later
                }
            }

            return affectedRows > 0;
        }

        public static async Task<List<PersonContactDto>> GetShortContactInfo(int personId)
        {
            List<PersonContactDto> contactDtos = new List<PersonContactDto>();
            string query = @"select ContactID, PhoneNumber, Email, PersonID, ContactTypeID
                from ContactInformations where PersonID = @personId";

            using (SqlConnection connection = new SqlConnection(AccessString.ConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@personId", SqlDbType.Int).Value = personId;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                contactDtos.Add(new PersonContactDto
                                {
                                    ContactId = (int)reader["ContactID"],
                                    ContactTypeId = (int)reader["ContactTypeID"],
                                    PersonId = (int)reader["PersonID"],
                                    PhoneNumber = (string)reader["PhoneNumber"],
                                    Email = (string)reader["Email"] ?? "No Email"
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return contactDtos;
        }

    }
}
