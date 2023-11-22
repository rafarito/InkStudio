using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InkStudio.Models.Services
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRolesAsync();
        Task SeedUsersAsync();
    }
}