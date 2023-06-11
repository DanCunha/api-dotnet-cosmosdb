using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChallengeAPI.Filters
{
    public class ChallengeActionFilter : IActionFilter
    {
        private Stopwatch _stopwatch;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            Console.WriteLine("OnActionExecuting");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var duracaoDaAction = _stopwatch.ElapsedMilliseconds;
            _stopwatch.Stop();
            Console.WriteLine($"Duration of execution: {duracaoDaAction}");
        }
    }
}
