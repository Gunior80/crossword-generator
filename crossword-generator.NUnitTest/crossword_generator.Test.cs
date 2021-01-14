using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace crossword_generator.NUnitTest
{
    public class Tests
    {
        Database db;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void DatabaseExistTest()
        {
            db = new Database();
            if (db != null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CreareUserTest()
        {
            db = new Database();
            if (db.CreateUser("Test", "Test", 1, 1))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CreateCategoryTest()
        {
            db = new Database();
            if (db.SetCategory("TestCat"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DelCategoryTest()
        {
            //CreateCategoryTest();
            db = new Database();
            if (db.DeleteCategory("TestCat"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ModifyUserTest()
        {
            db = new Database();
            if (db.ModifyUser("Test", "Test", 0, 0))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetUsersTest()
        {
            db = new Database();
            if (db.GetUsers().Contains("admin") && db.GetUsers().Contains("Test"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DelUsersTest()
        {
            CreareUserTest();
            db = new Database();
            if (db.DeleteUser("Test"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CheckUsersTest()
        {
            CreareUserTest();
            db = new Database();
            if (db.CheckUser("Test", "Test"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GeneratorCreateTest()
        {
            Generator a = new Generator('-', new List<string> { "testword", "wordtest" });
            if (a != null)
            {
                Assert.Pass();   
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GeneratorWorkTest()
        {
            Generator a = new Generator('-', new List<string> { "testword", "wordtest" });
            a.generate_crossword(11);
            if (a.GetGrid is List<List<char>>)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

    }
}