using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.ServiceCalls
{
    public interface IServiceCalls
    {
        public Task<List<Calendar>> GetCalendarByUserId(Guid userId);
    }
}
