using ASPWebTest.Models;
using ASPWebTest.ViewModels;
using System.Transactions;

namespace ASPWebTest.Services
{
    public class UserAccountService : BaseService
    {
        private readonly ApplicationDbContext db;
        public UserAccountService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<UserAccount> GetAll(string searchText = null)
        {
            IQueryable<UserAccount> query = db.UserAccounts.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.FirstName.Contains(searchText)
                || x.LastName.Contains(searchText)
                || x.Address1.Contains(searchText)
                || x.Address2.Contains(searchText)
                || x.City.Contains(searchText)
                || x.Email.Contains(searchText)
                || x.CreatedDate.ToString().Contains(searchText)
                );
            }

            return query.ToList();
        }

        public List<UsersViewModel> GetUsersViewModel(string searchText = null)
        {
            //IQueryable<UserAccount> query = db.UserAccounts.AsQueryable();
            var query = (from T0 in db.UserAccounts
                         join T1 in db.Offices on T0.OfficeId equals T1.Id
                         select new UsersViewModel()
                         {
                             Id = T0.Id,
                             AccountId = T0.AccountId,
                             FirstName = T0.FirstName,
                             MidleName = T0.MidleName,
                             LastName = T0.LastName,
                             Email = T0.Email,
                             Address1 = T0.Address1,
                             Address2 = T0.Address2,
                             City = T0.City,
                             OfficeId = T0.OfficeId,
                             OfficeName = T1.OfficeName,

                         });
            if (!string.IsNullOrEmpty(searchText))
            {
                query = (query.Where(x => x.FirstName.Contains(searchText)
                || x.LastName.Contains(searchText)
                || x.Address1.Contains(searchText)
                || x.Address2.Contains(searchText)
                || x.City.Contains(searchText)
                || x.Email.Contains(searchText)
                || x.CreatedDate.ToString().Contains(searchText)
                )) ;
            }

            return query.ToList();
        }
        public UserAccount GetById(int id)
        {
            var query = db.UserAccounts.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                return new UserAccount();
            }

            return query;
        }


        public async Task<OperationResult> Add(UserAccount viewModel)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = new UserAccount()
                    {
                        FirstName = viewModel.FirstName,
                        MidleName = viewModel.MidleName,
                        LastName = viewModel.LastName,
                        Address1 = viewModel.Address1,
                        Address2 = viewModel.Address2,

                        City = viewModel.City,
                        Email = viewModel.Email,
                        OfficeId = viewModel.OfficeId,

                        CreatedDate = DateTime.Now,
                        CreatedUser = viewModel.CreatedUser,
                    };
                    db.UserAccounts.Add(data);
                    var success = await db.SaveChangesAsync() > 0;
                    if (success)
                    {
                        scope.Complete();
                        return OperationResult.Success();
                    }

                    return OperationResult.Failed();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.ToString());
            }
        }

        public async Task<OperationResult> Update(UserAccount viewModel)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = db.UserAccounts.Find(viewModel.Id);
                    if (data != null)
                    {
                        data.FirstName = viewModel.FirstName;
                        data.MidleName = viewModel.MidleName;
                        data.LastName = viewModel.LastName;
                        data.Address1 = viewModel.Address1;
                        data.Address2 = viewModel.Address2;

                        data.City = viewModel.City;
                        data.Email = viewModel.Email;
                        data.OfficeId = viewModel.OfficeId;

                        data.ModifiedDate = DateTime.Now;
                        data.ModifiedUser = viewModel.ModifiedUser;

                        var success = await db.SaveChangesAsync() > 0;
                        if (success)
                        {
                            scope.Complete();
                            return OperationResult.Success();
                        }
                    }
                    return OperationResult.Failed();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.ToString());
            }
        }

        public async Task<OperationResult> Delete(int id)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = db.UserAccounts.Find(id);
                    if (data != null)
                    {
                        db.UserAccounts.Remove(data);
                        var success = await db.SaveChangesAsync() > 0;
                        if (success)
                        {
                            scope.Complete();
                            return OperationResult.Success();
                        }
                    }
                    return OperationResult.Failed();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.ToString());
            }
        }

    }
}
