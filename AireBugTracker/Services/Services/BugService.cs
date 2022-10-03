using DatabaseContext.Models;
using Repositories.Interfaces;
using Repositories.Respositories;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BugService : ServiceBase<Bug>, IBugService
    {
        public BugService(IBugRepository repository) : base(repository)
        {
        }

        public override async Task<ServiceResult<Bug>> UpdateAsync(Bug entity)
        {
            try
            {
                var updatedEntity = await Repository.UpdateAsync(entity);

                return new ServiceResult<Bug>
                {
                    Status = HttpStatusCode.OK,
                    Target = updatedEntity,
                    Message = $"Bug: \"{entity.Id}\" successfully updated"
                };
            }
            catch (DbUpdateException)
            {
                // Should be impossible to reach with current requirments, "IsOpen" is the only field a user can edit
                return new ServiceResult<Bug>
                {
                    Status = HttpStatusCode.Conflict,
                    Message = $"Failed to update bug: \"{entity.Id}\", the \"Title\" conflicted with another bug"
                };
            }
        }
    }
}
