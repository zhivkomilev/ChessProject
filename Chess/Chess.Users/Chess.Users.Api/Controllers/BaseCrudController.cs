﻿using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.EntityModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TService, TModel, TEntity, TRepositoryType> : Controller
        where TEntity: class, IBaseEntity
        where TModel: BaseModel
        where TRepositoryType: BaseRepository<TEntity>
        where TService : IBaseEntityService<TEntity, TModel, TRepositoryType>
    {
        protected readonly TService _service;

        protected BaseCrudController(TService service)
            => _service = service;
        
        [HttpGet("get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _service.GetByIdAsync(id);
            
            return Ok(model);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert(TModel model)
        {
            await _service.InsertAsync(model);
            await _service.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(TModel model)
        {
            await _service.UpdateAsync(model);
            await _service.SaveChangesAsync();
            
            return Ok();
        }
    }
}