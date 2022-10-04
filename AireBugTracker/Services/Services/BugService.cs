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
    public class BugService : ServiceBase<Bug, BugDTO>, IBugService
    {
        public BugService(IBugRepository repository) : base(repository)
        {
        }

        public override async Task<ServiceResult<Bug>> CreateAsync(BugDTO dto)
        {
            var theBug = new Bug
            {
                IsOpen = true,
                Title = dto.Title,
                Details = dto.Details,
                OpenedDate = DateTimeOffset.UtcNow,
                UserId = dto.UserId
            };

            return await CreateAsync(theBug);
        }

        public async Task<ServiceResult<List<Bug>>> GetOpenBugsAsync()
        {
            var theBugs = await Repository.GetFiltered();

            return new ServiceResult<List<Bug>>
            {
                Status = HttpStatusCode.OK,
                Target = theBugs,
                Message = "Retrieved all bugs"
            };
        }

        public override async Task<ServiceResult<Bug>> UpdateAsync(int id, BugDTO bugDTO)
        {
            try
            {
                var theBug = new Bug
                {
                    Id = id,
                    Title = bugDTO.Title,
                    Details = bugDTO.Details,
                    UserId = bugDTO.UserId
                };

                var updatedEntity = await Repository.UpdateAsync(theBug);

                return new ServiceResult<Bug>
                {
                    Status = HttpStatusCode.OK,
                    Target = updatedEntity,
                    Message = $"Bug: \"{id}\" successfully updated"
                };
            }
            catch (DbUpdateException)
            {
                // Should be impossible to reach with current requirments, "IsOpen" is the only field a user can edit
                return new ServiceResult<Bug>
                {
                    Status = HttpStatusCode.Conflict,
                    Message = $"Failed to update bug: \"{id}\", the \"Title\" conflicted with another bug"
                };
            }
        }
    }
}
