using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IDateTimeService _datetimeService;
        private readonly IUserService _userService;
        public Repository(
            AppDbContext dbContext,
            IDateTimeService datetimeServce,
            IUserService userService)
        {
            _dbContext = dbContext;
            _datetimeService = datetimeServce;
            _userService = userService;
        }
        public virtual IQueryable<TModel> Read<TModel>()
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            var entities = _dbContext.Set<TModel>()
                .Where(e => e.CreatedBy == _userService.Email)
                .AsNoTracking().AsQueryable();
            return entities;
        }
        public virtual async Task<TModel> Find<TModel>(int id)
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            var entity = await Read<TModel>().SingleOrDefaultAsync(x => x.Id == id && x.CreatedBy == _userService.Email);
            return entity;
        }
        public virtual async Task<bool> IsExist<TModel>(int id)
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            var result = await Read<TModel>().AnyAsync(x => x.Id == id && x.CreatedBy == _userService.Email);
            return result;
        }
        public virtual async Task<TModel> Add<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            if (string.IsNullOrEmpty(_userService.Email))
            {
                throw new ApplicationException("User Email is Empty");
            }

            if (entity is IDateTimeAudit)
            {
                var entityAudit = entity as IDateTimeAudit;
                entityAudit.CreatedDate = _datetimeService.Now;
            }
            if (entity is IAuthAudit)
            {
                var entityAudit = entity as IAuthAudit;
                entityAudit.CreatedBy = _userService.Email;
            }

            await _dbContext.Set<TModel>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task Update<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            var entityModel = await Find<TModel>(entity.Id);

            if (entity is IDateTimeAudit)
            {   
                var entityAudit = entity as IDateTimeAudit;
                entityAudit.ModifiedDate = _datetimeService.Now;
                entityAudit.CreatedDate = (entityModel as IDateTimeAudit).CreatedDate;
            }
            if (entity is IAuthAudit)
            {
                var entityAudit = entity as IAuthAudit;
                entityAudit.ModifiedBy = _userService.Email;
                entityAudit.CreatedBy = (entityModel as IAuthAudit).CreatedBy;
            }

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task Remove<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, IAuthAudit, new()
        {
            if (entity.CreatedBy == _userService.Email)
            {
                _dbContext.Set<TModel>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
