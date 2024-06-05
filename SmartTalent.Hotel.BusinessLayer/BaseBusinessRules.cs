namespace SmartTalent.Hotel.BusinessLayer
{
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;
    using SmartTalent.Hotel.DataAccess.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Transactions;

    public abstract partial class BaseBusinessRules<T, TImplementacion> : IRepository<T>
       where T : class, new()
    where TImplementacion : IRepository<T>
    {
        /// <summary>
        /// Entidad que contiene el contexto de Dao de la Entidad <typeparamref name="T"/>
        /// </summary>
        protected readonly TImplementacion DaoNegocio;

        /// <summary>
        /// Referencia del Contexto de la Base de Datos
        /// </summary>
        public IDatabaseContext RepositoryContext => DaoNegocio.RepositoryContext;

        /// <summary>
        /// Metodo Constructor
        /// </summary>
        /// <param name="daoNegocio">Referencia del negocio</param>
        protected BaseBusinessRules(TImplementacion daoNegocio)
        {
            DaoNegocio = daoNegocio;
        }
        #region Metodos Asincronos

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await DaoNegocio.SearchAsync(expression).ConfigureAwait(false);
        }
        public async Task<T> SearchAsync()
        {
            return await DaoNegocio.SearchAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<ICollection<T>> SearchListAsync(Expression<Func<T, bool>> expression)
        {
            return await DaoNegocio.SearchListAsync(expression).ConfigureAwait(false);
        }

        /// <summary>
        /// Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public async Task<T> CreateAsync(T objCreate)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.CreateAsync(objCreate).ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> EditAsync(T objEdit)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.EditAsync(objEdit).ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        /// <summary>
        /// Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var objDelete = Search(expression);
            if (objDelete != null)
            {
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var objReturn = await DaoNegocio.DeleteAsync(objDelete).ConfigureAwait(false);

                tran.Complete();
                return objReturn;
            }
            else
            {
                throw new Exception("No hay registro para eliminar");
            }
        }
        public T Search(Expression<Func<T, bool>> expression)
        {
            return DaoNegocio.Search(expression);
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            return DaoNegocio.Count(expression);
        }

        public async Task<bool?> DeleteAsync(T objDelete)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.DeleteAsync(objDelete).ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        public async Task<ICollection<T>> SearchListAsync()
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.SearchListAsync().ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        public async Task<T> SearchLastAsync(Expression<Func<T, bool>> expression)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.SearchLastAsync(expression).ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var objReturn = await DaoNegocio.CountAsync(expression).ConfigureAwait(false);

            tran.Complete();
            return objReturn;
        }

        #endregion
    }
}
