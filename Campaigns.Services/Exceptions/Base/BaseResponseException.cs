using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Services
{
    public class BaseResponseException : Exception
    {
        private int _statusCode;
        private string _message;

        public BaseResponseException(int statusCode, string message) : base(message)
        {
            _statusCode = statusCode;
            _message = message;
        }
        public string GetMessage()
        {
            return _message;
        }

        public int GetStatusCode()
        {
            return _statusCode;
        }
    }
}
