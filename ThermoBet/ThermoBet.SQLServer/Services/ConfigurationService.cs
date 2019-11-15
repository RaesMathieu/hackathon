using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ThermoBet.Core.Interfaces;
using ThermoBet.Data;

namespace ThermoBet.SQLServer.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ThermoBetContext _thermoBetContext;

        public ConfigurationService(ThermoBetContext thermoBetContext)
        {
            _thermoBetContext = thermoBetContext;
        }

        public async Task<DateTime> GetDateTimeUtcNow()
        {
            var config = await _thermoBetContext.Configurations.SingleOrDefaultAsync(x => x.Key == "DateTimeUtcNow");

            if (config == null || new DateTime(long.Parse(config.Value)).Year <= 1990)
                return DateTime.UtcNow;
            return new DateTime(long.Parse(config.Value));
        }

        public async Task SetDateTimeUtcNow(DateTime dateTime)
        {
            var config = await _thermoBetContext.Configurations.SingleOrDefaultAsync(x => x.Key == "DateTimeUtcNow");
            if (config == null)
                _thermoBetContext.Configurations
                    .Add(new Models.ConfigurationModel
                    {
                        Key = "DateTimeUtcNow",
                        Value = dateTime.Ticks.ToString()
                    });
            else
                config.Value = dateTime.Ticks.ToString();

            await _thermoBetContext.SaveChangesAsync();
        }
    }
}
