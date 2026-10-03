using System.Drawing;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class FormsDefaultSettings : Form
    {
        public FormsDefaultSettings()
        {
            InitializeComponent();
            this.Font = new Font("sans serif", 20, FontStyle.Regular, GraphicsUnit.Pixel);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}
