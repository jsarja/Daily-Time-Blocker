using System;

namespace Planner.Domain.Entities
{
    public class DailyTodoItem
    {
        public int DailyTodoItemId { get; set; }
        
        // https://stackoverflow.com/questions/18109547/orm-entities-vs-domain-entities-under-entity-framework-6-0
        public TodoItem TodoInfo { get; set; }
        public DateTime TodoDate { get; set; }
        public TimeSpan TimeUsedForTodo { get; set; }
        public TimeSpan? TimeReservedForTodo { get; set; }
    }
}