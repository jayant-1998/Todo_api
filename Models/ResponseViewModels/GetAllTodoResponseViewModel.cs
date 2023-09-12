namespace TodoAPI.Models.ResponseViewModels
{
    public class GetAllTodoResponseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
