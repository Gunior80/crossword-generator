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
    public partial class UserAddForm : Form
    {
        Database db;
        public UserAddForm(Database DB)
        {
            InitializeComponent();
            db = DB;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (db.CheckUser(UserTextBox.Text, ""))
            {
                MessageBox.Show("Такой пользователь уже существует.");
            }
            else
            {
                db.CreateUser(UserTextBox.Text, PasswordTextBox.Text, DoEditCheckBox.Checked ? 1 : 0, IsAdminCheckBox.Checked ? 1 : 0);
                this.Close();
            }
        }
    }
}
