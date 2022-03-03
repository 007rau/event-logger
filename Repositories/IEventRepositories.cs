using System;
using System.Collections.Generic;
using EventLogger.Entities;

namespace EventLogger.Repositories
{   

    /// <summary>
    /// Interface for Event Repository.
    /// </summary>
    public interface IEventRepositories
    {
        Event GetEvent(Guid id);
        IEnumerable<Event> GetEvents();
        void CreateEvent(Event e);
    }
}