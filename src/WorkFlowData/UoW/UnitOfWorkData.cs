using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowData.Context;
using WorkFlowDominioShared.Interfaces;

namespace WorkFlowData.UoW
{
    public class UnitOfWorkData : UnitOfWork<Contexto>
    {
        public UnitOfWorkData(Contexto context)
                    : base(context)
        {


        }
    }
}
