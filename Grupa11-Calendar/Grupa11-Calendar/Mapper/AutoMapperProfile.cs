using AutoMapper;
using Grupa11_Calendar.DTO;
using Grupa11_Calendar.Models;

namespace Grupa11_Calendar.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Morao si reci sta se u sta mapira zato sto koristis AutoMapper
            CreateMap<Event, eventDTO>();
            CreateMap<eventDTO, Event>(); //dodato zbog Create
            CreateMap<EventType, EventTypeDTO>();
            CreateMap<EventTypeDTO, EventType>();
            CreateMap<Calendar, CalendarDTO>();
            CreateMap<CalendarDTO, Calendar>();
        }
    }
}
