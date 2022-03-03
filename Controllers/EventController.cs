using Microsoft.AspNetCore.Mvc;
using EventLogger.Repositories;
using EventLogger.Entities;
using EventLogger.Dtos;
using EventLogger.Enums;
using EventLogger.Services;

namespace EventLogger.Controllers {

    /// <summary>
    /// Controller Class for Rest APIs.
    /// </summary>
    [ApiController]
    [Route("api/v1/events")]
    public class EventController : ControllerBase {
        private readonly EventService eventService;
        
        public EventController(EventService eventService) {
            this.eventService = eventService;
        }

        /// <summary>
        /// GET API. 
        /// Gets all the events from service.
        /// </summary>
        /// <returns>IEnumerable<EventDto></returns>
        [HttpGet]
        [Route("all")]
        public IEnumerable<EventDto> GetEvents() {
            var events =  eventService.getAllEvents();

            if(events is null || events.Count() == 0) {
                return Enumerable.Empty<EventDto>();
            }

            return events;
        }


        /// <summary>
        /// GET API. 
        /// Get event with param id from service.
        /// </summary>
        /// <param name="id">Guid of the event UI requests</param>
        /// <returns>ActionResult<EventDto></returns>
        [HttpGet("{id}")]
        public ActionResult<EventDto> GetEvent(Guid id) {
            var e = eventService.getEvent(id);

            if ( e is null) {
                return NotFound();
            }
            
            return e;
        }
        
        /// <summary>
        /// POST API. 
        /// Create new event and pass it to service.
        /// </summary>
        /// <param name="eventDto">List of fields needed to created new event record</param>
        /// <returns>ActionResult<EventDto></returns>
        [HttpPost]
        [Route("createEvent")]
        public ActionResult<EventDto> CreateEvent(CreateEventDto eventDto) {
            var createdEvent = eventService.CreateEvent(eventDto);

            return createdEvent;
        }

        /// <summary>
        /// GET API. 
        /// Get analytics from service for events in a given data range.
        /// </summary>
        /// <param name="dateStart">Start date in the range search. Format is "YYYY-MM-DDTHH:MM:SS"</param>
        /// <param name="dateEnd">End date in the range search. Format is "YYYY-MM-DDTHH:MM:SS"</param>
        /// <returns>ActionResult<AnalyticsDto></returns>
        [HttpGet]
        [Route("analytics/InRange")]
        public ActionResult<AnalyticsDto> Analytics(DateTime dateStart, DateTime dateEnd) {
            return eventService.GetAnalyticsInRange(dateStart, dateEnd);
        }


        /// <summary>
        /// GET API. 
        /// Get analytics for all event from service. 
        /// </summary>
        /// <returns>ActionResult<AnalyticsDto></returns>
        [HttpGet]
        [Route("analytics/All")]
        public ActionResult<AnalyticsDto> Analytics() {
            return eventService.GetAnalyticsAll();
        }
    }
}