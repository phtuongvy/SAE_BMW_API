using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class PhotoManager : IDataRepository<Photo>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public PhotoManager() { }

        public PhotoManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Photo>>> GetAllAsync()
        {
            return await bmwDBContext.Photos.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Photo>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Photos.FirstOrDefaultAsync(u => u.PhotoId == id);
        }

        public async Task<ActionResult<Photo>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<AChoisi>> GetByIdAsync(int id, int id2, int id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<Photo>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Photos.FirstOrDefaultAsync(u => u.LienPhoto.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Photo entity)
        {
            await bmwDBContext.Photos.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Photo photo, Photo entity)
        {
            bmwDBContext.Entry(photo).State = EntityState.Modified;

            photo.PhotoId = entity.PhotoId;
            photo.LienPhoto = entity.LienPhoto;
          

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Photo photo)
        {
            bmwDBContext.Photos.Remove(photo);
            await bmwDBContext.SaveChangesAsync();
        }
        public Task<ActionResult<Object>> GetByIdCustomAsync1(Int32 id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<Object>>> GetAllAsync1()
        {
            throw new NotImplementedException();
        }
    }
}
