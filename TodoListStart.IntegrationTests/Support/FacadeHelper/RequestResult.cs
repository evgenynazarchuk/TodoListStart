using System.Collections.Generic;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public class RequestResult<T>
    {
        public readonly T Value = default!;
        public readonly List<string> Errors = null;
        public RequestResult(T value) => this.Value = value;
        public RequestResult()
        {
            Errors = new List<string>();
        }
        public bool HasValue => Errors?.Count > 0;
        public RequestResult<T> AddErrors(List<string> errors)
        {
            Errors.AddRange(errors);
            return this;
        }
        public RequestResult<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}
