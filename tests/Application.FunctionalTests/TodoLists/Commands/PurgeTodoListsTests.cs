﻿using LinkIo.Application.Common.Exceptions;
using LinkIo.Application.Common.Security;
using LinkIo.Application.TodoLists.Commands.CreateTodoList;
using LinkIo.Application.TodoLists.Commands.PurgeTodoLists;
using LinkIo.Domain.Entities;

namespace LinkIo.Application.FunctionalTests.TodoLists.Commands;

using static Testing;

public class PurgeTodoListsTests : BaseTestFixture
{
    //[Test]
    //public async Task ShouldDenyAnonymousUser()
    //{
    //    var command = new PurgeTodoListsCommand();

    //    command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

    //    var action = () => SendAsync(command);

    //    await action.Should().ThrowAsync<UnauthorizedAccessException>();
    //}

    //[Test]
    //public async Task ShouldDenyNonAdministrator()
    //{
    //    var command = new PurgeTodoListsCommand();

    //    var action = () => SendAsync(command);

    //    await action.Should().ThrowAsync<ForbiddenAccessException>();
    //}

    //[Test]
    //public async Task ShouldAllowAdministrator()
    //{
    //    var command = new PurgeTodoListsCommand();

    //    var action = () => SendAsync(command);

    //    await action.Should().NotThrowAsync<ForbiddenAccessException>();
    //}

    [Test]
    public async Task ShouldDeleteAllLists()
    {
        await SendAsync(new CreateTodoListCommand
        {
            Title = "New List #1"
        });

        await SendAsync(new CreateTodoListCommand
        {
            Title = "New List #2"
        });

        await SendAsync(new CreateTodoListCommand
        {
            Title = "New List #3"
        });

        await SendAsync(new PurgeTodoListsCommand());

        var count = await CountAsync<TodoList>();

        count.Should().Be(0);
    }
}
