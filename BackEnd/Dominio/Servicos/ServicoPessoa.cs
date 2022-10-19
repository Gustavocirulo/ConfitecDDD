using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoPessoa : IServicoPessoa
    {

        private readonly IPessoa _IPessoa;

        public ServicoPessoa(IPessoa IPessoa)
        {
            _IPessoa = IPessoa;
        }

         public async Task AdicionarPessoa(Pessoa pessoa)
        {
            var validarNome = pessoa.ValidarPropriedadeString(pessoa.Nome, "Titulo");
            var validarSobrenome = pessoa.ValidarPropriedadeString(pessoa.Sobrenome, "Sobrenome");
            var validarEmail = pessoa.ValidarPropriedadeString(pessoa.Email, "Email") && pessoa.ValidarPropriedadeEmail(pessoa.Email, "Email");

            if (validarNome && validarSobrenome && validarEmail)
            {
                pessoa.DataAlteracao = DateTime.Now;
                pessoa.DataCadastro = DateTime.Now;
                pessoa.Ativo = true;
                await _IPessoa.Adicionar(pessoa);
            }

        }

        public async Task AtualizarPessoa(Pessoa pessoa)
        {
            var validarNome = pessoa.ValidarPropriedadeString(pessoa.Nome, "Titulo");
            var validarSobrenome = pessoa.ValidarPropriedadeString(pessoa.Sobrenome, "Sobrenome");
            var validarEmail = pessoa.ValidarPropriedadeString(pessoa.Email, "Email") && pessoa.ValidarPropriedadeEmail(pessoa.Email, "Email");

            if (validarNome && validarSobrenome && validarEmail)
            {
                pessoa.DataAlteracao = DateTime.Now;
                pessoa.Ativo = true;
                await _IPessoa.Atualizar(pessoa);
            }
        }

        public async Task<List<Pessoa>> ListarPessoasAtivas()
        {
            return await _IPessoa.ListarPessoas(x => x.Ativo);
        }
    }
}
