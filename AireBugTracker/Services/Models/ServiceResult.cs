using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    /// <summary>
    /// Class <c>ServiceResult<typeparamref name="T"/></c> conveys basic operation succuss information back to a calling client
    /// </summary>
    public class ServiceResult<T> where T : class
    {
        public HttpStatusCode Status { get; set; }
        public T? Target { get; set; }
        public string? Message { get; set; }
    }
}
