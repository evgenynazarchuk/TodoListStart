using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Buffers;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData;

namespace TodoListStart.Application.Controllers
{
    public class RestController<TModel, TValue> : Controller
        where TModel : class, IEntityIdentity, IAuthAudit, new()
        where TValue : class, IEntityIdentity, IAuthAudit, new()
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        public RestController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var entities = await _repo.Read<TModel>().ToListAsync();
            var entityValue = _mapper.Map<List<TModel>, List<TValue>>(entities);
            return Ok(entityValue);
        }
        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await _repo.Find<TModel>(id);
            if (entity != null)
            {
                var entityValue = _mapper.Map<TModel, TValue>(entity);
                return Ok(entityValue);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]TValue entitySource)
        {
            var entity = _mapper.Map<TValue, TModel>(entitySource);
            await _repo.Add(entity);
            var entityValue = _mapper.Map<TModel, TValue>(entity);
            return Ok(entityValue);
        }
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody]TValue entitySource)
        {
            var entityModel = _mapper.Map<TValue, TModel>(entitySource);
            await _repo.Update(entityModel);
            return Ok(entityModel);
        }
        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.Find<TModel>(id);
            if (entity != null)
            {
                await _repo.Remove(entity);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
