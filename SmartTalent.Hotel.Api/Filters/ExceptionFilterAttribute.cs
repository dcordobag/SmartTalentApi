namespace SmartTalent.Hotel.Api.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Diagnostics;


    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        public ExceptionFilterAttribute()
        {

        }

        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var baseException = context.Exception.GetBaseException();
            SendLogToEagle(baseException);
            base.OnException(context);
        }

        private static void SendLogToEagle(Exception baseException)
        {
            StackTrace trace = new(baseException, true);
            var frame = trace.GetFrame(0);

            string error = $"Date: {DateTime.Now.ToShortDateString()} - Error: {baseException.Message} - Method: {baseException.TargetSite?.Name} - StackTrace: {baseException.StackTrace} ";


            using StreamWriter writer = new("logs.txt", true);
            writer.WriteLine(error);
            writer.WriteLine("=".PadRight(50, '='));
        }
    }
}
