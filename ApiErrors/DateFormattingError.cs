
using System;
using System.Net;
namespace SenwesAssignment_API.ApiErrors
{
    public class DateFormattingError 
    {
        public string dateFormattingError(string inputString) 
        {
            //string inputString = "2000-02-02";
            DateTime dDate;

            if (DateTime.TryParse(inputString, out dDate))
            {
                var date = string.Format("{mm/dd/yyyy}", dDate);
                return date;
            }
            else
            {
                return "Invalid"; // <-- Control flow goes here
            }
        }

    }
}
