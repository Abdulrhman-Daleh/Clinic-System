
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
    public class MedicineData
    {
        private static MedicineDto MedicineMapper(SqlDataReader reader)
        {
            try
            {
                return new MedicineDto()
                {
                    MedicineID = (int)reader["MedicineID"],
                    DrugName = (string)reader["DrugName"],
                    StrengthValue = (decimal)reader["StrengthValue"],

                    StrengthType = (
                   (EnStrengthUnits)Convert.ToInt32(reader["StrengthUnitID"]),
                   (string)reader["StrengthUnitText"]),

                    DosageForm = (
                   (EnDosageForms)Convert.ToInt32(reader["DosageFormID"]),
                   (string)reader["DosageFormText"]),

                    UnitPrice = (decimal)reader["UnitPrice"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "MedicineData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        private static string DosageFormsMapper(SqlDataReader reader)
        {
            return (string)reader["DosageFormText"];
        }

        private static string StrengthUnitsMapper(SqlDataReader reader)
        {
            return (string)reader["StrengthUnitText"];
        }
        public static int AddNewMedicine(MedicineDto medicineDto)
        {
            int medicineId = -1;

            string query = @"
                INSERT INTO Medicines
                (
                    DrugName,
                    StrengthValue,
                    StrengthUnitID,
                    DosageFormID,
                    UnitPrice
                )
                VALUES
                (
                    @DrugName,
                    @StrengthValue,
                    @StrengthUnitID,
                    @DosageFormID,
                    @UnitPrice
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@DrugName", SqlDbType.VarChar)
                {
                    Value = medicineDto.DrugName
                },
                new SqlParameter("@StrengthValue", SqlDbType.Decimal)
                {
                    Value = medicineDto.StrengthValue
                },
               new SqlParameter("@StrengthUnitID", SqlDbType.Int)
                {
                    Value = (int)medicineDto.StrengthType.StrengthTypeID
                },

                new SqlParameter("@DosageFormID", SqlDbType.Int)
                {
                    Value = (int)medicineDto.DosageForm.DosageFormID
                },
                new SqlParameter("@UnitPrice", SqlDbType.Decimal)
                {
                    Value = medicineDto.UnitPrice
                }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out medicineId))
            {
            }

            return medicineId;
        }

        public static bool UpdateMedicine(MedicineDto medicineDto)
        {
            string query = @"
                UPDATE Medicines SET
                    DrugName = @DrugName,
                    StrengthValue = @StrengthValue,
                    StrengthUnitID = @StrengthUnitID,
                    DosageFormID = @DosageFormID,
                    UnitPrice = @UnitPrice
                WHERE MedicineID = @MedicineID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@MedicineID", SqlDbType.Int)
                {
                    Value = medicineDto.MedicineID
                },
                new SqlParameter("@DrugName", SqlDbType.VarChar)
                {
                    Value = medicineDto.DrugName
                },
                new SqlParameter("@StrengthValue", SqlDbType.Decimal)
                {
                    Value = medicineDto.StrengthValue
                },
                new SqlParameter("@StrengthUnitID", SqlDbType.Int)
                {
                    Value = medicineDto.StrengthType.StrengthTypeID
                },
                new SqlParameter("@DosageFormID", SqlDbType.Int)
                {
                    Value = medicineDto.DosageForm.DosageFormID
                },
                new SqlParameter("@UnitPrice", SqlDbType.Decimal)
                {
                    Value = medicineDto.UnitPrice
                }
            };

            int affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static MedicineDto FindMedicineByID(int medicineId)
        {
            string query = @"
                SELECT
                    m.MedicineID,
                    m.DrugName,
                    m.StrengthValue,
                    su.StrengthUnitID,
                    su.StrengthUnitText,
                    df.DosageFormID,
                    df.DosageFormText,
                    m.UnitPrice
                FROM Medicines AS m
                INNER JOIN StrengthUnits AS su
                    ON m.StrengthUnitID = su.StrengthUnitID
                INNER JOIN DosageForms AS df
                    ON m.DosageFormID = df.DosageFormID
                WHERE m.MedicineID = @MedicineID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@MedicineID", SqlDbType.Int)
                {
                    Value = medicineId
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                MedicineMapper);
        }

        public static async Task<BindingList<MedicineDto>> GetMedicines()
        {
            string query = @"
                SELECT
                    m.MedicineID,
                    m.DrugName,
                    m.StrengthValue,
                    su.StrengthUnitID,
                    su.StrengthUnitText,
                    df.DosageFormID,
                    df.DosageFormText,
                    m.UnitPrice
                FROM Medicines AS m
                INNER JOIN StrengthUnits AS su
                    ON m.StrengthUnitID = su.StrengthUnitID
                INNER JOIN DosageForms AS df
                    ON m.DosageFormID = df.DosageFormID";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                MedicineMapper);
        }

        public static async Task<BindingList<string>> GetMedicineStrengthUnits()
        {
            string query = @"SELECT
                    StrengthUnitText
                FROM StrengthUnits";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                StrengthUnitsMapper);
        }

        public static async Task<BindingList<string>> GetMedicineDosageForms()
        {
            string query = @"select DosageFormText from DosageForms";

            return await DataAccessHelper.ExecuteObjectList(
                query,
                null,
                DosageFormsMapper);
        }

        public static MedicineDto FindMedicineByDrugName(string drugName)
        {
            string query = @"
                SELECT
                    m.MedicineID,
                    m.DrugName,
                    m.StrengthValue,
                    su.StrengthUnitID,
                    su.StrengthUnitText,
                    df.DosageFormID,
                    df.DosageFormText,
                    m.UnitPrice
                FROM Medicines AS m
                INNER JOIN StrengthUnits AS su
                    ON m.StrengthUnitID = su.StrengthUnitID
                INNER JOIN DosageForms AS df
                    ON m.DosageFormID = df.DosageFormID
                WHERE m.DrugName = @drugName";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@drugName", SqlDbType.VarChar, 50)
                {
                    Value = drugName
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                MedicineMapper);
        }

    }
}
