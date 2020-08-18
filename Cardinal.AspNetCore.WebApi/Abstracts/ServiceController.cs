using Cardinal.AspNetCore.Identity;
using Cardinal.AspNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Cardinal.AspNetCore.WebApi.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Controllers
{
    /// <summary>
    /// Classe base para todos os controllers do sistema.
    /// </summary>
    /// <typeparam name="TService">Serviço associado ao controlador.</typeparam>
    public abstract class ServiceController<TService> : DefaultController where TService : IService
    {
        /// <summary>
        /// Serviço associado ao controlador.
        /// </summary>
        protected TService Service { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="loggerFactory">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public ServiceController(ILoggerFactory loggerFactory, IServiceProvider provider) : base(loggerFactory, provider)
        {
            this.Service = this.GetService<TService>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public ServiceController(ILogger logger, IServiceProvider provider) : base(logger, provider)
        {
            this.Service = this.GetService<TService>();
        }
    }

    /// <summary>
    /// Classe base para todos os controllers do sistema.
    /// </summary>
    /// <typeparam name="TService">Serviço associado ao controlador.</typeparam>
    /// <typeparam name="TEntity">Entidade associada ao controlador.</typeparam>
    public abstract class CardinalController<TService, TEntity> : DefaultController where TService : IService<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Serviço associado ao controlador.
        /// </summary>
        private IService<TEntity> Service { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="loggerFactory">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public CardinalController(ILoggerFactory loggerFactory, IServiceProvider provider) : base(loggerFactory, provider)
        {
            this.Service = this.GetService<TService>();
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public CardinalController(ILogger logger, IServiceProvider provider) : base(logger, provider)
        {
            this.Service = this.GetService<TService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Permission(Method.Get, PermissionValidationType.RequireOneOrMore)]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TEntity>> AllAsync()
        {
            try
            {
                var result = await this.Service.AllAsync();
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [Permission(Method.Get, PermissionValidationType.RequireOneOrMore)]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TEntity>> FindAsync(Guid id)
        {
            try
            {
                var result = await this.Service.FindAsync(id);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Permission(Method.Post, PermissionValidationType.RequireOneOrMore)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TEntity>> PostAsync(TEntity entity)
        {
            try
            {
                await this.Service.AddAsync(entity);
                return this.Ok(entity);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Permission(Method.Put, PermissionValidationType.RequireOneOrMore)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TEntity>> PutAsync(TEntity entity)
        {
            try
            {
                await Task.Run(() => this.Service.Update(entity));
                return this.Ok(entity);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [Permission(Method.Delete, PermissionValidationType.RequireOneOrMore)]
        public virtual async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await Task.Run(() => this.Service.Remove(id));
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDTO"></typeparam>
    public abstract class EntityController<TService, TEntity, TDTO> : DefaultController where TService : IService<TEntity> where TEntity : Entity where TDTO : BaseDTO
    {
        /// <summary>
        /// Serviço associado ao controlador.
        /// </summary>
        private IService<TEntity> Service { get; }

        private IMapper Mapper { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="loggerFactory">Instância do serviço de logs.</param>
        /// <param name="mapper"></param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public EntityController(ILoggerFactory loggerFactory, IMapper mapper, IServiceProvider provider) : base(loggerFactory, provider)
        {
            this.Service = this.GetService<TService>();
            this.Mapper = mapper;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="mapper"></param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public EntityController(ILogger logger, IMapper mapper, IServiceProvider provider) : base(logger, provider)
        {
            this.Service = this.GetService<TService>();
            this.Mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Permission(Method.Get, PermissionValidationType.RequireOneOrMore)]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TDTO>> AllAsync()
        {
            try
            {
                var entities = await this.Service.AllAsync();
                var result = this.Mapper.Map<IEnumerable<TDTO>>(entities);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [Permission(Method.Get, PermissionValidationType.RequireOneOrMore)]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TDTO>> FindAsync(Guid id)
        {
            try
            {
                var entities = await this.Service.FindAsync(id);
                var result = this.Mapper.Map<IEnumerable<TDTO>>(entities);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Permission(Method.Post, PermissionValidationType.RequireOneOrMore)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TDTO>> PostAsync(TDTO dto)
        {
            try
            {
                var requestEntity = this.Mapper.Map<TEntity>(dto);
                await this.Service.AddAsync(requestEntity);
                var responseDTO = this.Mapper.Map<TDTO>(requestEntity);
                return this.Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Permission(Method.Put, PermissionValidationType.RequireOneOrMore)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public virtual async Task<ActionResult<TDTO>> PutAsync(TDTO dto)
        {
            try
            {
                var requestEntity = this.Mapper.Map<TEntity>(dto);
                await Task.Run(() => this.Service.Update(requestEntity));
                var responseDTO = this.Mapper.Map<TDTO>(requestEntity);
                return this.Ok(responseDTO);
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [Permission(Method.Delete, PermissionValidationType.RequireOneOrMore)]
        public virtual async Task<ActionResult<TEntity>> DeleteAsync(Guid id)
        {
            try
            {
                await Task.Run(() => this.Service.Remove(id));
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.Error(ex);
            }
        }
    }
}
