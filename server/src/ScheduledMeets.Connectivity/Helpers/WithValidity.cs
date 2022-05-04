namespace ScheduledMeets.Connectivity.Helpers;

record WithValidity<TValue>
(
    TValue Value,
    DateTime? ValidTo
);
