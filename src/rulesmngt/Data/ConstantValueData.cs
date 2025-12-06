using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class ConstantValueData : IConstantValueData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;
        private ILogger Logger;

        public ConstantValueData(IOptions<DatabaseConfiguration> databaseConfiguration, ILoggerFactory loggerfactory)
        {
            this.databaseConfiguration = databaseConfiguration;
            this.Logger = loggerfactory.CreateLogger<ConstantValueData>();
        }

        public List<ConstantValue> GetConstants()
        {
            List<ConstantValue> constants = new List<ConstantValue>();

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    constants = (from e in context.ConstantValue select e).ToList();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Unable to get constaints.", e);
            }
            return constants;
        }

        public ConstantValue GetConstant()
        {
            List<ConstantValue> constants = new List<ConstantValue>();
            ConstantValue constant = new ConstantValue();

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    constants = (from e in context.ConstantValue select e).ToList();
                }

                constant = constants.FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.LogError("Unable to get the constan values.", e);
            }

            return constant;
        }
    }
}