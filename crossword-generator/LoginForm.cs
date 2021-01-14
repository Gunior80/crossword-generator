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
    public partial class LoginForm : Form
    {
        Database db;
        Form pform;
        bool status = false;
        public LoginForm(Database DB, Form PForm )
        {
            InitializeComponent();
            db = DB;
            pform = PForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.CheckUser(this.LoginTextBox.Text, this.PasswordTextBox2.Text))
            {
                status = true;
                this.Close();
            }
            else
            {
                LoginTextBox.Text = "";
                PasswordTextBox2.Text = "";
            }
            
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (status)
            {
                pform.Enabled = true;
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
