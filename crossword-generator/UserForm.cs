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
    public partial class UserForm : Form
    {
        Database db;
        public UserForm(Database DB)
        {
            InitializeComponent();
            db = DB;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            foreach (string user in db.GetUsers())
            {
                listBoxUser.Items.Add(user);
            }
        }

        private void добавитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAddForm form = new UserAddForm(this.db);
            form.Show();
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (string user in listBoxUser.SelectedItems)
            {
                UserDelForm form = new UserDelForm(this.db, user);
                form.Show();
            }
        }

        private void изменитьПраваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (string user in listBoxUser.SelectedItems)
            {
                UserModForm form = new UserModForm(this.db, user);
                form.Show();
            }
        }

        private void UserForm_Activated(object sender, EventArgs e)
        {
            listBoxUser.Items.Clear();
            foreach (string user in db.GetUsers())
            {
                listBoxUser.Items.Add(user);
            }
        }
    }
}
