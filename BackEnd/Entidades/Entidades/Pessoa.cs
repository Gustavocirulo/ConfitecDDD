using Entidades.Enums;
using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Pessoa : Notifica
    {
        [Column("PSA_ID")]
        public int Id { get; set; }

        [Column("PSA_NOME")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Column("PSA_SOBRENOME")]
        [MaxLength(100)]
        public string Sobrenome { get; set; }

        [Column("PSA_EMAIL")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("PSA_DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }       
        [Column("PSA_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }        
        [Column("PSA_DATA_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }
        [Column("PSA_ATIVO")]
        public bool Ativo { get; set; }

        [Column("PSA_ESCOLARIDADE")]
        public TipoEscolaridade Escolaridade { get; set; }

    }
}
