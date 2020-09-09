using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator mediator;
        public ActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List()
        {
            Console.WriteLine("getAll");
            return await mediator.Send(new ActivitiesList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            Console.WriteLine("get: " + id);
            return await mediator.Send(new ActivitiesDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(ActivitiesCreate.Command command)
        {
            return await mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, ActivitiesEdit.Command command)
        {
            Console.WriteLine("edit: " + id);
            command.Id = id;
            return await mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            Console.WriteLine("delete: " + id);
            return await mediator.Send(new ActivitiesDelete.Command{Id = id});
        }


    }
}