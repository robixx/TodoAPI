﻿namespace TodoAPI.DTOs
{
    public class TodoItemUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
