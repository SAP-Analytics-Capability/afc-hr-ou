using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IOrganizationData
    {
        void InsertOrganization(Organization organization);

        List<Organization> GetOrganizations();

        Organization GetOrganizationById(int organizationId);

        Organization GetOrganizationByCode(string organizationCode);

        Organization GetOrganizationByDesc(string organizationDesc);

    }
}