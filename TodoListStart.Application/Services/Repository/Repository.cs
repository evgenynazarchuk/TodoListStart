using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services.Repository
{
    public partial class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IDateTimeService _datetimeService;
        public Repository(AppDbContext dbContext, IDateTimeService datetimeServce)
        {
            _dbContext = dbContext;
            _datetimeService = datetimeServce;
        }
        public virtual IQueryable<TModel> Read<TModel>()
            where TModel : class, IEntityIdentity, new()
        {
            var entities = _dbContext.Set<TModel>().AsNoTracking().AsQueryable();
            return entities;
        }
        public virtual async Task<TModel> Find<TModel>(int id)
            where TModel : class, IEntityIdentity, new()
        {
            var entity = await Read<TModel>().SingleOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        public virtual async Task<TModel> Add<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, new()
        {
            if (entity is IDateTimeAudit)
            {
                var entityAudit = entity as IDateTimeAudit;
                entityAudit.CreatedDate = _datetimeService.Now;
            }
            await _dbContext.Set<TModel>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public virtual async Task Update<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, new()
        {
            if (entity is IDateTimeAudit)
            {
                var entityModel = await Find<TModel>(entity.Id);
                var entityAudit = entity as IDateTimeAudit;
                entityAudit.ModifiedDate = _datetimeService.Now;
                entityAudit.CreatedDate = (entityModel as IDateTimeAudit).CreatedDate;
            }
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task Remove<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, new()
        {
            _dbContext.Set<TModel>().Remove(entity);
            await _dbContext.SaveChangesAsync();

        }
        public virtual async Task<bool> IsExist<TModel>(int id)
            where TModel : class, IEntityIdentity, new()
        {

            var result = await Read<TModel>().AnyAsync(x => x.Id == id);
            return result;
        }
    }
}
