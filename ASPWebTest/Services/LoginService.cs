using ASPWebTest.Models;
using ASPWebTest.ViewModels;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Transactions;

namespace ASPWebTest.Services
{
    public class LoginService: BaseService
    {
        private readonly ApplicationDbContext db;
        public LoginService(ApplicationDbContext db) {
            this.db = db; 
        }
        public int CekExist(RegisterViewModel model)
        {
            IQueryable<Account> data = db.Accounts.AsQueryable();
            if (data.Any())
            {
                data = data.Where(x => x.AccountName == model.AccountName);
            }
            else
            {
                return 0;
            }
            return data.Count() == 0 ? 0 : 1;
        }

        public async Task<LoginOperation> Register(RegisterViewModel viewModel)
        {
            try 
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    string passwordHash = sha256_hash(viewModel.Password);

                    var account = new Account()
                    {
                        AccountName = viewModel.AccountName,
                        Password = passwordHash,
                        CreatedDate = DateTime.Now,
                        CreatedUser =1
                    };

                    db.Accounts.Add(account);
                    var success = await db.SaveChangesAsync() > 0;
                    if (success)
                    {
                        var userAccount = new UserAccount()
                        {
                            AccountId = account.Id,
                            CreatedDate = DateTime.Now,
                            CreatedUser = account.Id,
                        };

                        db.UserAccounts.Add(userAccount);
                        await db.SaveChangesAsync();

                        foreach (var item in viewModel.RoleIds)
                        {
                            var accountRoles = new AccountRole()
                            {
                                AccountId = account.Id,
                                RoleId = item,
                                IsActive = true,
                                CreatedDate = DateTime.Now,
                                CreatedUser = account.Id,
                            };
                            db.AccountRoles.Add(accountRoles);
                            await db.SaveChangesAsync();
                        }


                        scope.Complete();
                        return LoginOperation.Success(account.Id, userAccount.Id, account.AccountName);
                    }

                    return LoginOperation.Failed();
                }
            }
            catch(Exception ex) 
            {
                return LoginOperation.Failed(ex.ToString());
            }
        }
        public async Task<LoginOperation> Login(LoginViewModel viewModel)
        {
            if (viewModel != null) 
            {
                var password = sha256_hash(viewModel.Password);
                var login = (from T0 in db.Accounts.Where(x => x.AccountName == viewModel.AccountName && x.Password == password)
                             join T1 in db.UserAccounts on T0.Id equals T1.AccountId
                             select new UserLoginViewModel()
                             {
                                 AccountId = T1.AccountId,
                                 UserAccountId = T1.Id,
                                 AccountName = T0.AccountName
                             }
                ).FirstOrDefault();
                if(login != null)
                {
                    return LoginOperation.Success(login.AccountId, login.UserAccountId, login.AccountName);
                }
            }

            return LoginOperation.Failed("Account Name Empty");
        }


        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
