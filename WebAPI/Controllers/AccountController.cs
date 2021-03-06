using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccountAsync([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var added = await _accountService.PostAccountAsync(account);
                return Created($"/{added.Username}", added);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<Account>> GetAccountAsync([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var account = await _accountService.GetAccountAsync(username);
                return Ok(account);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<Account>> GetAccountAsync([FromQuery] string username, [FromQuery] string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var account = await _accountService.GetAccountAsync(username, password);
                return Ok(account);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }

        [HttpDelete]
        [Route("{username}")]
        public async Task<ActionResult> DeleteAccountAsync([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            { 
                await _accountService.DeleteAccountAsync(username);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); 
            }  
        }

        
        
        private ActionResult<Account> HandleException(string message)
        {
            Console.WriteLine($"ACCOUNT CONTROLLER: {message}");
            switch (message)
            {
                case Status.UserNotFound:
                {
                    return NotFound(message);
                }
                case Status.IncorrectPassword:
                {
                    return BadRequest(message);
                }
                case Status.UserAlreadyExists:
                {
                    return Conflict(message);
                }
            }
            return StatusCode(500, message); 
        }
    }
}