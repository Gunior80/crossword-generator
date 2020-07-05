using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace crossword_generator
{
    public class Category
    {
        public string ID, Name;
        public Category(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public class Database
    {
        string DataBaseName;
        public Database()
        {
            DataBaseName = "crossword.db";
            if (!File.Exists(DataBaseName))
            {
                SQLiteConnection.CreateFile(DataBaseName);
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE categories (id INTEGER PRIMARY KEY, name_category TEXT);", connection);
                connection.Open();
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE words (id INTEGER PRIMARY KEY, word TEXT, cat_id INTEGER, FOREIGN KEY (cat_id) REFERENCES category(id) ON UPDATE CASCADE ON DELETE CASCADE);", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> data = new List<Category>();
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM categories;", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                data.Add(new Category(record["id"].ToString(), record["name_category"].ToString()));
            }
            return data;
        }

        public void SetCategory(string name)
        {
            List<Category> list = GetCategories();
            foreach (Category cat in list)
            {
                if (cat.Name.ToLower() == name.ToLower())
                {
                    return;
                }
            }
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
            SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO categories(name_category) VALUES(\"{0}\");", name), connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteCategory(string name)
        {
            List<Category> list = GetCategories();
            foreach (Category cat in list)
            {
                if (cat.Name.ToLower() == name.ToLower())
                {
                    List<string> words = GetWords(cat.Name);
                    foreach (string word in words)
                    {
                        DeleteWord(cat.Name, word);
                    }
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                    SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM categories WHERE name_category = \"{0}\";", name), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public List<string> GetWords(string CatName)
        {
            List<Category> list = GetCategories();
            foreach (Category cat in list)
            {
                if (cat.Name.ToLower() == CatName.ToLower())
                {
                    List<string> data = new List<string>();
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(string.Format("SELECT * FROM words WHERE cat_id = \"{0}\";", cat.ID), connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    foreach (DbDataRecord record in reader)
                    {
                        data.Add(record["word"].ToString());
                    }
                    return data;
                }
            }
            return null;

        }

        public void SetWord(string CatName, string text)
        {
            List<Category> list = GetCategories();
            foreach (Category cat in list)
            {
                if (cat.Name.ToLower() == CatName.ToLower())
                {
                    foreach (string word in GetWords(CatName))
                    {
                        if (text == word)
                        {
                            return;
                        }
                    }
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                    SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO words(cat_id, word) VALUES({0}, \"{1}\");", cat.ID, text), connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void DeleteWord(string CatName, string word)
        {
            List<Category> list = GetCategories();
            foreach (Category cat in list)
            {
                if (cat.Name.ToLower() == CatName.ToLower())
                {
                    foreach (string w in GetWords(CatName))
                    {
                        if (w == word)
                        {
                            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                            connection.Open();
                            SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM words WHERE cat_id = {0} AND word = \"{1}\";", cat.ID, word), connection);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}
