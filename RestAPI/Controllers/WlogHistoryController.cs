using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Data.Interfaces;
using RestAPI.DTOs.WlogHistoryDTOs;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WlogHistoryController : ControllerBase
    {
        private readonly IWlogHistoryRepository _repository;
        private readonly IMapper _mapper;

        public WlogHistoryController(IWlogHistoryRepository wlogHistoryRepository, IMapper mapper)
        {
            _repository = wlogHistoryRepository;
            _mapper = mapper;
        }

        //GET api/WlogHistory/{id}
        [HttpGet("{id}", Name = "GetWlogHistoryById")]
        public ActionResult<WlogHistoryReadDto> GetWlogHistoryById(int id)
        {
            var history = _repository.Set().Where(x => x.Wlog.ID == id);
            if (history != null && history.Any(x=> x.SysFields.IsDeleted != true))
            {
                return Ok(_mapper.Map<WlogHistoryReadDto>(history));
            }
            return NotFound();
        }

        //POST api/wloghistory/{id}
        [HttpPost("{id}")]
        public ActionResult<WlogHistoryReadDto> CreateAccount(int id,WlogHistoryCreateDto acc)
        {
            var wlogHistoryModel = _mapper.Map<WlogHistory>(acc);
            _repository.Add(wlogHistoryModel);
            _repository.Save();

            var readDto = _mapper.Map<WlogHistoryReadDto>(wlogHistoryModel);

            return CreatedAtRoute(nameof(GetWlogHistoryById), new { id = wlogHistoryModel.ID }, readDto);
        }
    }
}
