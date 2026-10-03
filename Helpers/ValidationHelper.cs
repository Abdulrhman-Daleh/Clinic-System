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

    }
}
