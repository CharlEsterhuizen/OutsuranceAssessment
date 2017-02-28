using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assesment;
using System.IO;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCreateTestData()
        {
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(exepath);
            string inputFilename = path + "\\OutNames.csv";

            CreateDataSet ds = new CreateDataSet(inputFilename);
            ds.WriteNameList();
            ds.WriteAddressList();
        }

        [TestMethod]
        public void TestAddress()
        {
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(exepath);

            string outFile = path + "\\Address.txt";
            List<string> expectedData = GetAddressData();
            if (File.Exists(outFile))
            {
                List<string> exportData = new List<string>();
                using (StreamReader streamReader = new StreamReader(outFile))
                {
                    while (!streamReader.EndOfStream)
                    {
                        exportData.Add(streamReader.ReadLine());
                    }
                }
                if (expectedData.Count == exportData.Count)
                {
                    for (int i = 0; i < expectedData.Count; i++)
                    {
                        if (exportData[i] != expectedData[i])
                        {
                            Assert.Fail("Record {0} does not match is {1} should be {2}", i, exportData[i], expectedData[i]);
                            return;
                        }
                    }
                }
                else
                {
                    Assert.Fail("Record Count Don't Match");
                }
            }
            else
            {
                Assert.Fail("Output file not Found");
            }
        }

        private List<string> GetAddressData()
        {
            List<string> Retval = new List<string>();
            Retval.Add("12 Acton St");
            Retval.Add("31 Clifton rd");
            Retval.Add("22 Jones rd");
            Retval.Add("17 Smith st");
            return Retval;
        }


        [TestMethod]
        public void TestNameFrequency()
        {
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(exepath);

            string outFile = path + "\\Frequency.txt";
            List<string> expectedData = GetNameFrequencyData();
            if (File.Exists(outFile))
            {
                List<string> exportData = new List<string>();
                using (StreamReader streamReader = new StreamReader(outFile))
                {
                    while (!streamReader.EndOfStream)
                    {
                        exportData.Add(streamReader.ReadLine());
                    }
                }
                if (expectedData.Count == exportData.Count)
                {
                    for (int i = 0; i < expectedData.Count; i++)
                    {
                        if(exportData[i] != expectedData[i])
                        {
                            Assert.Fail("Record {0} does not match is {1} should be {2}",i, exportData[i], expectedData[i]);
                            return;
                        }
                    }
                }
                else
                {
                    Assert.Fail("Record Count Don't Match");
                }
            }
            else
            {
                Assert.Fail("Output file not Found");
            }
        }

        private List<string> GetNameFrequencyData()
        {
            List<string> Retval = new List<string>();
            Retval.Add("Johnson,2");
            Retval.Add("Brown,1");
            Retval.Add("Heinrich,1");
            Retval.Add("Jones,1");
            Retval.Add("Matt,1");
            Retval.Add("Smith,1");
            Retval.Add("Tim,1");
            return Retval;
        }
    }
}
