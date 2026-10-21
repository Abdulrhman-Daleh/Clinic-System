using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data_Access
{
    public class PersonData
    {
        private static PersonDto PersonMapper(SqlDataReader reader)
        {
            return new PersonDto()
            {
                PersonId = (int)reader["PersonID"],
                Firstname = (string)reader["Firstname"],
                Lastname = (string)reader["Lastname"],
                Gender = Convert.ToChar(reader["Gender"])
            };
        }

        public static bool UpdatePerson(PersonDto personInfo, PersonContactDto personalContact)
        {
            int affactedRows = -1;

            string query = @" update People
        set Firstname = @Firstname, Lastname = @Lastname, Gender = @Gender
        where PersonID = @personId";

            List<SqlParameter> paramters = new List<SqlParameter>()
            {
                new SqlParameter("@PersonId", SqlDbType.Int) {Value = personInfo.PersonId},
                new SqlParameter("@Firstname", SqlDbType.VarChar, 50){Value = personInfo.Firstname},
                new SqlParameter("@Lastname", SqlDbType.VarChar, 50){Value = personInfo.Lastname},
                new SqlParameter("@Gender", SqlDbType.VarChar, 1){ Value = personInfo.Gender},
            };

            try
            {
                affactedRows = DataAccessHelper.ExecuteNonQuery(query, paramters);
            }
            catch (Exception ex)
            {

            }

            return affactedRows > 0;
        }

        public static PersonDto FindPersonById(int personId)
        {
            PersonDto person = null;
            string query = @"select PersonID, Firstname, Lastname, Gender from People where PersonID = @PersonId";

            try
            {
                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonId", SqlDbType.Int) {Value = personId},
                };

                person = DataAccessHelper.ExecuteOneObject(query, paramters, PersonMapper);
            }
            catch (Exception ex)
            {

            }
            return person;
        }

        public static PersonDto FindPersonByFirstname(string Firstname)
        {
            PersonDto person = null;
            string query = @"select PersonID, Firstname, Lastname, Gender from People where Firstname = @Firstname";

            try
            {
                List<SqlParameter> paramters = new List<SqlParameter>()
                {
                    new SqlParameter("@Firstname", SqlDbType.VarChar, 50) {Value = Firstname},
                };

                person = DataAccessHelper.ExecuteOneObject(query, paramters, PersonMapper);

            }
            catch (Exception ex)
            {

            }

            return person;
        }

        public static async Task<BindingList<PersonDto>> GetPeople()
        {
            BindingList<PersonDto> people = new BindingList<PersonDto>();

            string query = @"select PersonID, Firstname, Lastname, Gender from People";

            try
            {
                people = await DataAccessHelper.ExecuteObjectList(query, null, PersonMapper);
            }
            catch (Exception ex)
            {

            }

            return people;
        }


    }
}