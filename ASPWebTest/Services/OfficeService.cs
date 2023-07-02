using ASPWebTest.Models;
using ASPWebTest.ViewModels;
using System.Transactions;

namespace ASPWebTest.Services
{
    public class OfficeService : BaseService
    {
        private readonly ApplicationDbContext db;
        public OfficeService(ApplicationDbContext db) { 
            this.db = db; 
        }

        public List<OfficeViewModel> GetAll(string searchText = null)
        {
            IQueryable<Office> query = db.Offices.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.OfficeCode.Contains(searchText)
                || x.OfficeName.Contains(searchText)
                );
            }
            var ret = (from T0 in query select new OfficeViewModel() { 
                Id = T0.Id,
                OfficeCode = T0.OfficeCode,
                OfficeName = T0.OfficeName,
                Address1 = T0.Address1,
                Address2 = T0.Address2,
                City = T0.City,
                Phone = T0.Phone,
                Fax = T0.Fax,
            }).ToList();

            return ret;
        }
        public Office GetById(int id)
        {
            var query = db.Offices.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                return new Office();
            }

            return query;
        }
        public async Task<OperationResult> Add(OfficeViewModel viewModel)
        {
            try 
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                )) 
                {
                    var data = new Office()
                    {
                        OfficeCode = viewModel.OfficeCode,
                        OfficeName = viewModel.OfficeName,
                        Address1 = viewModel.Address1,
                        Address2 = viewModel.Address2,

                        City = viewModel.City,
                        Phone = viewModel.Phone,
                        Fax = viewModel.Fax,

                        CreatedDate = DateTime.Now,
                        CreatedUser = viewModel.CreatedUser,
                    };
                    db.Offices.Add(data);
                    var success = await db.SaveChangesAsync() > 0;
                    if (success)
                    {
                        scope.Complete();
                        return OperationResult.Success();
                    }

                    return OperationResult.Failed(); 
                }
            }
            catch(Exception ex) 
            {
                return OperationResult.Failed(ex.ToString());
            }
        }

        public async Task<OperationResult> Update(OfficeViewModel viewModel)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = db.Offices.Find(viewModel.Id);
                    if(data != null)
                    {
                        data.Address1 = viewModel.Address1;
                        data.Address2 = viewModel.Address2; 
                        data.City = viewModel.City;
                        data.Phone = viewModel.Phone;
                        data.Fax = viewModel.Fax;

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
                    var data = db.Offices.Find(id);
                    if(data != null)
                    {
                        db.Offices.Remove(data);
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
            catch(Exception ex)
            {
                return OperationResult.Failed(ex.ToString());
            }
        }
    }
}
