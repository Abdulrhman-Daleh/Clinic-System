
using AppEnums.cs;
using Business;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class ValidationHelper
    {
        public static OperationResult ValidateRequiredTextBox(Control control, ErrorProvider errors)
        {
            if (Util.IsInputEmpty(control.Text))
            {
                errors.SetError(control, "this field must be filled");

                return new OperationResult(
                    "This field must be filled.",
                    "Invalid",
                    false);
            }

            errors.SetError(control, null);

            return new OperationResult(
                "Input is valid.",
                "Valid",
                true);
        }

        public static OperationResult ValidateForm(Control control, ErrorProvider errors)
        {
            foreach (Control c in control.Controls)
            {
                string error = errors.GetError(c);

                if (!string.IsNullOrWhiteSpace(error))
                {
                    return new OperationResult(
                        error,
                        "Not Valid",
                        false);
                }

                OperationResult result = ValidateForm(c, errors);

                if (!result.IsSuccess)
                    return result;
            }

            return new OperationResult(
                "Form is valid to save.",
                "Valid",
                true);
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
                if (char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
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

        public static void SetApproperiteMaxLength(
            TextBox txtFilterBy,
            List<string> allowedIntColumns,
            string currentColumn,
            int numAllowedLength,
            int textAllowedLength)
        {
            if (allowedIntColumns.Contains(currentColumn))
                txtFilterBy.MaxLength = numAllowedLength;
            else
                txtFilterBy.MaxLength = textAllowedLength;
        }

        public static bool IsDgvEmptyOrNull(DataGridView dgv)
        {
            if (IsEmptyOrNull(dgv))
                return true;

            return dgv.RowCount <= 0;
        }

        public static bool CheckAcceptablePasswordStrength(string password)
        {
            bool goodLength = false;
            bool hasNum = false;
            bool hasPunc = false;

            goodLength = password.Length >= 10;

            if (!goodLength)
                return false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                    hasNum = true;

                if (char.IsPunctuation(c))
                    hasPunc = true;

                if (goodLength && hasNum && hasPunc)
                    return true;
            }

            return false;
        }

        public static bool Date1BeforeDate2(System.DateTime date1, System.DateTime date2)
        {
            return date1.Date < date2.Date;
        }

        public static bool CheckAccess(params EnRole[] allowedRoles)
        {
            OperationResult result = Util.CanAccessForm(
                allowedRoles);

            if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message, result.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }
    }
}
