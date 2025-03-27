using AutoMapper;
using Kursregristrering.DTOs;
using Kursregristrering.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Kursregristrering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // Exempel på hur du mappar en User-entitet till en UserDTO

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var userEntity = await _userService.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDTO>(userEntity);
            return Ok(userDto);
        }

    }
}
