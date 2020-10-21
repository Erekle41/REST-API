using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data.Interfaces;
using RestAPI.DTOs.AccountDTOs;
using RestAPI.DTOs.WlogDTOs;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WlogsController : ControllerBase
    {
        private readonly IWlogRepository _repository;
        private readonly IWlogHistoryRepository _wlogHistoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public WlogsController(
            IWlogRepository wlogRepository, 
            IWlogHistoryRepository historyRepository,
            IAccountRepository accountRepository, 
            IMapper mapper)
        {
            _repository = wlogRepository;
            _wlogHistoryRepository = historyRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        //GET api/wlogs
        [HttpGet]
        public ActionResult<IEnumerable<WlogReadDto>> GetWlogs()
        {
            var wlogs = _repository.Set()
                .Where(x => x.SysFields.IsDeleted != true)
                /*.Include(x => x.Account)*/.ToList();

            return Ok(_mapper.Map<IEnumerable<WlogReadDto>>(wlogs));
        }

        //GET api/wlogs/{id}
        [HttpGet("{id}", Name = "GetWlogById")]
        public ActionResult<WlogReadDto> GetWlogById(int id)
        {
            var wlog = _repository.Find(id);
            if (wlog != null && wlog.SysFields.IsDeleted != true)
            {
                //wlog.Account = _accountRepository.Find(wlog.AccID);
                return Ok(_mapper.Map<WlogReadDto>(wlog));
            }
            return NotFound();
        }

        //POST api/wlogs
        [HttpPost]
        public ActionResult<WlogReadDto> CreateWlog(WlogCreateDto wlog)
        {
            var wlogModel = _mapper.Map<Wlog>(wlog);

            _repository.Add(wlogModel);
            _repository.Save();

            var readDto = _mapper.Map<WlogReadDto>(wlogModel);

            return CreatedAtRoute(nameof(GetWlogById), new { id = wlogModel.ID }, readDto);
        }

        //PUT api/Wlogs/{id}
        [HttpPut("{id}")]
        public ActionResult<WlogReadDto> UpdateWlog(int id, WlogUpdateDto dto)
        {
            var oldWlog = _repository.Find(id);
            if (oldWlog == null)
            {
                return NotFound();
            }

            _wlogHistoryRepository.Add(new WlogHistory() { Wlog = oldWlog, Content = oldWlog.PublishedContent });
            _wlogHistoryRepository.Save();

            _mapper.Map(dto, oldWlog);

            _repository.Save();

            return NoContent();// Ok(oldAcc);
        }

        //PATCH api/accounts/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchAccount(int id, JsonPatchDocument<WlogUpdateDto> patch)
        {
            var oldWlog = _repository.Find(id);
            if (oldWlog == null)
            {
                return NotFound();
            }
            _wlogHistoryRepository.Add(new WlogHistory() { Wlog = oldWlog, Content = oldWlog.PublishedContent });
            _repository.Save();

            var wlogToPatch = _mapper.Map<WlogUpdateDto>(oldWlog);
            patch.ApplyTo(wlogToPatch, ModelState);
            if (!TryValidateModel(wlogToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(wlogToPatch, oldWlog);

            _repository.Save();

            return NoContent();
        }

        //DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            var account = _repository.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            account.SysFields.IsDeleted = true;
            var history = _wlogHistoryRepository.Set().Where(x => x.Wlog.ID == id);
            if (history.Count() > 0)
            {
                foreach (var item in history)
                {
                    item.SysFields.IsDeleted = true;
                }
            }
            _repository.Save();

            return NoContent();
        }
    }
}
