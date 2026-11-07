
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
    public class PrescriptionItemData
    {
        private static PrescriptionItemDto PrescriptionItemMapper(SqlDataReader reader)
        {
            try
            {
                return new PrescriptionItemDto()
                {
                    ItemID = (int)reader["ItemID"],
                    MedicineID = (int)reader["MedicineID"],
                    PrescriptionID = (int)reader["PrescriptionID"],
                    Dose = (byte)reader["Dose"],
                    Frequency = (string)reader["Frequency"],
                    Quantity = (byte)reader["Quantity"],
                    Duration = (string)reader["Duration"]
                };
            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LogMessageInformation(
                    ex.Message,
                    "PrescriptionItemData",
                    "Application",
                    EventLogEntryType.Error));

                return null;
            }
        }

        public static int AddNewPrescriptionItem(PrescriptionItemDto itemDto)
        {
            int itemId = -1;

            string query = @"
                INSERT INTO PrescriptionItems
                (
                    MedicineID,
                    PrescriptionID,
                    Dose,
                    Frequency,
                    Quantity,
                    Duration
                )
                VALUES
                (
                    @MedicineID,
                    @PrescriptionID,
                    @Dose,
                    @Frequency,
                    @Quantity,
                    @Duration
                );

                SELECT SCOPE_IDENTITY();";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@MedicineID", SqlDbType.Int)
                {
                    Value = itemDto.MedicineID
                },
                new SqlParameter("@PrescriptionID", SqlDbType.Int)
                {
                    Value = itemDto.PrescriptionID
                },
                new SqlParameter("@Dose", SqlDbType.TinyInt)
                {
                    Value = itemDto.Dose
                },
                new SqlParameter("@Frequency", SqlDbType.VarChar)
                {
                    Value = itemDto.Frequency
                },
                new SqlParameter("@Quantity", SqlDbType.TinyInt)
                {
                    Value = itemDto.Quantity
                },
                new SqlParameter("@Duration", SqlDbType.VarChar)
                {
                    Value = itemDto.Duration
                }
            };

            object result = DataAccessHelper.ExecuteScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out itemId))
            {
            }

            return itemId;
        }

        public static bool UpdatePrescriptionItem(PrescriptionItemDto itemDto)
        {
            string query = @"
                UPDATE PrescriptionItems SET
                    MedicineID = @MedicineID,
                    PrescriptionID = @PrescriptionID,
                    Dose = @Dose,
                    Frequency = @Frequency,
                    Quantity = @Quantity,
                    Duration = @Duration
                WHERE ItemID = @ItemID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ItemID", SqlDbType.Int)
                {
                    Value = itemDto.ItemID
                },
                new SqlParameter("@MedicineID", SqlDbType.Int)
                {
                    Value = itemDto.MedicineID
                },
                new SqlParameter("@PrescriptionID", SqlDbType.Int)
                {
                    Value = itemDto.PrescriptionID
                },
                new SqlParameter("@Dose", SqlDbType.TinyInt)
                {
                    Value = itemDto.Dose
                },
                new SqlParameter("@Frequency", SqlDbType.VarChar)
                {
                    Value = itemDto.Frequency
                },
                new SqlParameter("@Quantity", SqlDbType.TinyInt)
                {
                    Value = itemDto.Quantity
                },
                new SqlParameter("@Duration", SqlDbType.VarChar)
                {
                    Value = itemDto.Duration
                }
            };

            int affectedRows = DataAccessHelper.ExecuteNonQuery(query, parameters);

            return affectedRows > 0;
        }

        public static PrescriptionItemDto FindPrescriptionItemByID(int itemId)
        {
            string query = @"
                SELECT
                    ItemID,
                    MedicineID,
                    PrescriptionID,
                    Dose,
                    Frequency,
                    Quantity,
                    Duration
                FROM PrescriptionItems
                WHERE ItemID = @ItemID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@ItemID", SqlDbType.Int)
                {
                    Value = itemId
                }
            };

            return DataAccessHelper.ExecuteOneObject(
                query,
                parameters,
                PrescriptionItemMapper);
        }

        public static async Task<BindingList<PrescriptionItemDto>> GetPrescriptionItems(int prescriptionId)
        {
            string query = @"
                SELECT
                    ItemID,
                    MedicineID,
                    PrescriptionID,
                    Dose,
                    Frequency,
                    Quantity,
                    Duration
                FROM PrescriptionItems
                WHERE PrescriptionID = @PrescriptionID";

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@PrescriptionID", SqlDbType.Int)
                {
                    Value = prescriptionId
                }
            };

            return await DataAccessHelper.ExecuteObjectList(
                query,
                parameters,
                PrescriptionItemMapper);
        }
    }
}
