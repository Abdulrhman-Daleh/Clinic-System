using Data_Access.DTOs;
using System;
using System.Data;
using System.Data.SqlClient;

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


    }
}
