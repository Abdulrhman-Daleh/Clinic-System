using AppEnums.cs;
using Data_Access.DTOs;
using Log;
using LoggerMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PersonContactData
    {
        private static PersonContactDto ContactMapper(SqlDataReader reader)
        {
            try
            {
                return new PersonContactDto()
                {
                    ContactId = (int)reader["ContactID"],
                    PersonId = (int)reader["PersonID"],
                    Email = (reader["Email"] == DBNull.Value) ? "No Email" : (string)reader["Email"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    ContactType = ((EnContactTypes)reader["ContactTypeId"], (string)reader["ContactTypeName"])
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(ex.Message, "PersonContactData", "Application", EventLogEntryType.Error));
                return null;
            }
        }

        public static int AddNewContact(PersonContactDto contactDto)
        {
            int contactId = -1;

            string query = @"insert into ContactInformations (Email, PhoneNumber, PersonID, ContactTypeID) values
            (@Email, @PhoneNumber, @PersonID, @ContactTypeID); select scope_identity()";

            SqlParameter emailParam = null;

            if (string.IsNullOrEmpty(contactDto.Email))
                emailParam = new SqlParameter("@Email", DBNull.Value) { Value = DBNull.Value };
            else
                emailParam = new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = contactDto.Email };

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    emailParam,
                    new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20) {Value = contactDto.PhoneNumber },
                    new SqlParameter("@PersonID", SqlDbType.Int) {Value = contactDto.PersonId},
                    new SqlParameter("@ContactTypeID", SqlDbType.Int) {Value = contactDto.ContactType.ContactTypeId},
                };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out contactId)) { }
                ;

            return contactId;
        }

        public static bool UpdateContact(PersonContactDto contactDto)
        {
            int affectedRows = -1;

            string query = @"update ContactInformations set Email = @Email, PhoneNumber = @PhoneNumber, PersonID = @PersonID,
            ContactTypeID = @ContactTypeID where ContactID = @ContactId";

            SqlParameter emailParam = null;

            if (string.IsNullOrEmpty(contactDto.Email))
                emailParam = new SqlParameter("@Email", DBNull.Value) { Value = DBNull.Value };
            else
                emailParam = new SqlParameter("@Email", SqlDbType.VarChar, 50) { Value = contactDto.Email };

            List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    emailParam,
                    new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20) {Value = contactDto.PhoneNumber },
                    new SqlParameter("@PersonID", SqlDbType.Int) {Value = contactDto.PersonId},
                    new SqlParameter("@ContactId", SqlDbType.Int) {Value = contactDto.ContactId},
                    new SqlParameter("@ContactTypeID", SqlDbType.Int) {Value = contactDto.ContactType.ContactTypeId},
                };

            affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static async Task<BindingList<PersonContactDto>> GetShortContactInfo(int personId)
        {
            BindingList<PersonContactDto> contactDtos = new BindingList<PersonContactDto>();
            string query = @"select ContactID, PhoneNumber, Email, PersonID, ContactTypeID
                from ContactInformations where PersonID = @PersonID";

            SqlParameter param = new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId };

            contactDtos = await DataAccessHelper.ExecuteObjectList(query, new List<SqlParameter>() { param }, ContactMapper);

            return contactDtos;
        }

    }
}
