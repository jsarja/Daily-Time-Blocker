using System;

namespace Planner.Domain.Entities
{
    public class DailyTodoItemBlock
    {
        public int DailyTodoItemBlockId { get; set; }
        
        public DailyTodoItem DTodoItem { get; set; }
        //public int? DailyTodoItemId => DTodoItem.DailyTodoItemId;
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}