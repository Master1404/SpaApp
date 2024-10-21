using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Responses
{
    public class Response<T> where T : class
    {
        /// <summary>
        /// Successful response
        /// </summary>
        /// <param name="responseValue"></param>
        public Response(T responseValue)
        {
            ResponseValue = responseValue;
            IsSuccessfull = true;
        }

        /// <summary>
        /// Failure response
        /// </summary>
        /// <param name="message"></param>
        public Response(string message)
        {
            IsSuccessfull = false;
            Message = message;
        }

        public T ResponseValue { get; set; }

        public bool IsSuccessfull { get; set; }

        public string? Message { get; set; } = null;
    }
}
