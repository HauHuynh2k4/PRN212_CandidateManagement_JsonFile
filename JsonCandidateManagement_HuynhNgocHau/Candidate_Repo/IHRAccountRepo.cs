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
	public interface IHRAccountRepo
	{
		public Hraccount GetHraccountByEmail(string email);
		public ArrayList GetHraccounts();
		public ArrayList GetHraccountsByNameOrRole(string? Name, int? Role);
		public bool AddHRAccount(Hraccount newAccount);
		public bool DeleteHRAccount(string Email);
		public bool UpdateHRAccount(Hraccount account);

    }
}
