using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Candidate_BO
{
    [XmlRoot("JobPostings")]
    public class JobPostingList
    {
        [XmlElement("JobPosting")]
        public List<JobPosting> JobPostings { get; set; }
    }

    public class JobPosting
    {
        [XmlElement("PostingID")]
        public string PostingID { get; set; }

        [XmlElement("JobPostingTitle")]
        public string JobPostingTitle { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("PostedDate")]
        public DateTime PostedDate { get; set; }
    }
}
