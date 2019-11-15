using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ThermoBet.Core.Interfaces
{
    public interface IConfigurationService
    {
        Task<DateTime> GetDateTimeUtcNow();

        Task SetDateTimeUtcNow(DateTime dateTime);
    }
}
