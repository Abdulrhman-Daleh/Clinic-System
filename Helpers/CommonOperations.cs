using Business;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
                }
                return;
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

        public static List<object> FilterData<T>(BindingList<T> list, string columnName, object valueToCheck)
        {
            List<object> temp = new List<object>();

            if (ValidationHelper.IsEmptyOrNull(list))
                return temp;

            foreach (T item in list)
            {
                PropertyInfo prop = item.GetType().GetProperty(columnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop == null)
                {
                    return null;
                }

                object currentValue = prop.GetValue(item);

                if (currentValue == null && valueToCheck == null)
                    return null;

                if (currentValue == null || valueToCheck == null)
                    return null;

                try
                {
                    object convertedValue = Convert.ChangeType(valueToCheck, prop.PropertyType);

                    if (currentValue.Equals(convertedValue))
                    {
                        temp.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(new LoggerMessage.LogMessageInformation(ex.Message, "CommonOperations", "Application", EventLogEntryType.Error));
                    return null;
                }
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

    }
}