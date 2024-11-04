using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Candidate_BO
{
    [XmlRoot("Candidates")]
    public class CandidateProfiles
    {
        [XmlElement("Candidate")]
        public List<CandidateProfile> Candidates { get; set; }
    }

    public class CandidateProfile
    {
        [XmlElement("CandidateID")]
        public string CandidateID { get; set; }

        [XmlElement("Fullname")]
        public string Fullname { get; set; }

        [XmlElement("Birthday")]
        public DateTime Birthday { get; set; }

        [XmlElement("ProfileShortDescription")]
        public string ProfileShortDescription { get; set; }

        [XmlElement("ProfileURL")]
        public string ProfileURL { get; set; }

        [XmlElement("PostingID")]
        public string PostingID { get; set; }
    }
}
