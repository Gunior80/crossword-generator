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
        public Database(string dbName = "")
        {
            if (dbName != "")
            {
                DataBaseName = dbName;
            }
            DataBaseName = "crossword.db";
            if (!File.Exists(DataBaseName))
            {
                SQLiteConnection.CreateFile(DataBaseName);
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("CREATE TABLE categories (id INTEGER PRIMARY KEY, name_category TEXT);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE words (id INTEGER PRIMARY KEY, word TEXT, cat_id INTEGER, FOREIGN KEY (cat_id) REFERENCES category(id) ON UPDATE CASCADE ON DELETE CASCADE);", connection);
                command.ExecuteNonQuery();
                command = new SQLiteCommand("CREATE TABLE users (id INTEGER PRIMARY KEY, username TEXT, password TEXT, do_edit INTEGER, is_admin INTEGER);", connection);
                command.ExecuteNonQuery();
                connection.Close();
                CreateUser("admin", "admin", 1, 1);
                
            }
        }
        public bool CreateUser(string username, string password, int do_edit, int is_admin)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO users(username, password, do_edit, is_admin) VALUES(\"{0}\", \"{1}\", \"{2}\", \"{3}\");", username, password, do_edit, is_admin), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> GetUsers()
        {
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM users", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<string> data = new List<string>();
            foreach (DbDataRecord record in reader)
            {
                data.Add(record["username"].ToString());
            }
            return data;
        }

        public bool CheckUser(string username, string password)
        {
            
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
            connection.Open();
            SQLiteCommand command;
            if (password == "")
            {
                command = new SQLiteCommand(string.Format("SELECT * FROM users WHERE username = \"{0}\";", username), connection);
            }
            else
            {
                command = new SQLiteCommand(string.Format("SELECT * FROM users WHERE username = \"{0}\" AND password = \"{1}\";", username, password), connection);
            }
            SQLiteDataReader reader = command.ExecuteReader();
            bool result = reader.HasRows;
            reader.Close();
            connection.Close();
            return result;
        }

        public bool DeleteUser(string username)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(string.Format("DELETE FROM users WHERE username = \"{0}\";", username), connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModifyUser(string username, string password, int do_edit, int is_admin)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                connection.Open();
                SQLiteCommand command;
                if (password == "")
                {
                    command = new SQLiteCommand(string.Format("UPDATE users SET do_edit = \"{0}\", is_admin = \"{1}\" WHERE username = \"{2}\"", do_edit, is_admin, username), connection);
                }
                else
                {
                    command = new SQLiteCommand(string.Format("UPDATE users SET do_edit = \"{0}\", is_admin = \"{1}\", password = \"{2}\" WHERE username = \"{3}\"", do_edit, is_admin, password, username), connection);
                }
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
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
            connection.Close();
            return data;
        }

        public bool SetCategory(string name)
        {
            try
            {
                List<Category> list = GetCategories();
                foreach (Category cat in list)
                {
                    if (cat.Name.ToLower() == name.ToLower())
                    {
                        return false;
                    }
                }
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DataBaseName));
                SQLiteCommand command = new SQLiteCommand(string.Format("INSERT INTO categories(name_category) VALUES(\"{0}\");", name), connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(string name)
        {
            try 
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
                return true;
            }
            catch
            {
                return false;
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
