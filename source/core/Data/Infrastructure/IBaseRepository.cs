using System;
using System.Linq;

namespace Color.Core.Data.Infrastructure
{
	public interface IBaseRepository<T>
	{
		IQueryable<T> GetAll();
		T Get(Guid id);
		void Save(T entity);
		void Delete(T entity);
	}
}