using System;
using EventLogger.Enums;

namespace EventLogger.Dtos
{   

    /// <summary>
    /// DTO to send the event records to UI.
    /// </summary>
    public record EventDto {

        /// <summary>
        /// Unique identifier for every event.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string UserName { get; init; }

        /// <summary>
        /// Event type (action) performed by the user. 
        /// </summary>
        public string EventType { get; init; }

        /// <summary>
        /// The action content taken by the user during the event. 
        /// </summary>
        public string EventContent { get; init; }

        /// <summary>
        /// Time when the event was performed (created).
        /// </summary>
        public DateTimeOffset CreatedAt { get; init; }
    }
}