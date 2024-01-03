﻿using Souq.Core.DataBase;
using Souq.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Repositories
{
    public interface IGenericRepository<T>where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsyc();
        Task<T> GetByIdAsync(int Id);


        public Task<IEnumerable<T>> GetAllAsycWithSpec(ISpecification<T> Spec);
        Task<T> GetByIdAsyncWithSpec(ISpecification<T> Spec);



    }
}
