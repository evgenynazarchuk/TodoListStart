using System.Collections.Generic;

namespace TodoListStart.Application.Controllers
{
    public class ModelErrorResult
    {
        public ModelErrorResult()
        {
            Errors = new Dictionary<string, List<string>>();
        }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
