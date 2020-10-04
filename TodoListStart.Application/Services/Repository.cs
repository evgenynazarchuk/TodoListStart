﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Interfaces;


namespace TodoListStart.Application.Services
{
    public partial class Repository
    {
        private readonly AppDbContext _dbContext;
        private readonly IDateTimeService _datetimeService;
        public Repository(AppDbContext dbContext, IDateTimeService datetimeServce)
        {
            _dbContext = dbContext;
            _datetimeService = datetimeServce;
        }
        public async Task<List<TModel>> ReadAsync<TModel>()
            where TModel : class, new()
        {
            var entities = await _dbContext.Set<TModel>().AsNoTracking().ToListAsync();
            return entities;
        }
        public async Task<TModel> FindAsync<TModel>(int id)
            where TModel : class, new()
        {
            var entity = await _dbContext.Set<TModel>().FindAsync(id);
            return entity;
        }
        public async Task<TModel> AddAsync<TModel>(TModel entity)
            where TModel : class, new()
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
        public async Task UpdateAsync<TModel>(TModel entity)
            where TModel : class, new()
        {
            if (entity is IDateTimeAudit)
            {
                var entityModel = await _dbContext.FindAsync<TModel>((entity as IEntityIdentity).Id);
                _dbContext.Entry(entityModel).State = EntityState.Detached;
                var entityAudit = entity as IDateTimeAudit;
                entityAudit.ModifiedDate = _datetimeService.Now;
                entityAudit.CreatedDate = (entityModel as IDateTimeAudit).CreatedDate;
            }
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAsync<TModel>(TModel entity)
            where TModel : class, new()
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

        }
        public bool Exist<TModel>(int id)
            where TModel : class, new()
        {
            var result = _dbContext.Set<TModel>()
                .AsNoTracking()
                .ToList()
                .Any(x => (x as IEntityIdentity).Id == id);
            return result;
        }
        public DbSet<TModel> GetDbContext<TModel>()
            where TModel : class, new()
        {
            return _dbContext.Set<TModel>();
        }
    }
}
