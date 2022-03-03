using System.ComponentModel.DataAnnotations;
using EventLogger.Enums;

namespace EventLogger.Dtos
{   
    /// <summary>
    /// DTO to create new events from the UI/Swagger/Postman.
    /// </summary>
    /// <value></value>
    public record CreateEventDto {

        /// <summary>
        /// Username of the user.
        /// </summary>
        [Required]
        public string UserName { get; init; }

        /// <summary>
        /// Event type (action) performed by the user. 
        /// Stored as Enum for validation of the input from UI.
        /// </summary>
        /// <value>[0, 1, 2]</value>
        [Required]
        [Range(0, 2)]
        public EventType EventType { get; init; }

        /// <summary>
        /// The action content taken by the user during the event. 
        /// </summary>
        [Required]
        public string EventContent { get; init; }
    }
}