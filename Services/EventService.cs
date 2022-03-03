using EventLogger.Dtos;
using EventLogger.Entities;
using EventLogger.Enums;
using EventLogger.Repositories;

namespace EventLogger.Services {

    /// <summary>
    /// Service Class for Events.
    /// </summary>
    public class EventService {
        private readonly IEventRepositories repository;

        public EventService(IEventRepositories repository) {
            this.repository = repository;
        }



        /// <summary>
        /// Gets all the events from DB and returns to controller as Event Dtos.
        /// If DB is empty, returns null.
        /// </summary>
        /// <returns>IEnumerable<EventDto></returns>
        public IEnumerable<EventDto> getAllEvents() {
            var events =  repository.GetEvents();

            if(events is null || events.Count() == 0) {
                return null;
            }

            return events.Select(e => e.AsDto());
        }


        /// <summary>
        /// Gets the event from the DB where Guid is same as id, then returns to controller as Event Dto. 
        /// Returns null if not found.
        /// </summary>
        /// <param name="id">Guid of the event requested</param>
        /// <returns>EventDto</returns>
        public EventDto getEvent(Guid id) {
            var e = repository.GetEvent(id);
            if ( e is null) {
                return null;
            }
            return e.AsDto();
        }


        /// <summary>
        /// Creates new event object and passes to repository to save in DB.
        /// Returns the newly created event to controller as Event Dto.
        /// </summary>
        /// <param name="createEventDto">List of fields needed to created new event record</param>
        /// <returns>EventDto</returns>
        public EventDto CreateEvent(CreateEventDto createEventDto) {
            Event e = new() {
                Id = Guid.NewGuid(),
                UserName = createEventDto.UserName,
                EventType = (EventType) createEventDto.EventType,
                EventContent = createEventDto.EventContent,
                CreatedAt = DateTimeOffset.UtcNow
            };

            repository.CreateEvent(e);

            return getEvent(e.Id);
        }


        /// <summary>
        /// Filters the events in the date range and performs analytics on the filtered events.
        /// Creates AnalyticsDto with values found by analytics else not found result. 
        /// </summary>
        /// <param name="StartTime">Start date in the range search. Format is "YYYY-MM-DDTHH:MM:SS"</param>
        /// <param name="EndTime">End date in the range search. Format is "YYYY-MM-DDTHH:MM:SS"</param>
        /// <returns>AnalyticsDto</returns>
        public AnalyticsDto GetAnalyticsInRange(DateTime StartTime, DateTime EndTime) {
            IEnumerable<Event> events = repository.GetEvents().Where(e => e.CreatedAt>=StartTime && e.CreatedAt<=EndTime);

            if(events == null || events.Count() == 0) {
                return new AnalyticsDto {
                    ActionTakenMostNumberOfTime = "No event found in this data range.",
                    TotalNumberOfTimeActionWasTaken = 0,
                    NumberOfTimeActionTakenPerUser = new SortedDictionary<string, int>()
                };
            }

            SortedDictionary<int, int> Actions = new SortedDictionary<int, int>();
            
            foreach( var e in events) {
                    if(Actions.ContainsKey(((int)e.EventType))) {
                      Actions[(int)e.EventType] += 1; 
                    } else {
                        Actions.Add((int) e.EventType, 1);
                    }
            }

            var ans = Actions.OrderByDescending(kvp => kvp.Value).First();

            SortedDictionary<string, int> ActionPerUser = new SortedDictionary<string, int>();

            foreach( var e in events) {
                    if(ActionPerUser.ContainsKey(e.UserName)) {
                        if((int)e.EventType == ans.Key) {
                            ActionPerUser[e.UserName] += 1;
                        }
                    } else {
                        if((int)e.EventType == ans.Key) {
                            ActionPerUser.Add(e.UserName, 1);
                        } else {
                            ActionPerUser.Add(e.UserName, 0);
                        }
                    }
            }

            return new AnalyticsDto {
                ActionTakenMostNumberOfTime = Enum.GetName(typeof(EventType), ans.Key),
                TotalNumberOfTimeActionWasTaken = ans.Value,
                NumberOfTimeActionTakenPerUser = ActionPerUser
            };
        }


        /// <summary>
        /// Performs analytics on the filtered events.
        /// Creates AnalyticsDto with values found by analytics else not found result. 
        /// </summary>
        /// <returns>AnalyticsDto</returns>
        public AnalyticsDto GetAnalyticsAll() {
            IEnumerable<Event> events = repository.GetEvents();

            if(events == null || events.Count() == 0) {
                return new AnalyticsDto {
                    ActionTakenMostNumberOfTime = "No event found.",
                    TotalNumberOfTimeActionWasTaken = 0,
                    NumberOfTimeActionTakenPerUser = new SortedDictionary<string, int>()
                };
            }

            SortedDictionary<int, int> Actions = new SortedDictionary<int, int>();
            
            foreach( var e in events) {
                    if(Actions.ContainsKey(((int)e.EventType))) {
                      Actions[(int)e.EventType] += 1; 
                    } else {
                        Actions.Add((int) e.EventType, 1);
                    }
            }

            var ans = Actions.OrderByDescending(kvp => kvp.Value).First();

            SortedDictionary<string, int> ActionPerUser = new SortedDictionary<string, int>();

            foreach( var e in events) {
                    if(ActionPerUser.ContainsKey(e.UserName)) {
                        if((int)e.EventType == ans.Key) {
                            ActionPerUser[e.UserName] += 1;
                        }
                    } else {
                        if((int)e.EventType == ans.Key) {
                            ActionPerUser.Add(e.UserName, 1);
                        } else {
                            ActionPerUser.Add(e.UserName, 0);
                        }
                    }
            }

            return new AnalyticsDto {
                ActionTakenMostNumberOfTime = Enum.GetName(typeof(EventType), ans.Key),
                TotalNumberOfTimeActionWasTaken = ans.Value,
                NumberOfTimeActionTakenPerUser = ActionPerUser
            };
        }
    } 
}