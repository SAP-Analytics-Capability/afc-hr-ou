using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class OrganizationData : IOrganizationData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public OrganizationData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }
        
        public void InsertOrganization(Organization organization)
        {
            //TODO
        }
        
        public List<Organization> GetOrganizations()
        {
            List<Organization> organizations;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    organizations = (from o in context.Organization select o).ToList<Organization>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetOrganizations: " + e.Message);
            }

            return organizations;
        }

        public Organization GetOrganizationById(int organizationId)
        {
            Organization organization;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    organization = (from o in context.Organization
                                    where o.OrganizationId == organizationId
                                    select o).SingleOrDefault<Organization>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetOrganizationById: " + e.Message);
            }

            return organization;
        }  

        public Organization GetOrganizationByCode(string organizationCode)
        {
            Organization organization;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    organization = (from o in context.Organization
                                    where string.Equals(o.OrganizationCode, organizationCode,
                                                        StringComparison.OrdinalIgnoreCase) 
                                    select o).SingleOrDefault<Organization>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetOrganizationByCode: " + e.Message);
            }

            return organization;
        }   

        public Organization GetOrganizationByDesc(string organizationDesc)
        {
            Organization organization;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    organization = (from o in context.Organization
                                    where string.Equals(o.Desc, organizationDesc,
                                                        StringComparison.OrdinalIgnoreCase) 
                                    select o).SingleOrDefault<Organization>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetOrganizationByDesc: " + e.Message);
            }

            return organization;
        }  
    }
}