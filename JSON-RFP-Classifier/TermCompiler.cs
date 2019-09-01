using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JSON_RFP_Classifier
{
    class TermCompiler
    {
        // These lists will keep track of which elements have been written to the respective file -- ensures only unique entries in file
        public static List<string> AgencyList = new List<string>();
        public static List<string> OfficeList = new List<string>();
        public static List<string> NaicsList = new List<string>();
        public static List<string> SubjectList = new List<string>();
        public static List<string> SetAsideList = new List<string>();

        public void TermListCompiler(string Filename)
        {
            Console.WriteLine("Compiling the list of terms you ordered....");
            List<RFPData> FileEntries = JsonConvert.DeserializeObject<List<RFPData>>(File.ReadAllText(Filename));

           // WriteAgency(FileEntries);
           // WriteOffice(FileEntries);
           // WriteNaics(FileEntries);
           // WriteSubject(FileEntries);
           // WriteSetAside(FileEntries);
        }

        public void WriteAgency(List<RFPData> FileEntries)
        {
            foreach (var entry in FileEntries)
            {
                if (!AgencyList.Contains(entry.Agency))
                {
                    AgencyList.Add(entry.Agency);
                    File.AppendAllText("Terms/Agencies.txt", entry.Agency + "\r\n");
                }
            }
        }

        public void WriteOffice(List<RFPData> FileEntries)
        {
            foreach (var entry in FileEntries)
            {
                if (!OfficeList.Contains(entry.Office))
                {
                    OfficeList.Add(entry.Office);
                    File.AppendAllText("Terms/Offices.txt", entry.Office + "\r\n");
                }
            }
        }

        public void WriteNaics(List<RFPData> FileEntries)
        {
            foreach (var entry in FileEntries)
            {
                if (!AgencyList.Contains(entry.Naics))
                {
                    NaicsList.Add(entry.Naics);
                    File.AppendAllText("Terms/Naics.txt", entry.Naics + "\r\n");
                }
            }
        }

        public void WriteSubject(List<RFPData> FileEntries)
        {
            foreach (var entry in FileEntries)
            {
                if (!SubjectList.Contains(entry.Subject))
                {
                    AgencyList.Add(entry.Subject);
                    File.AppendAllText("Terms/Subjects.txt", entry.Subject + "\r\n");
                }
            }
        }

        public void WriteSetAside(List<RFPData> FileEntries)
        {
            foreach (var entry in FileEntries)
            {
                if (!SetAsideList.Contains(entry.SetAside))
                {
                    SetAsideList.Add(entry.SetAside);
                    File.AppendAllText("Terms/SetAsides.txt", entry.SetAside + "\r\n");
                }
            }
        }
    }
}
