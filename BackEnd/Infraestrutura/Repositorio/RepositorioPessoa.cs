using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RepositorioPessoa : RepositorioGenerico<Pessoa>, IPessoa
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;
        public RepositorioPessoa()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }

        public async Task<List<Pessoa>> ListarPessoas(Expression<Func<Pessoa, bool>> exPessoa)
        {
            using (var data = new Contexto(_OptionsBuilder))
            {
                return await data.Pessoas.Where(exPessoa).AsNoTracking().ToListAsync();
            }
        }
    }
}
