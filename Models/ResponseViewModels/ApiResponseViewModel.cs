﻿namespace TodoAPI.Models.ResponseViewModels
{
    public class ApiResponseViewModel
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int Code { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
