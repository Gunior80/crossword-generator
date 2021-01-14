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
    public partial class UserDelForm : Form
    {
        Database db;
        string username;
        public UserDelForm(Database DB, string UserName)
        {
            InitializeComponent();
            db = DB;
            username = UserName;
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (ConfirmCheckBox.Checked)
            {
                db.DeleteUser(username);
                this.Close();
            }
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
