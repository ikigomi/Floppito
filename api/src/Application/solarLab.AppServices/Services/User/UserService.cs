using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using solarLab.Contracts.Enums;
using solarLab.Contracts.User;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.User
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;


        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <inheritdoc />
        public async Task AddAdminsAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                var result = await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }

        /// <inheritdoc />
        public async Task DeleteAdminsAsync(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                var result = await _userManager.RemoveFromRoleAsync(user, Roles.Admin.ToString());
            }

        }

        /// <inheritdoc />
        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var user =  await _userManager.FindByIdAsync(id.ToString());
            user.Roles = await _userManager.GetRolesAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc />
        public async Task<UserDto> GetCurrentUserAsync()
        {
            var user =  await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            user.Roles = await _userManager.GetRolesAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc />
        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return users.Count > 0 ? _mapper.Map<List<UserDto>>(users) : new List<UserDto>();
        }
    }
}
