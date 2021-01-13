using System;
using NUnit.Framework;

namespace crossword_generator.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void DatabaseTest()
        {
            Database db = new Database();
            if (db != null)
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