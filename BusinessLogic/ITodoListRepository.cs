using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.API.Model;

namespace ToDoList.API.BusinessLogic
{
    public interface ITodoListRepository
    {
        ApiResult<List<TodoItem>> GetAll();
        ApiResult<TodoItem> GetById(int todoId);
        ApiResult<TodoItem> Create(TodoItem item);
        ApiResult<TodoItem> Update(TodoItem item);
        ApiResult<TodoItem> Delete(TodoItem item);

    }
}
