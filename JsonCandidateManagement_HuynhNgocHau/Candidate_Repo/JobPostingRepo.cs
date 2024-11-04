using Candidate_BO;
using Candidate_DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
	public class JobPostingRepo : IJobPostingRepo
	{
		public JobPosting GetJobPostingById(string id) => JobPostingDAO.Instance.GetJobPostingById(id);

		public ArrayList LoadJobPostings() => JobPostingDAO.Instance.LoadJobPostings();
		public bool AddJobPosting(JobPosting job) => JobPostingDAO.Instance.AddJobPosting(job);
		public bool UpdateJobPosting(JobPosting job) => JobPostingDAO.Instance.UpdateJobPosting(job);
		public bool DeleteJobPosting(string id) => JobPostingDAO.Instance.DeleteJobPosting(id);
		public ArrayList GetJobPostingByTitleOrPostDate(string? Title, DateTime? Date) => JobPostingDAO.Instance.GetJobPostingByTitleOrPostDate(Title, Date);

    }
}
