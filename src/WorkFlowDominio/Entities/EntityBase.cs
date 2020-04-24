using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominioShared.Events;

namespace WorkFlowDominio.Entities
{
    public enum StateEntityBaseEnum
    {
        Inclusao = 1,
        Alteracao = 2,
        Delecao = 3
    }

    public abstract class EntityBase: Entity
    {

        protected EntityBase()
        {
           
            Ativo = true;
            StateEntityBase = StateEntityBaseEnum.Inclusao;
        }

        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public Guid IdUsuarioCadastro { get; set; }
        public Guid? IdUsuarioAlteracao { get; set; }
        public DateTime? DataDelecao { get; set; }
        public Guid? IdUsuarioDelecao { get; set; }

        public StateEntityBaseEnum StateEntityBase { get; set; }

    }
}
