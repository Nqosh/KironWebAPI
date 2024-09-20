using AutoMapper;
using KironWebAPI.API.Dtos;
using KironWebAPI.Core.Entities;
using KironWebAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KironWebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IMapper mapper, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();
            if (await _authRepository.UserExist(userForRegisterDto.Email))
                return BadRequest("UserName already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);

            return new UserDto
            {
                DisplayName = createdUser.DisplayName,
                Token = _tokenService.CreateToken(userToCreate),
                Email = createdUser.Email,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn(UserForLogInDto userForLogInDto)
        {
            var userfromRepo = await _authRepository.Login(userForLogInDto.Email.ToLower(), userForLogInDto.Password);

            if (userfromRepo == null)
                return Unauthorized();

            return new UserDto
            {
                Email = userfromRepo.Email,
                Token = _tokenService.CreateToken(userfromRepo),
                DisplayName = userfromRepo.DisplayName
            };
        }
    }
}
