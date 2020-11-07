using System.Collections.Generic;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public class RequestResult<T>
    {
        public readonly T Value = default!;
        public readonly List<string> Errors = new List<string>();
        public RequestResult(T value) => this.Value = value;
        public RequestResult()
        {
        }
        public bool HasValue => Errors.Count == 0;
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
