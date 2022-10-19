using Aplicacao.Interfaces.Genericos;
using Aplicacao.ViewModel;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoPessoa : IGenericaAplicacao<Pessoa>
    {
        Task AdicionarPessoa(Pessoa noticia);
        Task AtualizarPessoa(Pessoa noticia);
        Task<List<PessoaViewModel>> ListarPessoasAtivas();
    }
}
