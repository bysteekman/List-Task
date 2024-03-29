using System;
using System.ComponentModel.DataAnnotations;

namespace todo_rest_api.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Done { get; set; }


        public int ListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}