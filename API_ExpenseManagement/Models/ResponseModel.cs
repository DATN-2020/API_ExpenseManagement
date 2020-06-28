using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ExpenseManagement.Models
{
    public class ResponseModel
    {
        private string _message;
        private object _data;
        private string _status_code;

        public string Message { get => _message; set => _message = value; }
        public object Data { get => _data; set => _data = value; }
        public string Status_code { get => _status_code; set => _status_code = value; }

        public ResponseModel(string message, object data, string status_code )
        {
            this.Message = message;
            this.Data = data;
            this.Status_code = status_code;
        }
    }
}
