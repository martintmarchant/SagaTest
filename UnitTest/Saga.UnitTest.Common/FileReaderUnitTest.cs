using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Saga.UnitTest.Common
{
    [TestClass]
    public class FileReaderUnitTest
    {
        const string TestFilename = "testfile.csv";


        [TestMethod]
        public void CommonReaderTestReadFromFile()
        {
            var filename = GetTestFile();

            string[] TestFileContent = Saga.Common.File.Reader.ContentToStringArray(filename);

            // verify file has correct number of records
            Assert.AreNotEqual(TestFileContent.Length, 0);
            Assert.AreEqual(TestFileContent.Length, 5);
          
            // test file data
            Assert.IsTrue(TestFileContent[0].ToString().StartsWith("TransactionNumber"));
        }


        
        private string GetTestFile()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            while (!File.Exists(Path.Combine(path, TestFilename)))
            {
                path = Directory.GetParent(path).FullName;
                string fullFile = Path.Combine(path, TestFilename);

                if (Path.GetPathRoot(path)==path)
                {
                    Assert.Fail("Cannot test csv Reader. File TestFile.CSV Not Found");
                }
            }
            return Path.Combine(path, TestFilename);
        }
    }
}
