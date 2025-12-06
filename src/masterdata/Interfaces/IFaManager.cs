using System;
using System.Collections.Generic;

using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IFaManager
    {
        bool CanStart(FunctionalAck fa);
        void ToRunning();
        void ToCompleted();
        void ToInterrupted();
        FunctionalAck GetCurrFA();
        void SetCurrFa(FunctionalAck fa);
    }
}