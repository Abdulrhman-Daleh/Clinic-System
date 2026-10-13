using ClinicSystem.People;
using ClinicSystem.People.Controls;
using System;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class MainForm : FormsDefaultSettings
    {
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void managePeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople managePeople = new ManagePeople();
            managePeople.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddUpdatePerson addPerson = new AddUpdatePerson();
            addPerson.ShowDialog();
        }

        private void viewPersonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ViewPerson viewPerson = new ViewPerson();
            viewPerson.ShowDialog();
        }

        private void addPersonContactToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            AddPersonContacts addContact = new AddPersonContacts();
            addContact.ShowDialog();
        }
    }
}
