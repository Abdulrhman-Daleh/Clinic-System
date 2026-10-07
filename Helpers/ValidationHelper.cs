using Business;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateRequiredTextBox(Control control, ErrorProvider errors)
        {
            if (Util.IsInputEmpty(control.Text))
            {
                errors.SetError(control, "this field must be filled");
                return false;
            }

            errors.SetError(control, null);
            return true;
        }
        public static bool UserControlHasErrors(Control control, ErrorProvider errors)
        {
            foreach (Control c in control.Controls)
            {
                if (!string.IsNullOrWhiteSpace(errors.GetError(c)))
                    return true;

                foreach (Control child in control.Controls)
                {
                    if (UserControlHasErrors(child, errors))
                        return true;
                }
            }

            return false;
        }
        public static bool IsEmptyOrNull<T>(T value, string message = null)
        {
            bool result = string.IsNullOrWhiteSpace(value != null ? value.ToString() : string.Empty);

            if (result)
            {
                if (message != null)
                    MessageBox.Show(message, "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return result;
            }

            return false;
        }
        public static void LockComboBox(KeyPressEventArgs e) => e.Handled = true;
        public static void HandleInputType(KeyPressEventArgs e, bool AllowDigit, bool AllowText)
        {
            if (AllowDigit && AllowText)
            {
                e.Handled = false;
                return;
            }

            if (AllowDigit)
            {
                if (char.IsLetter(e.KeyChar))
                    e.Handled = true;
                else
                    e.Handled = false;

            }
            else
            {
                if (char.IsDigit(e.KeyChar))
                    e.Handled = true;
                else
                    e.Handled = false;
            }
        }
        public static void ResetDefaultFilter(TextBox txtBox)
        {
            txtBox.Text = string.Empty;
            txtBox.Focus();
        }
        public static bool IsEmailValid(string email)
        {
            if (IsEmptyOrNull(email, null))
                return true;

            string strPattern = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
            Regex rgx = new Regex(strPattern);

            return rgx.IsMatch(email);
        }
        private static int CountPhoneDigits(string phoneNumber)
        {
            return phoneNumber.Count(char.IsDigit);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return CountPhoneDigits(phoneNumber) == 10;
        }

    }
}
