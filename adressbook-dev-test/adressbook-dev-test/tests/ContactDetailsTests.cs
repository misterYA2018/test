using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDetailsTests : AuthTestBase
    {
        [Test]
        public void ContactDetailsTest()
        {
            ContactData fromDetails = app.Contacts.GetContactDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromForm.Details, fromDetails.Details);
        }
    }
} 