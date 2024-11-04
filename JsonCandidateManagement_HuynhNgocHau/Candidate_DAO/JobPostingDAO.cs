using Candidate_BO;
using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Candidate_DAO
{
    public class JobPostingDAO
    {
        private readonly string _dataPath;
        private static JobPostingDAO instance;
        public JobPostingDAO()
        {
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData");
        }
        public static JobPostingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JobPostingDAO();
                }
                return instance;
            }
        }
        public ArrayList LoadJobPostings()
        {
            ArrayList jobPostings = new ArrayList();
            string filePath = Path.Combine(_dataPath, "JobPosting.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Cannot find file: {filePath}");
            }

            string jsonString = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new DateTimeConverter() }
            };
            var postings = JsonSerializer.Deserialize<JobPostingList>(jsonString, options);
            
            if (postings?.JobPostings != null)
            {
                foreach (var posting in postings.JobPostings)
                {
                    jobPostings.Add(posting);
                }
            }

            return jobPostings;
        }

        public bool AddJobPosting(JobPosting posting)
        {
            try
            {
                var postings = LoadJobPostings();
                postings.Add(posting);
                return SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateJobPosting(JobPosting posting)
        {
            try
            {
                var postings = LoadJobPostings();
                bool found = false;

                for (int i = 0; i < postings.Count; i++)
                {
                    var existing = postings[i] as JobPosting;
                    if (existing.PostingID == posting.PostingID)
                    {
                        postings[i] = posting;
                        found = true;
                        break;
                    }
                }

                if (!found) return false;
                return SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }

        private bool SaveJobPostings(ArrayList postings)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "JobPosting.json");
                var jobPostingList = new JobPostingList
                {
                    JobPostings = postings.Cast<JobPosting>().ToList()
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new DateTimeConverter() }
                };
                string jsonString = JsonSerializer.Serialize(jobPostingList, options);
                File.WriteAllText(filePath, jsonString);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteJobPosting(string postingId)
        {
            try
            {
                var postings = LoadJobPostings();
                bool found = false;

                for (int i = postings.Count - 1; i >= 0; i--)
                {
                    var posting = postings[i] as JobPosting;
                    if (posting.PostingID == postingId)
                    {
                        postings.RemoveAt(i);
                        found = true;
                        break;
                    }
                }

                if (!found) return false;
                return SaveJobPostings(postings);
            }
            catch
            {
                return false;
            }
        }
        public JobPosting GetJobPostingById(string id)
        {
            try
            {
                var ListJobPosting = LoadJobPostings();
                foreach (JobPosting jobposting in ListJobPosting)
                {
                    if (jobposting.PostingID.Equals(id))
                    {
                        return jobposting;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public ArrayList GetJobPostingByTitleOrPostDate(string? Title, DateTime? Date)
        {
            try
            {
                var allJobPosting = LoadJobPostings();
                ArrayList result = new ArrayList();

                if (string.IsNullOrWhiteSpace(Title) && !Date.HasValue)
                {
                    return allJobPosting;
                }
                if (!string.IsNullOrWhiteSpace(Title) && !Date.HasValue)
                {
                    foreach (JobPosting job in allJobPosting)
                    {
                        if (job.JobPostingTitle.Contains(Title, StringComparison.OrdinalIgnoreCase))
                        {
                            result.Add(job);
                        }
                    }
                    return result;
                }
                if (string.IsNullOrWhiteSpace(Title) && Date.HasValue)
                {
                    foreach (JobPosting job in allJobPosting)
                    {
                        if (job.PostedDate == Date.Value.Date)
                        {
                            result.Add(job);
                        }
                    }
                    return result;
                }
                foreach (JobPosting job in allJobPosting)
                {
                    if (job.JobPostingTitle.Contains(Title, StringComparison.OrdinalIgnoreCase) &&
                        job.PostedDate == Date.Value.Date)
                    {
                        result.Add(job);
                    }
                }
                return result;
            }
            catch
            {
                return new ArrayList();
            }
        }
    }
}
