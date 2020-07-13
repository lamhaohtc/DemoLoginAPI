using DemoLogin.Models;
using DemoLogin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLogin.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase

    {
        private readonly AccountService _accountService;

        public AccountsController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<List<Account>> Get() =>
            _accountService.Get();

        [HttpGet("{id:length(24)}", Name ="GetAccount")]
        public ActionResult<Account> Get(string id)
        {
            var account = _accountService.Get(id);
            if(account == null)
            {
                return NotFound();
            }
            else
            {
                return account;
            }
        }

        [HttpPost]
        public ActionResult<Account> Create (Account account)
        {
            _accountService.Create(account);

            return CreatedAtRoute("GetAccount", new { id = account.Id.ToString() }, account);
        }

        [HttpPut("{id:lenght(24)}")]
        public IActionResult Update(string id, Account accountIn)
        {
            var account = _accountService.Get(id);

            if(account == null)
            {
                return NotFound();
            }

            _accountService.Update(id, accountIn);

            return NoContent();
        }

        [HttpDelete("{id:lenght(24)}")]
        public IActionResult Delete(string id)
        {
            var account = _accountService.Get(id);
            if(account == null)
            {
                return NotFound();
            }
            _accountService.Remove(id);
            return NoContent();
        }    


    }
}
