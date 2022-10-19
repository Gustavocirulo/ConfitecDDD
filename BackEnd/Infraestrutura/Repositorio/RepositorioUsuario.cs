using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Entidades.Entidades;
using Entidades.Enums;
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
    public class RepositorioUsuario : RepositorioGenerico<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<Contexto> _OptionsBuilder;
        public RepositorioUsuario()
        {
            _OptionsBuilder = new DbContextOptions<Contexto>();
        }

        public async Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    await data.ApplicationUsers.AddAsync(
                        new ApplicationUser() { 
                            Email= email,
                            PasswordHash= senha,
                            Idade= idade,
                            Celular=celular,
                            Tipo = TipoUsuario.Comum
                        });
                }
            }
            catch
            {
                return false;
            }

            return true;

        }

        public async Task<bool> ExisteUsuario(string email, string senha)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    return await data.ApplicationUsers.Where(x => x.Email.Equals(email) && x.PasswordHash.Equals(senha)).AsNoTracking().AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<string> RetornaIdUsuario(string email)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    var usuario = await data.ApplicationUsers.Where(x => x.Email.Equals(email)).AsNoTracking().FirstOrDefaultAsync();

                    if (usuario == null)
                        return string.Empty;

                    return usuario.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public async Task<ApplicationUser> RetornaUsuarioPeloId(string id)
        {
            try
            {
                using (var data = new Contexto(_OptionsBuilder))
                {
                    var usuario = await data.ApplicationUsers.Where(x => x.Id.Equals(id)).AsNoTracking().FirstOrDefaultAsync();

                    if (usuario == null) return new ApplicationUser();

                    return usuario;
                }
            }
            catch (Exception)
            {
                return new ApplicationUser();
            }
        }
    }
}
