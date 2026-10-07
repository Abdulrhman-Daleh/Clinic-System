using Business;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class CommonOperations
    {
        public static bool LoadDataToDGV<T>(DataGridView dgv, List<T> values, Label lblRecords)
        {
            if (values.Count == 0)
                return false;

            dgv.DataSource = values;
            lblRecords.Text = $"{values.Count} records";
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            return true;
        }

        private static void SwitchingFilterHelper(ComboBox cbFilterBy, ComboBox cbMisc, TextBox txtFilterBy, string combBoxFilterName)
        {
            cbMisc.SelectedIndex = 0;
            txtFilterBy.Text = string.Empty;
            cbMisc.Visible = false;
            txtFilterBy.Visible = false;
            string filterName = GetFilterName(cbFilterBy);

            if (filterName == "None")
            {
                cbFilterBy.Focus();
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
            SwitchingFilterHelper(cbFilterBy, cbMisc, txtFilterBy, combBoxFilterName);
        }

        public static string GetFilterName(ComboBox cbFilterBy)
        {
            return cbFilterBy.Text;
        }
    }
}
