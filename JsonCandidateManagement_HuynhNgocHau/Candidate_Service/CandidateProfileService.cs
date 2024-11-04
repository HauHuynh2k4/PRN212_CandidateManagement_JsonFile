using Candidate_BO;
using Candidate_Repository;
using System;
using System.Collections;

namespace Candidate_Service
{
    public class CandidateProfileService : ICandidateProfileService
    {
        private ICandidateProfileRepo repo;

        public CandidateProfileService()
        {
            repo = new CandidateProfileRepo();
        }

        public ArrayList GetAllCandidateProfiles() => repo.GetCandidateProfiles();

        public bool AddCandidate(CandidateProfile candidate) =>  repo.AddCandidateProfile(candidate);

        public bool UpdateCandidate(CandidateProfile candidate) => repo.UpdateCandidateProfile(candidate);

        public bool DeleteCandidate(string candidateId) => repo.DeleteCandidateProfile(candidateId);

        public CandidateProfile GetCandidateProfileById(string id) => repo.GetCandidateProfileById(id);
        public ArrayList GetCandidateProfileByNameOrDateTime(string? Name, DateTime? Birthday) => repo.GetCandidateProfileByNameOrDateTime(Name, Birthday);
    }
}
