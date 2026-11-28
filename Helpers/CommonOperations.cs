using Business;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class CommonOperations
    {
        public static bool LoadDataToDGV<T>(DataGridView dgv, T values, Label lblRecords)
        {
            dgv.DataSource = values;
            lblRecords.Text = $"{dgv.Rows.Count} records";
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.ScrollBars = ScrollBars.Both;
            return true;
        }
        public static bool ResetDataGrid<T>(DataGridView dgv, BindingList<T> values, Label lblRecords, string value)
        {
            if (ValidationHelper.IsEmptyOrNull(values) || values.Count == 0)
            {
                values = new BindingList<T>();
                dgv.DataSource = values;
                lblRecords.Text = $"{values.Count} records";
                return true;
            }

            if (ValidationHelper.IsEmptyOrNull(value))
            {
                dgv.DataSource = values;
                lblRecords.Text = $"{values.Count} records";
                return true;
            }

            return false;
        }
        private static void SwitchingFilterHelper<T>(DataGridView dgv, BindingList<T> people, Label lblRecords, ComboBox cbFilterBy, ComboBox cbMisc, TextBox txtFilterBy, string combBoxFilterName)
        {
            txtFilterBy.Text = string.Empty;
            if (cbMisc != null)
            {
                cbMisc.SelectedIndex = 0;
                cbMisc.Visible = false;
            }

            txtFilterBy.Visible = false;
            string filterName = GetFilterName(cbFilterBy);

            if (filterName == "None")
            {
                cbFilterBy.Focus();
                LoadDataToDGV(dgv, people, lblRecords);
                return;
            }

            if (filterName == combBoxFilterName)
            {
                if (cbMisc != null)
                {
                    cbMisc.Focus();
                    cbMisc.Visible = true;
                    return;
                }
            }

            txtFilterBy.Visible = true;
            txtFilterBy.Focus();
        }
        public static void HandleSwitchingFilter<T>(ComboBox cbFilterBy, ComboBox cbMisc, TextBox txtFilterBy, DataGridView dgv, BindingList<T> people, Label lblRecords, string combBoxFilterName)
        {
            dgv.DataSource = people;
            lblRecords.Text = $"{dgv.Rows.Count} records";
            SwitchingFilterHelper(dgv, people, lblRecords, cbFilterBy, cbMisc, txtFilterBy, combBoxFilterName);
        }
        public static string GetFilterName(ComboBox cbFilterBy)
        {
            return cbFilterBy.Text;
        }
        public static List<object> FilterData<T>(BindingList<T> list, string columnName, string valueToCheck)
        {
            List<object> temp = new List<object>();

            if (ValidationHelper.IsEmptyOrNull(list))
                return temp;

            PropertyInfo property = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).FirstOrDefault(x => x.Name.ToLower() == columnName.ToLower());

            try
            {
                if (property != null)
                {
                    foreach (T item in list)
                    {
                        string propertyValue = property.GetValue(item).ToString();

                        if (propertyValue.Contains(valueToCheck))
                            temp.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "CommonOperations", "Application", EventLogEntryType.Error));
                return new List<object>();
            }

            return temp;
        }
        public static void SetUpDefaultControlsValue(List<Control> controls, string defaultValue)
        {
            foreach (Control c in controls)
                c.Text = $"[{defaultValue}]";
        }
        public static void UpdateAfterAdding<T>(BindingList<T> list, T obj, int targetIndex = -1)
        {
            Util.RefreshRecordsList(list, obj, targetIndex);
        }
        public static void SaveUsernameToFile(string fileName, string userName)
        {
            if (File.Exists(fileName))
            {
                using (StreamWriter sw = new StreamWriter(fileName, false))
                    sw.WriteLine(userName);
            }
            else
            {
                using (var stream = File.Create(fileName))
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                        sw.WriteLine(userName);
                }
            }
        }
        public static string ReadRememberedUsername(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                    return reader.ReadLine();
            }

            return "";
        }
        public static int FindTargetIndexOfObj<T, U>(BindingList<T> list, string propertyName, U value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var convertedValue = Convert.ChangeType(list[i].GetType().GetProperty(propertyName).GetValue(list[i]), TypeCode.Int32);
                int sendValue = Convert.ToInt32(value);

                if (convertedValue.Equals(sendValue))
                    return i;
            }

            return -1;
        }
    }
}