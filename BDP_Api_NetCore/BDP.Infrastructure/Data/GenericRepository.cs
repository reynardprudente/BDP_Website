using BDP.Domain.Entities;
using BDP.Infrastructure.Data.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly DatabaseContext databaseContext;
        public GenericRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public void Add<TEntity>(TEntity entity)
        {
            this.databaseContext.Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities)
        {
            this.databaseContext.AddRange(entities);
        }

        public void SaveChanges()
        {
            this.databaseContext.SaveChanges();
        }


        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return this.databaseContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Delete<TEntity>(TEntity entity)
        {
            this.databaseContext.Remove(entity);
        }
    }
}
