using DatabaseContext.Models;
using Repsoitories.Interfaces;
using Repsoitories.Respositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BugService : ServiceBase<Bug>, IBugService
    {
        public BugService(IBugRepository repository) : base(repository)
        {
        }
    }
}
