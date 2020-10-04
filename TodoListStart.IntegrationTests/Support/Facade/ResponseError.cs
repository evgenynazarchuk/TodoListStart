using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public class ResponseError
    {
        public string Title { get; set; }
        public Dictionary<string, IList<string>> Errors { get; set; }
    }
}
