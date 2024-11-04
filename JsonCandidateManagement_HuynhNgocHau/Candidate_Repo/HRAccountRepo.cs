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
	public class HRAccountRepo : IHRAccountRepo
	{
        public bool AddHRAccount(Hraccount newAccount) => HRAccountDAO.Instance.AddHRAccount(newAccount);

        public bool DeleteHRAccount(string Email) => HRAccountDAO.Instance.DeleteHRAccount(Email);

        public Hraccount GetHraccountByEmail(string email) => HRAccountDAO.Instance.GetHraccountByEmail(email);

		public ArrayList GetHraccounts() => HRAccountDAO.Instance.LoadHRAccounts();
		public ArrayList GetHraccountsByNameOrRole(string? Name, int? Role) => HRAccountDAO.Instance.GetHraccountsByNameOrRole(Name, Role);

        public bool UpdateHRAccount(Hraccount account) => HRAccountDAO.Instance.UpdateHRAccount(account);
    }
}
