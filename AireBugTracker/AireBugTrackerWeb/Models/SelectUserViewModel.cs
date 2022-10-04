using DatabaseContext.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AireBugTrackerWeb.Models
{
    public class SelectUserViewModel
    {
        public string Name { get; set; }
        public int? Id { get; set; }
    }
}
