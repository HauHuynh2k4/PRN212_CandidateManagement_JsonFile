using Candidate_BO;
using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Candidate_DAO
{
    public class HRAccountDAO
    {
        private readonly string _dataPath;
        private static HRAccountDAO instance;
        public HRAccountDAO()
        {
            _dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData");
        }
        public static HRAccountDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new HRAccountDAO(); 
                }
                return instance;
            }
        }
        public ArrayList LoadHRAccounts()
        {
            ArrayList hrAccounts = new ArrayList();
            string filePath = Path.Combine(_dataPath, "HRAccount.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Cannot find file: {filePath}");
            }

            string jsonString = File.ReadAllText(filePath);
            var accounts = JsonSerializer.Deserialize<HraccountList>(jsonString);
            
            if (accounts?.Users != null)
            {
                foreach (var account in accounts.Users)
                {
                    hrAccounts.Add(account);
                }
            }

            return hrAccounts;
        }

        public bool AddHRAccount(Hraccount account)
        {
            try
            {
                var accounts = LoadHRAccounts();
                accounts.Add(account);
                return SaveHRAccounts(accounts);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateHRAccount(Hraccount account)
        {
            try
            {
                var accounts = LoadHRAccounts();
                bool found = false;

                for (int i = 0; i < accounts.Count; i++)
                {
                    var existing = accounts[i] as Hraccount;
                    if (existing.Email == account.Email)
                    {
                        accounts[i] = account;
                        found = true;
                        break;
                    }
                }

                if (!found) return false;
                return SaveHRAccounts(accounts);
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteHRAccount(string email)
        {
            try
            {
                var accounts = LoadHRAccounts();
                bool found = false;

                for (int i = accounts.Count - 1; i >= 0; i--)
                {
                    var account = accounts[i] as Hraccount;
                    if (account.Email == email)
                    {
                        accounts.RemoveAt(i);
                        found = true;
                        break;
                    }
                }

                if (!found) return false;

                SaveHRAccounts(accounts);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool SaveHRAccounts(ArrayList accounts)
        {
            try
            {
                string filePath = Path.Combine(_dataPath, "HRAccount.json");
                var hrAccountList = new HraccountList
                {
                    Users = accounts.Cast<Hraccount>().ToList()
                };
                string jsonString = JsonSerializer.Serialize(hrAccountList);
                File.WriteAllText(filePath, jsonString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Hraccount GetHraccountByEmail(string email)
        {
            try
            {
                var accounts = LoadHRAccounts();
                foreach (Hraccount account in accounts)
                {
                    if (account.Email.Equals(email))
                    {
                        return account;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public ArrayList GetHraccountsByNameOrRole(string? Name, int? Role)
        {
            try
            {
                var allHrAccount = LoadHRAccounts();
                ArrayList result = new ArrayList();

                if (string.IsNullOrWhiteSpace(Name) && Role == null)
                {
                    return allHrAccount;
                }

                if (!string.IsNullOrWhiteSpace(Name) && Role == null)
                {
                    foreach (Hraccount hraccount in allHrAccount)
                    {
                        if (hraccount.FullName.Contains(Name, StringComparison.OrdinalIgnoreCase))
                        {
                            result.Add(hraccount);
                        }
                    }
                    return result;
                }

                if (string.IsNullOrWhiteSpace(Name) && Role != null)
                {
                    foreach (Hraccount hraccount in allHrAccount)
                    {
                        if (hraccount.MemberRole == Role)
                        {
                            result.Add(hraccount);
                        }
                    }
                    return result;
                }
                foreach (Hraccount hraccount in allHrAccount)
                {
                    if (hraccount.FullName.Contains(Name, StringComparison.OrdinalIgnoreCase) &&
                        hraccount.MemberRole == Role)
                    {
                        result.Add(hraccount);
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
