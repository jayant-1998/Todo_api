namespace TodoAPI.Models.ResponseViewModels
{
    public class TodoResponseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }

    }
}
