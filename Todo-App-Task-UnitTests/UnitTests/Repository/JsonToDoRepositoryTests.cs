using Todo_App_Task.App.Repository;
using todoapp.App.Repository.Dtos;
using Xunit;

namespace Todo_App_Task_UnitTests.UnitTests.Repository;

public class JsonToDoRepositoryTests
{
        private readonly string _testFilePath = "TestToDoData.json";

        private JsonToDoRepository GetRepository()
        {
            return new JsonToDoRepository(_testFilePath);
        }

        [Fact]
        public async Task AddAsync_ShouldAddToDo()
        {
            // Arrange
            var repository = GetRepository();
            var todo = new ToDo { Title = "Test ToDo", IsCompleted = false };

            // Act
            await repository.AddAsync(todo);
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test ToDo", result.First().Title);

            // Clean up
            File.Delete(_testFilePath);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnToDo()
        {
            // Arrange
            var repository = GetRepository();
            var todo = new ToDo { Id = 1, Title = "Test ToDo", IsCompleted = false };
            await repository.AddAsync(todo);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test ToDo", result.Title);

            // Clean up
            File.Delete(_testFilePath);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveToDo()
        {
            // Arrange
            var repository = GetRepository();
            var todo = new ToDo { Id = 1, Title = "Test ToDo", IsCompleted = false };
            await repository.AddAsync(todo);

            // Act
            await repository.DeleteAsync(1);
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);

            // Clean up
            File.Delete(_testFilePath);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyToDo()
        {
            // Arrange
            var repository = GetRepository();
            var todo = new ToDo { Id = 1, Title = "Original Title", IsCompleted = false };
            await repository.AddAsync(todo);

            // Act
            todo.Title = "Updated Title";
            await repository.UpdateAsync(todo);
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);

            // Clean up
            File.Delete(_testFilePath);
        }
}
