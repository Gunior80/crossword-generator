using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace crossword_generator
{
    public partial class CatForm : Form
    {
        Database db;
        ListBox ParentLB;
        public CatForm(Database DB, ListBox lb)
        {
            InitializeComponent();
            db = DB;
            ParentLB = lb;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CatForm_Load(object sender, EventArgs e)
        {
            ShowCat();
        }

        private void ShowCat()
        {
            listBoxCat.Items.Clear();
            foreach (Category cat in db.GetCategories())
            {
                listBoxCat.Items.Add(cat.Name);
            }
        }

        private void ShowWords(string cat)     // Выбрать и отобразить слова из категории
        {
            listBoxWord.Items.Clear();
            foreach (string word in db.GetWords(cat))
            {
                listBoxWord.Items.Add(word);
            }
        }

        private void buttonAddCat_Click(object sender, EventArgs e)  // Кнопка добавления категории
        {
            textBoxCat.Text = textBoxCat.Text.TrimStart();
            textBoxCat.Text = textBoxCat.Text.TrimEnd();
            if (textBoxCat.Text != "")
            {
                db.SetCategory(textBoxCat.Text.ToLower());
                ShowCat();
            }
            textBoxCat.Clear();
        }

        private void CatForm_FormClosing(object sender, FormClosingEventArgs e)  // Закрытие формы
        {
            ParentLB.Items.Clear();
            foreach (Category cat in db.GetCategories())
            {
                ParentLB.Items.Add(cat.Name);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // Клик по контекстному меню в категориях
        {
            foreach (string cat in listBoxCat.SelectedItems)
            {
                db.DeleteCategory(cat);
            }
            ShowCat();
            listBoxWord.Items.Clear();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) // Клик по контекстному меню в словах
        {

            foreach (string word in listBoxWord.SelectedItems)
            {
                db.DeleteWord(listBoxCat.SelectedItem.ToString(), word);
            }
            ShowWords(listBoxCat.SelectedItem.ToString());
        }

        private void listBoxCat_MouseDown(object sender, MouseEventArgs e)
        {

            if (listBoxCat.SelectedItems.Count != 1)
            {
                listBoxWord.Items.Clear();
            }
        }

        private void listBoxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCat.SelectedItems.Count == 1)
            {
                ShowWords(listBoxCat.SelectedItem.ToString());
            }
        }

        private void buttonAddWord_Click(object sender, EventArgs e)   // Кнопка добавления
        {
            if (listBoxCat.SelectedItems.Count == 1) {
                List<string> words = new List<string>(textBoxWord.Text.Split(' '));
                foreach(string word in words)
                {
                    var w = word.Trim();
                    if (w != "")
                    {
                        db.SetWord(listBoxCat.SelectedItem.ToString(), w.ToLower());
                        ShowWords(listBoxCat.SelectedItem.ToString());
                    }
                }
                textBoxWord.Clear();
            }
        }
    }
}
