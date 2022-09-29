using DatabaseContext.Models;
using Repsoitories.Interfaces;
using Repsoitories.Respositories;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

        public override async Task<ServiceResult<Bug>> UpdateAsync(Bug entity)
        {
            try
            {
                var updatedEntity = await Repository.UpdateAsync(entity);

                return new ServiceResult<Bug>
                {
                    Target = updatedEntity,
                    IsSuccessful = true,
                    Message = $"Bug: \"{entity.Id}\" successfully updated"
                };
            }
            catch (DbUpdateException)
            {
                // Should be impossible to reach with current requirments, "IsOpen" is the only field a user can edit
                return new ServiceResult<Bug>
                {
                    IsSuccessful = false,
                    Message = $"Failed to update bug: \"{entity.Id}\", the \"Title\" conflicted with another bug"
                };
            }
        }
    }
}
