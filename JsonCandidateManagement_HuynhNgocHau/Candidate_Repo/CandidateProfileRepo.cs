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
	public class CandidateProfileRepo : ICandidateProfileRepo
	{
		public bool AddCandidateProfile(CandidateProfile candidateProfile) => CandidateProfileDAO.Instance.AddCandidate(candidateProfile);

		public bool DeleteCandidateProfile(string id) => CandidateProfileDAO.Instance.DeleteCandidate(id);

		public CandidateProfile GetCandidateProfileById(string id) => CandidateProfileDAO.Instance.GetCandidateProfileById(id);

		public ArrayList GetCandidateProfiles() => CandidateProfileDAO.Instance.LoadCandidateProfiles();

		public bool UpdateCandidateProfile(CandidateProfile candidate) => CandidateProfileDAO.Instance.UpdateCandidate(candidate);

        public ArrayList GetCandidateProfileByNameOrDateTime(string? Name, DateTime? Birthday) => CandidateProfileDAO.Instance.GetCandidateProfileByNameOrDateTime(Name, Birthday);
	}
}
