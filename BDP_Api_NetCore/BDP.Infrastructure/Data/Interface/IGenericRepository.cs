using BDP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Infrastructure.Data.Interface
{
    public interface IGenericRepository
    {
        void Add<TEntity>(TEntity entity);

        void AddRange<TEntity>(IEnumerable<TEntity> entities);

        void Delete<TEntity>(TEntity entity);

        void SaveChanges();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
