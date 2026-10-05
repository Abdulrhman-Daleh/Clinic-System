using Business;
using System.Windows.Forms;

namespace ClinicSystem.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateRequiredTextBox(TextBox txtBox, ErrorProvider errors)
        {
            if (Util.IsInputEmpty(txtBox.Text))
            {
                errors.SetError(txtBox, "this field must be filled");
                return false;
            }

            errors.SetError(txtBox, null);
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


        public static bool IsValidEmptyOrNull<T>(T value) => string.IsNullOrWhiteSpace(value != null ? value.ToString() : string.Empty);

    }
}
