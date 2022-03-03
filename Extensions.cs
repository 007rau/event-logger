using EventLogger.Dtos;
using EventLogger.Entities;

namespace EventLogger
{   

    /// <summary>
    /// Helper Class to transform Event Object to EventDto Object.
    /// </summary>
    public static class Extensions {

        /// <summary>
        /// Helper function to transform the Event Object to EventDto Object
        /// </summary>
        /// <param name="e">Event Object to be transformed</param>
        /// <returns>EventDto</returns>    
        public static EventDto AsDto(this Event e) {
            return new EventDto {
                Id = e.Id,
                UserName = e.UserName,
                EventType = e.EventType.ToString(),
                EventContent = e.EventContent,
                CreatedAt = e.CreatedAt
            };
        }
    }
}