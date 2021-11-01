using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using todo_rest_api.Models;

namespace todo_rest_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private TasksService tasksService;
        private ListService listService;
        
        private int listId;
        public TasksController(TasksService service, ListService listservice)
        {
            this.tasksService = service;
            this.listService = listservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTask()
        {
            listId = Int32.Parse(Request.Query["listId"]);

            if(listId == 0)
            {
                return tasksService.GetAll(listService.GetAll());
            }
            return tasksService.GetAllTaskByTodoListId(listService.GetListByListId(listId));
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTaskById(int id)
        {
            var todoTask = tasksService.GetTaskById(id, listService.GetListByListId(listId));

            if(todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTask(TodoItem todoTask)
        {
            TodoItem createdTask = tasksService.CreateTask(todoTask, listService.GetListByListId(listId), listId);
            
            return Created($"api/task/{createdTask.Id}", createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TodoItem model)
        {   
            var todoTask = tasksService.PutTask(id, model, listService.GetListByListId(listId), listId);

            if (todoTask == null)
            {
                return NotFound();
            }

            return Created($"/task/{id}", todoTask);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTask (int id, TodoItem model)
        {
            var todoTask = tasksService.PatchTask(id, model, listService.GetListByListId(listId));

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpDelete("{id}")]
        public ActionResult<TodoItem> DeleteTaskById(int id)
        {
            var todoTask = tasksService.DeleteTask(id, listService.GetListByListId(listId));

            if (todoTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}