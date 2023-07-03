using ASPWebTest.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace ASPWebTest.Services
{
    public class RoleService : BaseService
    {
        private readonly ApplicationDbContext db;
        public RoleService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Role> GetAll(string searchText = null)
        {
            IQueryable<Role> query = db.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.RoleName.Contains(searchText)
                || x.RoleName.Contains(searchText)
                );
            }

            return query.ToList();
        }
        public List<SelectListItem> GetAllSelectList()
        {
            var firstId = db.Roles.Select(x => x.Id).FirstOrDefault();
            var ret = (from T0 in db.Roles
                       select new SelectListItem() { 
                       Value = T0.Id.ToString(),
                       Text = T0.RoleName,
                       Selected = (T0.Id == firstId)
                       });
            return ret.ToList();
        }
        public Role GetById(int id)
        {
            var query = db.Roles.Where(x => x.Id == id).FirstOrDefault();
            if (query == null)
            {
                return new Role();
            }

            return query;
        }

        public int GetFirst()
        {
            int query = db.Roles.Select(x=>x.Id).FirstOrDefault() ?? 0;
            return  query;
        }


        public int CekExist(Role model)
        {
            IQueryable<Role> data = db.Roles.AsQueryable();
            if (data.Any())
            {
                data = data.Where(x => x.RoleName == model.RoleName);
            }
            else
            {
                return 0;
            }
            return data.Count() == 0 ? 0 : 1;
        }

        public async Task<OperationResult> Add(Role viewModel)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = new Role()
                    {
                        RoleName = viewModel.RoleName,

                        CreatedDate = DateTime.Now,
                        CreatedUser = viewModel.CreatedUser,
                    };
                    db.Roles.Add(data);
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

        public async Task<OperationResult> Update(Role viewModel)
        {
            try
            {
                using (var scope = new TransactionScope(
                    TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(60),
                    TransactionScopeAsyncFlowOption.Enabled
                ))
                {
                    var data = db.Roles.Find(viewModel.Id);
                    if (data != null)
                    {
                        data.RoleName = viewModel.RoleName;

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
                    var data = db.Roles.Find(id);
                    if (data != null)
                    {
                        db.Roles.Remove(data);
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
