using Aplicacao.Interfaces;
using Aplicacao.ViewModel;
using Entidades.Entidades;
using Entidades.Enums;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IAplicacaoPessoa _IAplicacaoPessoa;

        public PessoasController(IAplicacaoPessoa IAplicacaoPessoa)
        {
            _IAplicacaoPessoa = IAplicacaoPessoa;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/ListarPessoas")]
        public async Task<List<PessoaModel>> ListarPessoas()
        {
            var _response = await _IAplicacaoPessoa.ListarPessoasAtivas();

            return _response.Select(x => new PessoaModel
            {
                Id = x.Id,
                DataNascimento = x.DataNascimento,
                Email = x.Email,
                Escolaridade = (int)x.Escolaridade,
                Nome = x.Nome,
                Sobrenome = x.Sobrenome
            }).ToList();

        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/AdicionarPessoa")]
        public async Task<List<Notifica>> AdicionarPessoa(PessoaModel pessoa)
        {
            var pessoaNova = new Pessoa();

            pessoaNova.Id = pessoa.Id;
            pessoaNova.Nome = pessoa.Nome;
            pessoaNova.Sobrenome = pessoa.Sobrenome;
            pessoaNova.Email = pessoa.Email;
            pessoaNova.DataNascimento = pessoa.DataNascimento;
            pessoaNova.Escolaridade = (TipoEscolaridade)pessoa.Escolaridade;

            await _IAplicacaoPessoa.AdicionarPessoa(pessoaNova);

            return pessoaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AtualizarPessoa")]
        public async Task<List<Notifica>> AtualizarPessoa(PessoaModel pessoa)
        {
            var pessoaNova = await _IAplicacaoPessoa.BuscarPorId(pessoa.Id);

            pessoaNova.Id = pessoa.Id;
            pessoaNova.Nome = pessoa.Nome;
            pessoaNova.Sobrenome = pessoa.Sobrenome;
            pessoaNova.Email = pessoa.Email;
            pessoaNova.DataNascimento = pessoa.DataNascimento;
            pessoaNova.Escolaridade = (TipoEscolaridade)pessoa.Escolaridade;

            await _IAplicacaoPessoa.AtualizarPessoa(pessoaNova);

            if (pessoaNova == null)
            {
                throw new Exception("Erro Ao Atualizar Usuário");
            }

            return pessoaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/ExcluirPessoa")]
        public async Task<List<Notifica>> ExcluirPessoa(PessoaModel pessoa)
        {
            var pessoaNova = await _IAplicacaoPessoa.BuscarPorId(pessoa.Id);

            pessoaNova.Id = pessoa.Id;
            pessoaNova.Nome = pessoa.Nome;
            pessoaNova.Sobrenome = pessoa.Sobrenome;
            pessoaNova.Email = pessoa.Email;
            pessoaNova.DataNascimento = pessoa.DataNascimento;
            pessoaNova.Escolaridade = pessoaNova.Escolaridade;

            await _IAplicacaoPessoa.Excluir(pessoaNova);

            return pessoaNova.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/BuscarPessoaPorId")]
        public async Task<Pessoa> BuscarPessoaPorId([FromQuery] int id)
        {
            var pessoaNova = await _IAplicacaoPessoa.BuscarPorId(id);

            return pessoaNova;
        }


        private Task<string> RetornaIdUsuarioLogado()
        {
            if(User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");

                if (idUsuario == null)
                    return Task.FromResult(String.Empty);

                return Task.FromResult(idUsuario.Value);
            } else
            {
                return Task.FromResult(String.Empty);
            }
        }

    }
}
