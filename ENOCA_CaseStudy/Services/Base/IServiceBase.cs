using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ENOCA_CaseStudy.Services.Base
{
    public interface IServiceBase<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        Task<IEnumerable<TModel>> Get(Expression<Func<TModel, bool>> predicate = null);


        Task<TModel> GetById(int id);


        Task<TModel> Add(TModel model);


        Task<TModel> Update(TModel model);


        Task<string> Delete(int id);
    }
}
