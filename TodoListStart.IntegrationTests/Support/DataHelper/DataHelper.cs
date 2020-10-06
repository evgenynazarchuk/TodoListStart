using AutoMapper;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public DataHelper(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
