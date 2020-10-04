using System.Collections.Generic;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public class ResponseError
    {
        public string Title { get; set; }
        public Dictionary<string, IList<string>> Errors { get; set; }
    }
}
