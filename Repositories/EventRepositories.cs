using System.Collections.Generic;
using EventLogger.Entities;
using System.Linq;
using EventLogger.Enums;

namespace EventLogger.Repositories
{   

    /// <summary>
    /// Event repository for the Events. Implments EventRepositories interface.
    /// </summary>
    public class EventRepositories : IEventRepositories
    {   

        /// <summary>
        /// Works as In Memory DB to store and read all events. 
        /// List (Collections) is used to simply the coding process.
        /// </summary>
        /// <returns>List<Event></returns>
        private readonly List<Event> events = new()
        {
            new Event { Id = Guid.NewGuid(), UserName = "007rau", EventType = EventType.Clicked_Link_On_Web_Page, EventContent = "https://007rau.github.io", CreatedAt = DateTimeOffset.UtcNow },
            new Event { Id = Guid.NewGuid(), UserName = "pandey72", EventType = EventType.Print_Content_Web_Page, EventContent = "Hello World", CreatedAt = DateTimeOffset.UtcNow },
            new Event { Id = Guid.NewGuid(), UserName = "User123", EventType = EventType.Print_Content_Web_Page, EventContent = "This Assessment Rocks!!!", CreatedAt = DateTimeOffset.UtcNow }
        };

        public List<Event> Events => events;


        /// <summary>
        /// Returns all the event for the DB.
        /// </summary>
        /// <returns>IEnumerable<Event></returns>
        public IEnumerable<Event> GetEvents()
        {
            return Events;
        }


        /// <summary>
        /// Returns event with id same as param id else null.
        /// </summary>
        /// <param name="id">id of the event</param>
        /// <returns>Event</returns>
        public Event GetEvent(Guid id)
        {
            var e = Events.Where(e => e.Id == id).SingleOrDefault();
            return e;
        }


        /// <summary>
        /// Adds newly created event into List of events in the DB.
        /// </summary>
        /// <param name="e">Event</param>
        public void CreateEvent(Event e)
        {
            Events.Add(e);
        }
    }
}