using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Todo_App_Task.App.Controller;
using Todo_App_Task.App.Repository;
using todoapp.App.Repository.Dtos;
using Xunit;

namespace Todo_App_Task_UnitTests.UnitTests.Controller
{
    public class ToDoControllerTests
    {
        private readonly IJsonToDoRepository _repository;
        private readonly ToDoController _controller;

        public ToDoControllerTests()
        {
            _repository = Substitute.For<IJsonToDoRepository>();
            _controller = new ToDoController(_repository);
        }

        [Fact]
        public async Task GetToDos_ShouldReturnOkWithToDos()
        {
            // Arrange
            var todos = new List<ToDo> { new ToDo { Id = 1, Title = "Test ToDo", IsCompleted = false } };
            _repository.GetAllAsync().Returns(todos);

            // Act
            var result = await _controller.GetToDos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ToDo>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Test ToDo", returnValue[0].Title);
        }

        [Fact]
        public async Task GetToDo_ShouldReturnNotFoundWhenToDoNotExists()
        {
            // Arrange
            _repository.GetByIdAsync(1).Returns((ToDo)null);

            // Act
            var result = await _controller.GetToDo(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetToDo_ShouldReturnOkWithToDoWhenExists()
        {
            // Arrange
            var todo = new ToDo { Id = 1, Title = "Test ToDo", IsCompleted = false };
            _repository.GetByIdAsync(1).Returns(todo);

            // Act
            var result = await _controller.GetToDo(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ToDo>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Test ToDo", returnValue.Title);
        }

        [Fact]
        public async Task PostToDo_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var todo = new ToDo { Title = "New ToDo", IsCompleted = false };

            // Act
            var result = await _controller.PostToDo(todo);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ToDo>(createdAtActionResult.Value);
            Assert.Equal("New ToDo", returnValue.Title);
        }

        [Fact]
        public async Task PutToDo_ShouldReturnNoContentOnSuccess()
        {
            // Arrange
            var todo = new ToDo { Id = 1, Title = "Updated ToDo", IsCompleted = true };
            _repository.GetByIdAsync(1).Returns(todo);
            _repository.UpdateAsync(todo).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutToDo(1, todo);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutToDo_ShouldReturnNotFoundWhenToDoNotExists()
        {
            // Arrange
            var todo = new ToDo { Id = 1, Title = "Updated ToDo", IsCompleted = true };
            _repository.GetByIdAsync(1).Returns(null as ToDo);

            // Act
            var result = await _controller.PutToDo(1, todo);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteToDo_ShouldReturnNoContentOnSuccess()
        {
            // Arrange
            var todo = new ToDo { Id = 1, Title = "Updated ToDo", IsCompleted = true };
            _repository.GetByIdAsync(1).Returns(todo);
            _repository.DeleteAsync(1).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteToDo(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteToDo_ShouldReturnNotFoundWhenToDoNotExists()
        {
            // Arrange
            var todo = new ToDo { Id = 2, Title = "Updated ToDo", IsCompleted = true };
            _repository.GetByIdAsync(2).Returns(null as ToDo);

            // Act
            var result = await _controller.DeleteToDo(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
