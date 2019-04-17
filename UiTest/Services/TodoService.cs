namespace UiTest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ApiTest;
    using UiTest.Models;

    public class TodoService : HttpService
    {
        public async Task<List<Todo>> GetAllTodos()
        {
            List<Todo> todos;
            todos = await this.Get<List<Todo>>(path: $"{BaseUrl}/todos");
            return todos;
        }

        public async Task<Todo> CreateNewTodo(Todo newItem)
        {
            var response = await Post(newItem, path: $"{BaseUrl}/todos");
            return response;
        }
    }
}
