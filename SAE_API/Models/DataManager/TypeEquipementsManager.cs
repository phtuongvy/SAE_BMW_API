﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class TypeEquipementManager : IDataRepository<TypeEquipement>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public TypeEquipementManager() { }

        public TypeEquipementManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetAllAsync()
        {
            return await bmwDBContext.TypeEquipements.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<TypeEquipement>> GetByIdAsync(int id)
        {
            return await bmwDBContext.TypeEquipements.FirstOrDefaultAsync(u => u.IdTypeEquipement == id );
        }
        public async Task<ActionResult<TypeEquipement>> GetByIdAsync(int id, int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<TypeEquipement>> GetByIdAsync(int id, int? id2, int? id3)
        {
            throw new NotImplementedException();
        }
        //recherche par nom de moto
        public async Task<ActionResult<TypeEquipement>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(TypeEquipement entity)
        {
            await bmwDBContext.TypeEquipements.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(TypeEquipement TypeEquipement, TypeEquipement entity)
        {
            bmwDBContext.Entry(TypeEquipement).State = EntityState.Modified;

            TypeEquipement.IdTypeEquipement = entity.IdTypeEquipement;
            TypeEquipement.IdSurTypeEquipement = entity.IdSurTypeEquipement;
            TypeEquipement.NomTypeEquipement = entity.NomTypeEquipement;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(TypeEquipement TypeEquipement)
        {
            bmwDBContext.TypeEquipements.Remove(TypeEquipement);
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

        public Task<ActionResult<IEnumerable<TypeEquipement>>> GetByIdAsyncList(int id)
        {
            throw new NotImplementedException();
        }
    }
}
