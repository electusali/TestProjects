using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestProjects.Core.Entities;

namespace TestProjects.Core.DataAccess
{
	public interface IEntitiyRepository<T> where T : class,IEntitiy,new()
	{
		T Add(T entity);
		Task<T> AddAsync(T entity);
		T Update(T entity);
		Task<T> UpdateAsync(T entity);
		void Delete(T entity);
		Task DeleteAsync(T entity);
		T GetT(Expression<Func<T, bool>> filter=null);
		List<T> GetAll(Expression<Func<T, bool>> filter = null);
	}
}
