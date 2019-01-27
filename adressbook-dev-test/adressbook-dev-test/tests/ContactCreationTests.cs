using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            var contacts = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30)));
            }

            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            var contacts = new List<ContactData>();

            var lines = File.ReadAllLines(@"contacts.csv");

            foreach (var l in lines)
            {
                var parts = l.Split(',');

                contacts.Add(new ContactData()
                {
                    FirstName = parts[0],
                    LastName = parts[1]
                });
            }

            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                   File.ReadAllText(@"contacts.json"));
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            var contacts = new List<ContactData>();

            var app = new Excel.Application();
            var wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlsx"));

            var sheet = wb.ActiveSheet;

            var range = sheet.UsedRange;

            for (var i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value,
                    LastName = range.Cells[i, 2].Value,
                });
            }

            wb.Close();

            app.Visible = false;
            app.Quit();

            return contacts;
        }
        [Test, TestCaseSource("ContactDataFromExcelFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.CountRowsInTable);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            newContacts.Sort();

            oldContacts.Add(contact);
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
} 