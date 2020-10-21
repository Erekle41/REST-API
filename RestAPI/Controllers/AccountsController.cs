using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Data.Interfaces;
using RestAPI.DTOs.AccountDTOs;
using RestAPI.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.JsonPatch;

namespace RestAPI.Controllers
{
    //api/Accounts
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository accountRepository, IMapper mapper)
        {
            _repository = accountRepository;
            _mapper = mapper;
        }
        //GET api/Accounts
        [HttpGet]
        public ActionResult<IEnumerable<AccountReadDto>> GetAccounts()
        {
            var accs = _repository.Set().Where(x => x.SysFields.IsDeleted != true).ToList();

            return Ok(_mapper.Map<IEnumerable<AccountReadDto>>(accs));
        }

        //GET api/accounts/{id}
        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<AccountReadDto> GetAccountById(int id)
        {
            var acc = _repository.Find(id);
            if (acc != null && acc.SysFields.IsDeleted != true)
            {
                return Ok(_mapper.Map<AccountReadDto>(acc));
            }
            return NotFound();
        }

        //POST api/accounts
        [HttpPost]
        public ActionResult<AccountReadDto> CreateAccount(AccountCreateDto acc)
        {
            var accModel = _mapper.Map<Account>(acc);
            _repository.Add(accModel);
            _repository.Save();

            var readDto = _mapper.Map<AccountReadDto>(accModel);

            return CreatedAtRoute(nameof(GetAccountById), new { id = accModel.ID }, readDto);
        }

        //PUT api/accounts/{id}
        [HttpPut("{id}")]
        public ActionResult<AccountReadDto> UpdateAccount(int id, AccountUpdateDto dto)
        {
            var oldAcc = _repository.Find(id);
            if (oldAcc == null)
            {
                return NotFound();
            }
            if (_repository.Set().Where(x=> x.ID != id).Any(x=> x.UserName == dto.UserName))
            {
                return BadRequest(); // Could use retouching
            }

            _mapper.Map(dto, oldAcc);

            _repository.Save();

            return NoContent();// Ok(oldAcc);
        }

        //PATCH api/accounts/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchAccount(int id,JsonPatchDocument<AccountUpdateDto> patch)
        {
            var oldAcc = _repository.Find(id);
            if (oldAcc == null)
            {
                return NotFound();
            }

            var accToPatch = _mapper.Map<AccountUpdateDto>(oldAcc);
            patch.ApplyTo(accToPatch, ModelState);
            if (!TryValidateModel(accToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(accToPatch, oldAcc);

            _repository.Save();

            return NoContent();
        }

        //DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount (int id)
        {
            var account = _repository.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            account.SysFields.IsDeleted = true;
            _repository.Save();

            return NoContent();
        }

        ////Get api/accounts/{username}
        //[HttpGet("{username}", Name = "GetAccountByName")]
        //public ActionResult<AccountReadDto> GetAccountByName(string username)
        //{
        //    var acc = _repository.Set().FirstOrDefault(x => x.UserName == username);
        //    if (acc != null)
        //    {
        //        return Ok(_mapper.Map<AccountReadDto>(acc));
        //    }
        //    return NotFound();
        //}
    }
}