using System.Collections.Generic;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public class ResponseResult<T>
    {
        public readonly T Value = default!;
        public readonly List<string> Errors;
        public readonly string ErrorTitle;
        public ResponseResult(T value) => this.Value = value;
        public ResponseResult(string title = null, List<string> errors = null)
        {
            this.ErrorTitle = title;
            this.Errors = errors;
        }
        public bool HasValue => Errors.Count != 0 || !string.IsNullOrEmpty(ErrorTitle);
        public static implicit operator bool(ResponseResult<T> result) => result.HasValue;
    }
}
