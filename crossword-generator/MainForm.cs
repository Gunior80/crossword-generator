using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace crossword_generator
{
    public partial class MainForm : Form
    {
        Database db;
        bool filled = false;
        List<List<char>> CurrentGrid;
        public MainForm()
        {
            InitializeComponent();
            db = new Database();
        }

        private void buttonGen_Click(object sender, EventArgs e)
        {
            if (listBoxCat.SelectedItems.Count > 0)
            {
                List<string> word_list = new List<string>();
                word_list.Clear();
                foreach (string cat in listBoxCat.SelectedItems)
                {
                    foreach (string word in db.GetWords(cat))
                    {
                        word_list.Add(word);
                    }
                }

                if (word_list.Count > 1)
                {
                    int CurCount = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        Generator a = new Generator('-', word_list);
                        a.generate_crossword((int)numericUpDown1.Value);
                        if (CurCount < a.used_words.Count)
                        {
                            CurCount = a.used_words.Count;
                            CurrentGrid = a.GetGrid;
                        }
                    }

                    Draw();
                    buttonSave.Enabled = true;
                }
            } 
        }

        private void Draw()
        {
            Painter.DrawImage(CurrentGrid, filled, pictureBox1);
            pictureBox1.Refresh();
        }

        private void buttonFill_Click(object sender, EventArgs e)
        {
            if (filled)
            {
                toolStripStatusLabel1.Text = "Заполнение выключено";
            }
            else
            {
                toolStripStatusLabel1.Text = "Заполнение включено";
            }
            filled = !filled;
            if (CurrentGrid != null)
            {
                Draw();
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Jpeg|*.jpg";
            ImageFormat format = ImageFormat.Jpeg;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }

        private void категорииИСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CatForm FCat = new CatForm(this.db, listBoxCat);
            FCat.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBoxCat.Items.Clear();
            foreach (Category cat in db.GetCategories())
            {
                listBoxCat.Items.Add(cat.Name);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа \"Генератор кроссвордов\" разработана\n отделом информации для нужд\n ГБУК НАО \"НЦБ им. А.И. Пичкова\" ",
                            "О программе",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
