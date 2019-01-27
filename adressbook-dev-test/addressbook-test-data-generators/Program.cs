using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = Convert.ToInt32(args[0]);
            var fileName = args[1];
            string format = args[2];
            string type = args[3];

            var groups = new List<GroupData>();
            var contacts = new List<ContactData>();

            for (var i = 0; i < count; i++)
            {
                if(type == "groups")
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }
                
                else if(type == "contacts")
                {
                    contacts.Add(new ContactData()
                    {
                        FirstName = TestBase.GenerateRandomString(10),
                        LastName = TestBase.GenerateRandomString(10)
                    });
                }
                else
                    Console.Out.Write("Unrecognized type " + type);
            }
            if (format == "excel")
            {
                WriteToExcelFile(groups, contacts, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);

                if (format == "csv")
                    WriteToCsvFile(groups, contacts, writer);
                else if (format == "xml")
                    WriteToXmlFile(groups, contacts, writer);
                else if (format == "json")
                    WriteToJsonFile(groups, contacts, writer);
                else
                    Console.Out.Write("Unrecognized format " + format);

                writer.Close();
            }
        }

        static void WriteToExcelFile(List<GroupData> groups, List<ContactData> contacts,string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;

            var wb = app.Workbooks.Add();

            var sheet = wb.ActiveSheet;

            int row = 1;
            foreach (var group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;

                row++;
            }

            foreach (var contact in contacts)
            {
                sheet.Cells[row, 1] = contact.FirstName;
                sheet.Cells[row, 2] = contact.LastName;

                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            File.Delete(fullPath);

            wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), fileName));
            wb.Close();

            app.Visible = false;

            app.Quit();
        }

        static void WriteToCsvFile(List<GroupData> groups, List<ContactData> contacts, StreamWriter writer)
        {
            foreach(var group in groups)
            {
                writer.WriteLine(string.Format("${0},${1},${2}",
                                    group.Name,
                                    group.Header,
                                    group.Footer));
            }

            foreach (var contact in contacts)
            {
                writer.WriteLine(string.Format("${0},${1}",
                                    contact.FirstName,
                                    contact.LastName));
            }
        }

        static void WriteToXmlFile(List<GroupData> groups, List<ContactData> contacts, StreamWriter writer)
        {
            if(groups.Count > 0)
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            if(contacts.Count > 0)
                new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);

        }

        static void WriteToJsonFile(List<GroupData> groups, List<ContactData> contacts, StreamWriter writer)
        {
            if(groups.Count > 0)
                writer.Write(JsonConvert.SerializeObject(groups, Formatting.Indented));
            if (contacts.Count > 0)
                writer.Write(JsonConvert.SerializeObject(contacts, Formatting.Indented));
        }
    }
}
