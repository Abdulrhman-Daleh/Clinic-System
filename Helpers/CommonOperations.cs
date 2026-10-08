using Business;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class CommonOperations
    {
        public static bool LoadDataToDGV<T>(DataGridView dgv, List<T> values, Label lblRecords)
        {
            dgv.DataSource = values;
            lblRecords.Text = $"{values.Count} records";
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            return true;
        }

        public static bool ResetDataGrid<T>(DataGridView dgv, List<T> values, Label lblRecords, string value)
        {
            if (ValidationHelper.IsEmptyOrNull(values) || values.Count == 0)
            {
                values = new List<T>();
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

        private static void SwitchingFilterHelper(DataGridView dgv, List<Person> people, Label lblRecords, ComboBox cbFilterBy, ComboBox cbMisc, TextBox txtFilterBy, string combBoxFilterName)
        {
            txtFilterBy.Text = string.Empty;
            cbMisc.SelectedIndex = 0;
            cbMisc.Visible = false;
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
                cbMisc.Focus();
                cbMisc.Visible = true;
                return;
            }

            txtFilterBy.Visible = true;
            txtFilterBy.Focus();
        }

        public static void HandleSwitchingFilter(ComboBox cbFilterBy, ComboBox cbMisc, TextBox txtFilterBy, DataGridView dgv, List<Person> people, Label lblRecords, string combBoxFilterName)
        {
            dgv.DataSource = people;
            lblRecords.Text = $"{dgv.Rows.Count} records";
            SwitchingFilterHelper(dgv, people, lblRecords, cbFilterBy, cbMisc, txtFilterBy, combBoxFilterName);
        }

        public static string GetFilterName(ComboBox cbFilterBy)
        {
            return cbFilterBy.Text;
        }

        public static List<object> FilterData<T>(List<T> list, string columnName, object valueToCheck)
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
                    return null;
                }
            }

            return temp;
        }
    }
}
