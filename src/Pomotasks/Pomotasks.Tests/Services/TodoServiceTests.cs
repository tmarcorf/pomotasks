using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Mappers;
using Pomotasks.Persistence.Context;
using Pomotasks.Persistence.Interfaces;
using Pomotasks.Persistence.Repositories;
using Pomotasks.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Tests.Services
{
    [TestClass]
    public class TodoServiceTests
    {
        private Mock<ApplicationContext> _mockApplicationContext = null;
        private Mock<TodoRepository> _mockTodoRepository = null;
        private Mock<Mapping<Todo, DtoTodo>> _mapper = null;
        private TodoService _todoService = null;

        [TestInitialize]
        public void Setup()
        {
            InitializeMocks();
            _todoService = new TodoService(_mockTodoRepository.Object, _mapper.Object);
        }

        [TestMethod]
        [DataRow("98BF4295-726F-477A-A185-695BA32ED7AD", 0, 10)]
        public async Task FindAll_InformationExists(string userId, int skip, int take)
        {
            var pagedResult = new List<Todo>
            {
                new Todo
                {
                    CreationDate = DateTime.Now,
                    Details = "test details",
                    Title = "test title",
                    Done = false,
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(userId)
                }
            };

            MockApplicationContext(pagedResult);

            var tcsPagedResult = new TaskCompletionSource<IEnumerable<Todo>>();
            tcsPagedResult.SetResult(pagedResult);

            _mockTodoRepository
                .Setup(mockRepo => mockRepo.FindAll(Guid.Parse(userId), skip, take))
                .Returns(tcsPagedResult.Task);

            var serviceResult = await _todoService.FindAll(userId, skip, take);

            Assert.IsNotNull(serviceResult);
            Assert.AreEqual(serviceResult.Data?.ToList()[0].UserId, pagedResult[0].UserId.ToString());
        }

        private void InitializeMocks()
        {
            _mockApplicationContext = new Mock<ApplicationContext>();

            _mockTodoRepository = new Mock<TodoRepository>(_mockApplicationContext.Object);
            _mapper = new Mock<Mapping<Todo, DtoTodo>>();
        }

        private void MockApplicationContext(List<Todo> todos)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "PomotasksDB")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                todos.ForEach(todo =>
                {
                    context.Todos?.Add(todo);
                });

                context.SaveChanges();
            }
        }
    }
}
