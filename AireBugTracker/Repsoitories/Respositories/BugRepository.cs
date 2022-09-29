using DatabaseContext;
using DatabaseContext.Models;
using Repsoitories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repsoitories.Respositories
{
    public class BugRepository : RepositoryBase<Bug>, IBugRepository
    {
        public BugRepository(BugTrackerContext context) : base(context)
        {
        }
    }
}
