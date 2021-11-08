using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AccountController : ControllerBase
    {
        private IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccountAsync([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var added = await _accountRepo.PostAccountAsync(account);
                return Created($"/{added.Username}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Account>> GetAccountAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var account = await _accountRepo.GetAccountAsync();
                return Ok(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}