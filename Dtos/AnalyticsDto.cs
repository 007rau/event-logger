
using EventLogger.Enums;

namespace EventLogger.Dtos
{       

    /// <summary>
    /// DTO to send Analytics Result.
    /// </summary>
    public record AnalyticsDto {

        /// <summary>
        /// The action that was taken the most number of times.
        /// </summary>
        public string ActionTakenMostNumberOfTime { get; init; }

        /// <summary>
        /// The total number of times the action was taken.
        /// </summary>
        public Int64 TotalNumberOfTimeActionWasTaken { get; init; }

        /// <summary>
        /// The number of times the action was taken per user.
        /// </summary>
        public SortedDictionary<string, int> NumberOfTimeActionTakenPerUser { get; init; }
    }
}