using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string? NomePropriedade { get; set; }
        [NotMapped]
        public string? Mensagem { get; set; }
        [NotMapped]
        public List<Notifica> Notificacoes { get; set; }

        public bool ValidarPropriedadeString(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica()
                {
                    Mensagem = "Campo Obrigatório",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidarPropriedadeEmail(string valor, string nomePropriedade)
        {

            if (!new EmailAddressAttribute().IsValid(valor))
            {
                Notificacoes.Add(new Notifica()
                {
                    Mensagem = "Email inválido",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }

            return true;
        }

        public bool ValidarPropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor == 0 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica()
                {
                    Mensagem = "Não pode ser valor zerado",
                    NomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
    }
}
