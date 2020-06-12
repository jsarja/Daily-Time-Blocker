using System;
using Microsoft.AspNetCore.Mvc;
using Planner.Application.TodoManagement.TodoActions.CreateActions;
using Planner.Application.TodoManagement.TodoActions.DeleteActions;
using Planner.Application.TodoManagement.TodoActions.ReadActions;
using Planner.Application.TodoManagement.TodoActions.UpdateActions;

namespace Planner.WebMVC.Controllers
{
    [Route("/")]
    public class TodosController : Controller
    {
        private readonly ICreateActions m_createController;
        private readonly IReadActions m_readController;
        private readonly IUpdateActions m_updateController;
        private readonly IDeleteActions m_deleteController;
        
        public TodosController(ICreateActions createActions, IReadActions readActions,
            IUpdateActions updateActions, IDeleteActions deleteActions)
        {
            m_createController = createActions;
            m_readController = readActions;
            m_updateController = updateActions;
            m_deleteController = deleteActions;
        }
        
        public IActionResult DailyProgression()
        {
            var todaySearchArg = new GetDailyTodoItemBlocksSearchArgs
            {
                Date = new DateTime(2020, 6, 9)
            };

            var tasksToday = m_readController.GetDailyTodoItemBlocks(todaySearchArg);
                
            return View();
        }

        public IActionResult SchelduleTodos()
        {
            return View();
        }
    }
}