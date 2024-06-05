namespace SmartTalent.Hotel.DataAccess
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using SmartTalent.Hotel.DataAccess.Database.Dto.Interfaces;
    using SmartTalent.Hotel.DataAccess.Database.Interfaces;
    using SmartTalent.Hotel.DataAccess.Interfaces;
    using System.Linq.Expressions;


    public abstract class Repository<T> : IRepository<T> where T : class, IMongoEntities, new()
    {
        protected IMongoCollection<T> _dbCollection;
        public IDatabaseContext RepositoryContext { get; protected set; }

        protected Repository(IDatabaseContext contexto)
        {
            if (contexto != null)
            {
                RepositoryContext = contexto;
                _dbCollection = RepositoryContext.GetCollection<T>(typeof(T).Name);
            }
        }

        /// <summary>
        /// Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public async Task<T> CreateAsync(T objCreate)
        {
            if (objCreate != null)
            {
                await this._dbCollection.InsertOneAsync(objCreate).ConfigureAwait(false);
            }

            return objCreate;
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> EditAsync(T objEdit)
        {
            bool? returnEdit = null;
            if (objEdit != null)
            {
                ObjectId objectId = new ObjectId(objEdit.Id);
                returnEdit = (await this._dbCollection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), objEdit).ConfigureAwait(false)).IsAcknowledged;
                return returnEdit;
            }

            return returnEdit;
        }

        public async Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression != null)
            {
                var objDelete = this._dbCollection.FindAsync(expression);
                if (objDelete != null)
                {
                    returnDelete = (await this._dbCollection.DeleteManyAsync(expression).ConfigureAwait(false)).IsAcknowledged;
                }
                else
                {
                    returnDelete = false;
                }
            }

            return returnDelete;
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            long returnCount = 0;
            if (expression != null)
            {
                FilterDefinition<T> filter = Builders<T>.Filter.And(expression);
                returnCount = this._dbCollection.Find(filter).CountDocuments();
            }

            return returnCount;
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            T obj = null;
            if (expression != null)
            {
                obj = await (await this._dbCollection.FindAsync(expression)).FirstOrDefaultAsync().ConfigureAwait(false);
            }

            return obj;
        }

        public async Task<T> SearchAsync()
        {
            T obj = await (await this._dbCollection.FindAsync(Builders<T>.Filter.Empty)).FirstOrDefaultAsync().ConfigureAwait(false);

            return obj;
        }

        public async Task<ICollection<T>> SearchListAsync(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
            {
                var obj = await this._dbCollection.FindAsync(expression).ConfigureAwait(false);
                return obj.ToList();
            }
            return new List<T>();
        }
        public async Task<ICollection<T>> SearchListAsync()
        {
            var obj = await this._dbCollection.FindAsync(Builders<T>.Filter.Empty).ConfigureAwait(false);
            return obj != null ? obj.ToList() : new List<T>();
        }

        public T Search(Expression<Func<T, bool>> expression)
        {
            T obj = null;
            if (expression != null)
            {
                obj = this._dbCollection.Find(expression).FirstOrDefault();
            }

            return obj;
        }

        public async Task<bool?> DeleteAsync(T objDelete)
        {
            bool? returnDelete = null;
            if (objDelete != null)
            {
                ObjectId objectId = new ObjectId(objDelete.Id);
                returnDelete = (await this._dbCollection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId)).ConfigureAwait(false)).IsAcknowledged;
            }

            return returnDelete;
        }

        public async Task<T> SearchLastAsync(Expression<Func<T, bool>> expression)
        {
            T obj = null;
            if (expression != null)
            {
                obj = await this._dbCollection.Find(expression).SortByDescending(c => c.Id).FirstOrDefaultAsync();
            }

            return obj;
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            var obj = await this._dbCollection.CountDocumentsAsync(expression).ConfigureAwait(false);
            return obj;
        }
    }
}
