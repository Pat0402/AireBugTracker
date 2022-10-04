using DatabaseContext.Models;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBugService : IServiceBase<Bug, BugDTO> {
        Task<ServiceResult<List<Bug>>> GetOpenBugsAsync();
    }
}
