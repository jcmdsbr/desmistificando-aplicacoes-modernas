﻿using _5by5.InterAction.Sample.Domain.Commands.v1.CreateCustomer;
using _5by5.InterAction.Sample.Domain.Queries.v1.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _5by5.InterAction.Sample.Api.Controllers.v1;

[Route("api/v1/customers")]
[ApiController]
public sealed class CustomerController(IMediator bus) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerCommand model, CancellationToken token)
    {
        var result = await bus.Send(model);

        return StatusCode((int)HttpStatusCode.Created, new
        {
            Content = result,
            Notification = "Cliente cadastrado com sucesso!!!"
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken token)
    {
        var customer = await bus.Send(new GetCustomerByIdQuery(id));

        if (customer is null) return NotFound();

        return Ok(new
        {
            Content = customer
        });
    }
}