using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crossword_generator
{
    public partial class UserModForm : Form
    {
        Database db;
        string username;
        public UserModForm(Database DB, string UserName)
        {
            InitializeComponent();
            db = DB;
            username = UserName;
        }

        private void UserModForm_Load(object sender, EventArgs e)
        {
            UserTextBox.Text = username;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if(PasswordTextBox.Text == "")
            {
                db.ModifyUser(username, "", DoEditCheckBox.Checked ? 1 : 0, IsAdminCheckBox.Checked ? 1 : 0);
            }
            else
            {
                db.ModifyUser(username, PasswordTextBox.Text, DoEditCheckBox.Checked ? 1 : 0, IsAdminCheckBox.Checked ? 1 : 0);
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
