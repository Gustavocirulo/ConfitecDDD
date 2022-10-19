using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoPessoa
    {
        Task AdicionarPessoa(Pessoa noticia);
        Task AtualizarPessoa(Pessoa noticia);
        Task<List<Pessoa>> ListarPessoasAtivas();
    }
}
