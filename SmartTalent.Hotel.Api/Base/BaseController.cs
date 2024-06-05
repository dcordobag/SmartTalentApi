namespace SmartTalent.Hotel.Api.Base
{
    using Microsoft.AspNetCore.Mvc;
    using SmartTalent.Hotel.Api.Base.Models;
    using SmartTalent.Hotel.DataAccess.Interfaces;
    using System.Linq.Expressions;


    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="repoBusinessRules">Clase de BusinessRules de la entidad</param>
    public class BaseController<T, TImplementacion>(TImplementacion repoBusinessRules) : ControllerBase where T : class, new() where TImplementacion : IRepository<T>
    {
        /// <summary>
        /// usiness repo for the entity <TImplementacion></TImplementacion>
        /// </summary>
        protected readonly TImplementacion RepoBusinessRules = repoBusinessRules;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<ICollection<T>> GetListAsync()
        {
            return await RepoBusinessRules.SearchListAsync();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<T> GetAsync()
        {
            return await RepoBusinessRules.SearchAsync();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<T> PostAsync([FromBody] T objBase)
        {
            return await RepoBusinessRules.CreateAsync(objBase);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<bool?> PutAsync([FromBody] T objBase)
        {
            return await RepoBusinessRules.EditAsync(objBase);
        }

        #region Methods protected for the class

        protected async Task<T> GetGenericAsync(Expression<Func<T, bool>> expression)
        {
            return await RepoBusinessRules.SearchAsync(expression);

        }
        protected async Task<ICollection<T>> GetGenericListAsync(Expression<Func<T, bool>> expression)
        {
            return await RepoBusinessRules.SearchListAsync(expression);
        }

        protected async Task<T> SearchLastAsync(Expression<Func<T, bool>> expression)
        {
            return await RepoBusinessRules.SearchLastAsync(expression);
        }

        protected async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await RepoBusinessRules.CountAsync(expression);
        }

        protected async Task<bool?> DeleteGenericAsync(Expression<Func<T, bool>> expression)
        {
            return await RepoBusinessRules.DeleteAsync(expression);
        }

        #endregion
    }
}
