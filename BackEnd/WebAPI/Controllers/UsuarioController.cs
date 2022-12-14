using Aplicacao.Interfaces;
using Aplicacao.ViewModel;
using Entidades.Entidades;
using Entidades.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IAplicacaoUsuario _IAplicacaoUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuarioController(
            IAplicacaoUsuario IAplicacaoUsuario,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager

            )
        {
            _IAplicacaoUsuario = IAplicacaoUsuario;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]
        public async Task<IActionResult> CriarToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Unauthorized();

            var resultado = await _IAplicacaoUsuario.ExisteUsuario(login.email, login.senha);

            if(resultado)
            {
                var idUsuario = await _IAplicacaoUsuario.RetornaIdUsuario(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Teste DDD Conhecimento")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(60)
                    .Builder();

                return Ok(token.value);

            } else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Ok("Usuario ou senha incompletos");
            }

            var resultado = await _IAplicacaoUsuario.AdicionarUsuario(login.email, login.senha, login.idade, login.celular);

            if(resultado) return Ok("Usuario Adicionado com Sucesso");
            else return Ok("Erro ao Adicionar usuário");
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarTokenIdentity")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Unauthorized();

            var resultado = await _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var idUsuario = await _IAplicacaoUsuario.RetornaIdUsuario(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Teste DDD Conhecimento")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste.Security.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(60)
                    .Builder();

                UsuarioViewModel _response = new UsuarioViewModel() { Id = idUsuario, Email = login.email, Nome = login.email.Split('@')[0], TokenJwt = token.value };

                return Ok(_response);
            }
            else
            {
                return Unauthorized();
            }

        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetUsuarioLogado")]
        public async Task<IActionResult> PegarUsuarioLogado()
        {
                return Ok(await _IAplicacaoUsuario.RetornaUsuarioPeloId(await RetornaIdUsuarioLogado())); 
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioIdentity")]
        public async Task<IActionResult> AdicionaUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Ok("Usuario ou senha incompletos");
            }

            var user = new ApplicationUser()
            {
                UserName = login.email,
                Email = login.email,
                Celular = login.celular,
                Tipo = TipoUsuario.Comum
            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if(resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);
            var StatusMessage = resultado2.Succeeded;

            if (resultado2.Succeeded) return Ok("Usuário Adicionado com Sucesso");
            else return Ok("Erro ao confirmar usuário");
        }

        private Task<string> RetornaIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");

                if (idUsuario == null)
                    return Task.FromResult(String.Empty);

                return Task.FromResult(idUsuario.Value);
            }
            else
            {
                return Task.FromResult(String.Empty);
            }
        }

    }


}
