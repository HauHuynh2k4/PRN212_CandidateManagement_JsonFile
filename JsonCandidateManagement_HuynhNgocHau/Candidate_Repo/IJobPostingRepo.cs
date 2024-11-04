using Candidate_BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
	public interface IJobPostingRepo
	{
		public JobPosting GetJobPostingById(String id);
		public ArrayList LoadJobPostings();
		public bool AddJobPosting(JobPosting job);
		public bool UpdateJobPosting(JobPosting job);
		public bool DeleteJobPosting(string id);
		public ArrayList GetJobPostingByTitleOrPostDate(string? Title, DateTime? Date);
	}
}
