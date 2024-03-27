using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class ColorisManager : IDataRepository<Coloris>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public ColorisManager() { }

        public ColorisManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Coloris>>> GetAllAsync()
        {
            return await bmwDBContext.Coloris.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Coloris>> GetByIdAsync(int id)
        {
            return await bmwDBContext.Coloris.FirstOrDefaultAsync(u => u.IdColoris == id);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Coloris>> GetByStringAsync(string nom)
        {
            return await bmwDBContext.Coloris.FirstOrDefaultAsync(u => u.NomColoris.ToUpper() == nom.ToUpper());
        }
        //ajoute une moto 
        public async Task AddAsync(Coloris entity)
        {
            await bmwDBContext.Coloris.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Coloris coloris, Coloris entity)
        {
            bmwDBContext.Entry(coloris).State = EntityState.Modified;

            coloris.IdColoris = entity.IdColoris;
            coloris.IdPhoto = entity.IdPhoto;
            coloris.NomColoris = entity.NomColoris;
            coloris.DescriptionColoris = entity.DescriptionColoris;
            coloris.PrixColoris = entity.PrixColoris;
            coloris.TypeColoris = entity.TypeColoris;
           

            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Coloris coloris)
        {
            bmwDBContext.Coloris.Remove(coloris);
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
