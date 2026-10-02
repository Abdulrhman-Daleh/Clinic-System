using Data_Access.DTOs;
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
    }
}