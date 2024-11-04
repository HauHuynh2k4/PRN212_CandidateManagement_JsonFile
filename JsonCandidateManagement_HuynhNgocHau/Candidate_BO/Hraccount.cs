using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Candidate_BO
{
    [XmlRoot("Users")]
    public class HraccountList
    {
        [XmlElement("User")]
        public List<Hraccount> Users { get; set; }
    }

    public class Hraccount
    {
        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        [XmlElement("FullName")]
        public string FullName { get; set; }

        [XmlElement("MemberRole")]
        public int MemberRole { get; set; }
    }
}
