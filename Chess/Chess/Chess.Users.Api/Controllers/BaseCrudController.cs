﻿using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.Models.EntityModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TService, TModel, TEntity, TRepositoryType> : Controller
        where TEntity : class, IBaseEntity
        where TModel : class, IBaseModel
        where TService : IBaseEntityService<TEntity, TModel, TRepositoryType>
        where TRepositoryType : BaseRepository<TEntity>
    {
        protected readonly TService _service;
        protected readonly ILogger _logger;

        protected BaseCrudController(TService service,
            ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (id == null || id == default)
                    return BadRequest();

                var model = await _service.GetByIdAsync(id);
                if (model == null)
                    return NotFound();

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("insert")]
        public virtual async Task<IActionResult> Insert(TModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var savedModel = await _service.InsertAsync(model);
                if (savedModel == null)
                    return BadRequest();

                await _service.SaveChangesAsync();

                return Created(Request.Path.Value, savedModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(TModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var updatedModel = await _service.UpdateAsync(model);
                if (updatedModel == null)
                    return StatusCode(500, $"Update failed.");

                await _service.SaveChangesAsync();

                return Ok(updatedModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == null || id == default)
                {
                    return BadRequest();
                }

                await _service.DeleteAsync(id);
                await _service.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        #region Dispose pattern
        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);


            if (_disposed || !disposing) return;

            _service.Dispose();
            _disposed = true;
        }
        #endregion
    }
}