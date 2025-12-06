using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class VcsData : IVcsData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public VcsData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertVcs(Vcs vcs)
        {
            //TODO
        }

        public List<Vcs> GetVcs()
        {
            List<Vcs> list;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    list = (from v in context.Vcs select v).ToList<Vcs>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetVcs: " + e.Message);
            }

            return list;
        }

        public Vcs GetVcsById(int vcsId)
        {
            Vcs vcs;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    vcs = (from v in context.Vcs
                           where v.VcsId == vcsId
                           select v).SingleOrDefault<Vcs>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetVcsById: " + e.Message);
            }

            return vcs;
        }        

        
        public Vcs GetVcsByCode(string vcsCode)
        {
            Vcs vcs;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    vcs = (from v in context.Vcs
                           where string.Equals(v.VcsCode, vcsCode, 
                                               StringComparison.OrdinalIgnoreCase)
                           select v).SingleOrDefault<Vcs>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetVcsByCode: " + e.Message);
            }

            return vcs;
        }

        public Vcs GetVcsByDesc(string vcsDesc)
        {
            Vcs vcs;

            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    vcs = (from v in context.Vcs
                           where string.Equals(v.Desc, vcsDesc, 
                                               StringComparison.OrdinalIgnoreCase)
                           select v).SingleOrDefault<Vcs>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetVcsByDesc: " + e.Message);
            }

            return vcs;
        }
    }
}