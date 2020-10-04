using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application.Services;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        private readonly Repository _repo;
        private readonly IMapper _mapper;
        public DataHelper(Repository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
