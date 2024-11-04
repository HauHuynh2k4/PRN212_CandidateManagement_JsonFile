using Candidate_BO;
using Candidate_DAO;
using Candidate_Repository;
using System;
using System.Collections;

namespace Candidate_Service
{
    public class JobPostingService : IJobPostingService
    {
        private readonly IJobPostingRepo repo;

        public JobPostingService()
        {
            repo = new JobPostingRepo();
        }

        public ArrayList GetAllJobPostings() => repo.LoadJobPostings();

        public bool AddJobPosting(JobPosting posting) => repo.AddJobPosting(posting);

        public bool UpdateJobPosting(JobPosting posting) => repo.UpdateJobPosting(posting);

        public bool DeleteJobPosting(string postingId) => repo.DeleteJobPosting(postingId);
        public JobPosting GetJobPostingById(string id) => repo.GetJobPostingById(id);
        public ArrayList GetJobPostingByTitleOrPostDate(string? Title, DateTime? Date) => repo.GetJobPostingByTitleOrPostDate(Title, Date);
    }
}
