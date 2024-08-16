namespace TaskManagementEntity.Model
{
    public class TaskItem
    {
        public enum StatusTask
        {
            Undefined = 0,
            Created = 1,
            Started = 2,
            Completed = 3,
            Cancelled = 4,
            Error = 5
        }

        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public StatusTask Status { get; set; }
    }
}
