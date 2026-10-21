using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data_Access
{
    public class PatientData
    {
        public static int AddNewPatient(PatientDto patientDto)
        {
            int patientId = -1;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@PersonId", SqlDbType.Int) {Value = patientDto.PersonId},
                    new SqlParameter("@Firstname", SqlDbType.VarChar, 40) {Value = patientDto.Firstname},
                    new SqlParameter("@Lastname", SqlDbType.VarChar, 40) {Value = patientDto.Lastname},
                    new SqlParameter("@Gender", SqlDbType.VarChar, 1) {Value = patientDto.Gender},
                };

                List<SqlParameter> outputParamters = new List<SqlParameter>()
                {
                    new SqlParameter("@PatientId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                List<object> values = DataAccessHelper.ExecuteNonQueryStoredProcedure<int>("AddPatient", parameters, outputParamters);

                if (values.Count > 0)
                {
                    patientId = Convert.ToInt32(values[0]);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return patientId;
        }
    }
}
