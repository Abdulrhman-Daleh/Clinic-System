using System.Drawing;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class UserCtrlDefaultSettings : UserControl
    {
        public UserCtrlDefaultSettings()
        {
            InitializeComponent();
            this.Font = new Font("sans serif", 20, FontStyle.Regular, GraphicsUnit.Pixel);
        }
    }
}
