﻿namespace TodoAPI.DTOs
{
    public class TodoItemReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
