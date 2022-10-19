using Aplicacao.Interfaces;
using Aplicacao.ViewModel;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoPessoa : IAplicacaoPessoa
    {
        IPessoa _IPessoa;
        IServicoPessoa _IServicoPessoa;
        public AplicacaoPessoa(IPessoa IPessoa, IServicoPessoa IServicoPessoa)
        {
            _IPessoa = IPessoa;
            _IServicoPessoa = IServicoPessoa;
        }
        public async Task AdicionarPessoa(Pessoa pessoa)
        {
            await _IServicoPessoa.AdicionarPessoa(pessoa);
        }

        public async Task AtualizarPessoa(Pessoa pessoa)
        {
            await _IServicoPessoa.AtualizarPessoa(pessoa);
        }

        public async Task<List<PessoaViewModel>> ListarPessoasAtivas()
        {
            var _response = new List<PessoaViewModel>();

            var _data = await _IServicoPessoa.ListarPessoasAtivas();
            if (_data.Count() > 0)
            {
                _response = _data.Select(x => new PessoaViewModel
                {
                    Id = x.Id,
                    DataNascimento = x.DataNascimento,
                    Email = x.Email,
                    Escolaridade = x.Escolaridade,
                    Nome = x.Nome,
                    Sobrenome = x.Sobrenome
                }).ToList();

            }

            return _response;

        }

        public async Task Adicionar(Pessoa Objeto)
        {
            await _IPessoa.Adicionar(Objeto);
        }

        public async Task Atualizar(Pessoa Objeto)
        {
            await _IPessoa.Atualizar(Objeto);
        }

        public async Task<Pessoa> BuscarPorId(int Id)
        {
            return await _IPessoa.BuscarPorId(Id);
        }

        public async Task Excluir(Pessoa Objeto)
        {
            await _IPessoa.Excluir(Objeto);
        }

        public async Task<List<Pessoa>> Listar()
        {

            return await _IPessoa.Listar();

        }


    }
}
