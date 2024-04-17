using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exo.WebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Exo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [ApiController]
    public class UsuarioControllers : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioControllers(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }
        [HttpGet("id")]
        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(id);
            if(usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        public IActionResult Post(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email,usuario.Senha);
            if(usuarioBuscado == null)
            {
                return NotFound("Email ou Senha Inválidos!");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.id.ToString()),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoder.UTF8.GetBytes("exoapi-chave-auteticacao"));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: "exoapi.webapi",
                audience: "exoapi.webapi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return Ok (
                new {token = new JwtSecurityTokenHandler().WriteToken(token)}
            );
        }
        [Authorize]
        [HttpPut("id")]
         public IActionResult Atualizar(int id, Usuario usuario)
         {
             _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
         }

        [Authorize]
        [HttpDelete("id")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}