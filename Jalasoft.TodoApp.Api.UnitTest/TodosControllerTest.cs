namespace Tests
{
    using System.Collections.Generic;
    using ApiTest.Controllers;
    using ApiTest.Model;
    using ApiTest.Services;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [TestCase]

        // Name -> NameOfTheMethod_Scenario_ExpectedBehavior
        public void GetAll_Success_ReturnTodoList()
        {
            // Arrange -> definitions
            ITodoService service = new TodoService();
            TodosController api = new TodosController(service);

            // Act -> the endpoint for testing
            var result = api.Get();

            // Assert -> verifying the result
            Assert.IsInstanceOf<ActionResult<IEnumerable<Todo>>>(result);
        }
    }
}