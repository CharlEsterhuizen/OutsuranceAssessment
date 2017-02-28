using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assesment
{
    public class CreateDataSet
    {
        private string inputFileName = "";
        private List<PersonData> NameList;
        public string outFolder = "";
        /// <summary>
        /// Create Class and read csv file into a list
        /// </summary>
        /// <param name="InputFileName"></param>
        public CreateDataSet(string InputFileName)
        {
            inputFileName = InputFileName;
            outFolder = System.IO.Path.GetDirectoryName(InputFileName);
            GetData();
        }

        public List<PersonData> GetData()
        {
            NameList = new List<PersonData>();
            using (var fStream = File.OpenRead(inputFileName))
            {
                using (var fileReader = new StreamReader(fStream))
                {
                    while (!fileReader.EndOfStream)
                    {
                        var line = fileReader.ReadLine();
                        var values = line.Split(',');
                        PersonData person = new PersonData()
                        {
                            FirstName = values[0],
                            LastName = values[1],
                            StreetName = values[2],
                            StreetNumber = Convert.ToInt32(values[3])
                        };
                        NameList.Add(person);
                    }
                }
            }

            return NameList;
        }

        public void WriteAddressList()
        {
            Dictionary<string, int> freq = GetAddress();
            string outFile = outFolder + "\\Address.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outFile, false))
            {
                foreach (KeyValuePair<string, int> keyValuePairString in freq)
                {
                    string outPut = String.Format("{0} {1}",  keyValuePairString.Value, keyValuePairString.Key);
                    file.WriteLine(outPut);
                }
            }
        }

        public void WriteNameList()
        {
            Dictionary<string, int> freq = GetFrequency();
            string outFile = outFolder + "\\Frequency.txt";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outFile, false))
            {
                foreach (KeyValuePair<string, int> keyValuePairString in freq)
                {
                    string outPut = String.Format("{0},{1}", keyValuePairString.Key, keyValuePairString.Value);
                    file.WriteLine(outPut);
                }
            }
        }

        public Dictionary<string, int> GetFrequency()
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();

            foreach (PersonData personData in NameList)
            {
                string firstName = personData.FirstName;
                string lastName = personData.LastName;
                if (freq.ContainsKey(firstName))
                {
                    freq[firstName] += 1;
                }
                else
                {
                    freq.Add(firstName, 1);
                }
                if (freq.ContainsKey(lastName))
                {
                    freq[lastName] += 1;
                }
                else
                {
                    freq.Add(lastName, 1);
                }
            }

            freq = freq.OrderByDescending(t => t.Value).ThenBy(t => t.Key).ToDictionary(x => x.Key, x => x.Value);

            return freq;
        }

        public Dictionary<string, int> GetAddress()
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();

            foreach (PersonData personData in NameList)
            {
                string streetName = personData.StreetName;
                int streetNumber = personData.StreetNumber;
                freq.Add(streetName, streetNumber);
            }

            freq = freq.OrderBy(t => t.Key).ToDictionary(x => x.Key, x => x.Value);

            return freq;
        }
    }

    public class PersonData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }
    }
}
