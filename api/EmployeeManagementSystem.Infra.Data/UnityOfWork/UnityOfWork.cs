using Microsoft.AspNetCore.Mvc.Filters;
using EmployeeManagementSystem.Infra.Data.Contexts;

namespace EmployeeManagementSystem.Infra.Data.UnityOfWork
{
    public class UnitOfWork : IAsyncActionFilter, IUnityOfWork
    {
        private readonly EmployeeManagementSystemContext _context;

        public UnitOfWork(EmployeeManagementSystemContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var result = await next();
                if ((result.Exception == null || result.ExceptionHandled) && _context.ChangeTracker.HasChanges())
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
