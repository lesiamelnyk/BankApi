using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApi.Services;
using BankApi.Models;

namespace BankApi.Controllers;

[ApiController]
[Route("api/transfer")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _service;

    public TransferController(ITransferService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public IActionResult Transfer([FromBody] TransferRequest request)
    {
        var result = _service.ProcessTransfer(request);
        if (!result) return BadRequest("Invalid transfer");

        return Ok("Success");
    }
}