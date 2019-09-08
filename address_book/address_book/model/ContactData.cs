using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "Firstname: " + Firstname + " Lastname: " + Lastname + "\nAddress: " + Address + "\nHomePhone:  " + Home + "\nEmail: " + Email;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }

            return Lastname.CompareTo(other.Lastname);
        }

        private string allPhones;

        private string allEmails;

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Lastname { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string Home { get; set; }

        public string Mobile { get; set; }

        public string Work { get; set; }

        public string Fax { get; set; }

        public string Home2 { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpMail(Email) + CleanUpMail(Email2) + CleanUpMail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work) + CleanUp(Home2)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        private string CleanUpMail(string mail)
        {
            if (mail == null || mail == "")
            {
                return "";
            }
            return mail.Replace(" ", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Homepage { get; set; }

        public string Bday { get; set; }

        public string Bmonth { get; set; }

        public string Byear { get; set; }

        public string Aday { get; set; }

        public string Amonth { get; set; }

        public string Ayear { get; set; }

        public string Address2 { get; set; }

        public string Phone2 { get; set; }

        public string Notes { get; set; }

        public string Id { get; set; }
    }
}
