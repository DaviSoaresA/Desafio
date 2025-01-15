using Microsoft.AspNetCore.Mvc;
using SGFP.Application.DTOs;
using SGFP.Application.Services;
using SGFP.Domain.Services;

namespace SGFP.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public LoginController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var usuario = await _userService.Authenticate(loginDTO.Email, loginDTO.Password);
                if (usuario == null)
                {
                    return Unauthorized(new { Message = "Dados pessoais inválidas!" });
                }
                var token = _jwtService.GenerateJwtToken(usuario.Id, usuario.Email, usuario.Name);

                return Ok(new { Token = token, Message = "Login feito com sucesso!", Expiration = DateTime.UtcNow.AddHours(24) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao realizar login: {ex.Message}");
            }
        }
    }
}
