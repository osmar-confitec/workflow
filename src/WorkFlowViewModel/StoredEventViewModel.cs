using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowViewModel
{

    public class StoredEventTipo
    {
        public int Ordem { get; set; }
        public string Coluna { get; set; }
        public string Valor { get; set; }

        public StatusAlteracaoColunaStoredEvent Status { get; set; }

        public StoredEventTipo()
        {
            Status = StatusAlteracaoColunaStoredEvent.NaoHouveAlteracao;
        }
    }

    public enum StatusAlteracaoColunaStoredEvent
    {
        Alterada = 1,
        Incluido = 2,
        Deletado = 3,
        NaoHouveAlteracao = 4
    }

    public class StoredEventViewModelDadosEntrada : PaginacaoViewModel
    {

        public Guid AggregatedId { get; set; }

        public string StrAggregatedId { get; set; }
    }



    public class StoredEventViewModelDadosRetorno
    {

        public List<StoredEventTipo> ColunasStatus { get; set; }

        public StatusAlteracaoColunaStoredEvent Status { get; set; }

        public string Usuario { get; set; }

        public List<StoredEventViewModelDadosRetorno> StoredEventViewModelDadosRetornos { get; set; }

        public string Id { get; set; }

        public string DataEventoString { get; set; }

        public DateTime DataEvento { get; set; }

        public string Dados { get; set; }

        public StoredEventViewModelDadosRetorno()
        {
            Status = StatusAlteracaoColunaStoredEvent.NaoHouveAlteracao;
            ColunasStatus = new List<StoredEventTipo>();
            StoredEventViewModelDadosRetornos = new List<StoredEventViewModelDadosRetorno>();
        }

    }
}
