﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Models.DataManager
{
    public class EstimerManager : IDataRepository<Estimer>
    {
        readonly BMWDBContext? bmwDBContext;

        //création des controlleur
        public EstimerManager() { }

        public EstimerManager(BMWDBContext context)
        {
            bmwDBContext = context;
        }

        // recherche toute les moto 
        public async Task<ActionResult<IEnumerable<Estimer>>> GetAllAsync()
        {
            return await bmwDBContext.Estimers.ToListAsync();
        }
        //recherche par ID moto
        public async Task<ActionResult<Estimer>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Estimer>> GetByIdAsync(int id , int id2)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Estimer>> GetByIdAsync(int id, int id2, int id3)
        {
            return await bmwDBContext.Estimers.FirstOrDefaultAsync(u => u.IdCompteClient == id && u.IdMoyenDePaiement == id2 && u.IdEstimationMoto == id3);
        }
        //recherche par nom de moto
        public async Task<ActionResult<Estimer>> GetByStringAsync(string nom)
        {
            throw new NotImplementedException();
        }
        //ajoute une moto 
        public async Task AddAsync(Estimer entity)
        {
            await bmwDBContext.Estimers.AddAsync(entity);
            await bmwDBContext.SaveChangesAsync();
        }
        //Mise à jour de la moto 
        public async Task UpdateAsync(Estimer Estimer, Estimer entity)
        {
            bmwDBContext.Entry(Estimer).State = EntityState.Modified;

            Estimer.IdEstimationMoto = entity.IdEstimationMoto;
            Estimer.IdCompteClient = entity.IdCompteClient;
            Estimer.IdMoyenDePaiement = entity.IdMoyenDePaiement;


            await bmwDBContext.SaveChangesAsync();
        }

        //supprimer la moto
        public async Task DeleteAsync(Estimer Estimer)
        {
            bmwDBContext.Estimers.Remove(Estimer);
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
