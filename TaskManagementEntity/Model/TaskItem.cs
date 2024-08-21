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

        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public StatusTask Status { get; set; }
    }
}
