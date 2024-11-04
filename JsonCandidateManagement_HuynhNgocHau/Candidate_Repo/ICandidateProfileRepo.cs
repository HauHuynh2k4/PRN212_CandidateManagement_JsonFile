using Candidate_BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate_Repository
{
	public interface ICandidateProfileRepo
	{
		public CandidateProfile GetCandidateProfileById(string id);
		public ArrayList GetCandidateProfiles();
		public bool AddCandidateProfile(CandidateProfile candidateProfile);
		public bool DeleteCandidateProfile(string id);
		public bool UpdateCandidateProfile(CandidateProfile candidate);

        public ArrayList GetCandidateProfileByNameOrDateTime(string? Name, DateTime? Birthday);
	}
}
