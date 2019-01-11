using System;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhone;
        private string details;

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData()
        {
        }

        public bool Equals(ContactData other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return (FirstName + LastName).GetHashCode();
        }

        public override string ToString()
        {
            return "firstName=" + FirstName + "lastName=" + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return (FirstName + LastName).CompareTo(other.FirstName + other.LastName);
        }

        public bool Equals(GroupData other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(GroupData other)
        {
            throw new NotImplementedException();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string AllPhone
        {
            get
            {
                if(allPhone != null)
                {
                    return allPhone;
                }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone).Trim(); 
                }
            }
            set
            {
                allPhone = value;
            }
        }

        public string Details
        {
            get
            {
                if (details != null)
                {
                    return details;
                }
                else
                {
                    var text = string.IsNullOrEmpty(FirstName) ? "" : $"{FirstName}";
                    text += string.IsNullOrEmpty(LastName) ? "" : $" {LastName}";
                    text += string.IsNullOrEmpty(Address) ? "" : $"{Address}";

                    text += string.IsNullOrEmpty(HomePhone) ? "" : $"H: {HomePhone}";
                    text += string.IsNullOrEmpty(MobilePhone) ? "" : $"M: {MobilePhone}";
                    text += string.IsNullOrEmpty(WorkPhone) ? "" : $"W: {WorkPhone}";

                    text += string.IsNullOrEmpty(Email) ? "" : $"{Email}";
                    text += string.IsNullOrEmpty(Email2) ? "" : $"{Email2}";
                    text += string.IsNullOrEmpty(Email3) ? "" : $"{Email3}";

                    return text;
                }
            }
            set
            {
                details = value;
            }
        }

        private string CleanUp(string phone)
        {
            if(phone == null || phone == "")
            {
                return "";
            }

            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string AllEmail { get; set; }



    }
}
