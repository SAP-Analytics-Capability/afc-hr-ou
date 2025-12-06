using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IVcsData
    {
        void InsertVcs(Vcs vcs);

        Vcs GetVcsById(int vcsId);

        Vcs GetVcsByCode(string code);

        Vcs GetVcsByDesc(string vcsDesc);

        List<Vcs> GetVcs();
    }
}