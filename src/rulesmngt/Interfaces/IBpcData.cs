using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IBpcData
    {
        void InsertBpc(Bpc bpc);

        List<Bpc> GetBpc();

        Bpc GetBpcById(int bpcId);
        
        Bpc GetBpcByCode(string bpcCode);

        Bpc GetBpcByDesc(string bpcDesc);
    }
}