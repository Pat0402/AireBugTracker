using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    /// <summary>
    /// Class <c>ServiceResult<typeparamref name="T"/></c> conveys basic operation succuss information back to a calling client
    /// </summary>
    public class ServiceResult<T> where T : class
    {
        public T Target { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
