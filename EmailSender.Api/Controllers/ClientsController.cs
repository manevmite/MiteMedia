using EmailSender.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace EmailSender.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IMessageService _messageService;

    public ClientsController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ImportClientsAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            XDocument xmlDocument = XDocument.Load(filePath);
            string json = JsonConvert.SerializeXNode(xmlDocument);

            var response = await _messageService.SendMessage(json);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
