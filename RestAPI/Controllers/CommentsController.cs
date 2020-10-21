using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RestAPI.Data.Interfaces;
using RestAPI.DTOs.CommentDTOs;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _repository = commentRepository;
            _mapper = mapper;
        }

        //GET api/comments/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CommentReadDto>> GetCommentById(int id)
        {
            var comm = _repository.Find(id);
            if (comm == null && comm.SysFields.IsDeleted != true)
            {
                return Ok(_mapper.Map<CommentReadDto>(comm));
            }
            return NotFound();
        }

        //GET api/comments/user/{id}
        [HttpGet("user/{id}")]
        public ActionResult<CommentReadDto> GetCommentsByUserId(int id)
        {
            var comms = _repository.Set().Where(x => x.SysFields.IsDeleted != true && x.UserID == id).ToList();
            if (comms == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommentReadDto>>(comms));
        }

        //GET api/comments/wlog/{id}
        [HttpGet("wlog/{id}")]
        public ActionResult<CommentReadDto> GetCommentsByWlogId(int id)
        {
            var comms = _repository.Set().Where(x => x.SysFields.IsDeleted != true && x.WlogID == id);
            if (comms == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CommentReadDto>>(comms));
        }

        //POST api/comments
        [HttpPost("wlog/{id}")]
        public ActionResult<CommentReadDto> CreateComment(int id, CommentCreateDto com)
        {//Needs Authentication
            var comModel = _mapper.Map<Comment>(com);
            _repository.Add(comModel);
            _repository.Save();

            var readDto = _mapper.Map<CommentReadDto>(comModel);

            return CreatedAtRoute(nameof(GetCommentById), new { id = comModel.ID }, readDto);
        }

        //PUT api/comments/{id}
        [HttpPut("{id}")]
        public ActionResult<CommentReadDto> UpdateComment(int id, CommentUpdateDto dto)
        {
            var oldCom = _repository.Find(id);
            if (oldCom == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, oldCom);

            _repository.Save();

            return NoContent();// Ok(oldAcc);
        }

        //PATCH api/comments/{id}
        [HttpPatch("{id}")]
        public ActionResult PatchComment(int id, JsonPatchDocument<CommentUpdateDto> patch)
        {
            var oldCom = _repository.Find(id);
            if (oldCom == null)
            {
                return NotFound();
            }

            var comToPatch = _mapper.Map<CommentUpdateDto>(oldCom);
            patch.ApplyTo(comToPatch, ModelState);
            if (!TryValidateModel(comToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(comToPatch, oldCom);

            _repository.Save();

            return NoContent();
        }

        //DELETE api/comments/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteComment(int id)
        {
            var comment = _repository.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            comment.SysFields.IsDeleted = true;
            _repository.Save();

            return NoContent();
        }
    }
}
