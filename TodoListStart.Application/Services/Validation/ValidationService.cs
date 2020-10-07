using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService : IValidationService
    {
        private readonly IRepository _repo;
        public ValidationService(IRepository repo)
        {
            _repo = repo;
        }
        public bool IsNull<TValue>(TValue entityValue)
        {
            return entityValue == null ? true : false;
        }
        public bool IsNullOrEmptyOrWhiteSpace(string field)
        {
            return string.IsNullOrEmpty(field) || string.IsNullOrWhiteSpace(field);
        }
    }
}
