using Microsoft.AspNetCore.Mvc;

namespace SAE_API.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();

        Task<ActionResult<object>> GetByIdCustomAsync1(int id);

        Task<ActionResult<TEntity>> GetByIdAsync(int id);

        Task<ActionResult<TEntity>> GetByIdAsync(int id, int id2);

        Task<ActionResult<TEntity>> GetByIdAsync(int id, int? id2 , int? id3);

        Task<ActionResult<TEntity>> GetByStringAsync(string str);

        Task<ActionResult<IEnumerable<TEntity>>> GetByIdAsyncList(int id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<ActionResult<IEnumerable<object>>> GetAllAsync1();
        
    }
}
