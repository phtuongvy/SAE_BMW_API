using Microsoft.EntityFrameworkCore;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // add injection dependency idatarepo
            builder.Services.AddScoped<IDataRepository<AChoisi>, AChoisiManager>();
            builder.Services.AddScoped<IDataRepository<AChoisiOption>, AChoisiOptionManager>();
            builder.Services.AddScoped<IDataRepository<Acquerir>, AcquerirManager>();
            builder.Services.AddScoped<IDataRepository<Adresse>, AdresseManager>();
            builder.Services.AddScoped<IDataRepository<APourCouleur>, APourCouleurManager>();
            builder.Services.AddScoped<IDataRepository<APourTaille>, APourTailleManager>();
            builder.Services.AddScoped<IDataRepository<APourValeur>, APourValeurManager>();
            builder.Services.AddScoped<IDataRepository<CaracteristiqueMoto>, CaracteristiqueMotoManager>();
            builder.Services.AddScoped<IDataRepository<CarteBancaire>, CarteBancaireManager>();
            builder.Services.AddScoped<IDataRepository<CategorieCaracteristiqueMoto>, CategorieCaracteristiqueMotoManager>();
            builder.Services.AddScoped<IDataRepository<Collection>, CollectionManager>();
            builder.Services.AddScoped<IDataRepository<Coloris>, ColorisManager>();
            builder.Services.AddScoped<IDataRepository<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepository<Commander>, CommanderManager>();
            builder.Services.AddScoped<IDataRepository<CompteAdmin>, CompteAdminManager>();
            builder.Services.AddScoped<IDataRepository<CompteClient>, CompteClientManager>();
            builder.Services.AddScoped<IDataRepository<CompteClientPrive>, CompteClientPriveManager>();
            builder.Services.AddScoped<IDataRepository<CompteClientProfessionnel>, CompteClientProfessionnelManager>();
            builder.Services.AddScoped<IDataRepository<ConfigurationMoto>, ConfigurationMotoManager>();
            builder.Services.AddScoped<IDataRepository<CouleurEquipement>, CouleurEquipementManager>();
            builder.Services.AddScoped<IDataRepository<DateLivraison>, DateLivraisonManager>();
            builder.Services.AddScoped<IDataRepository<DemanderContact>, DemanderContactManager>();
            builder.Services.AddScoped<IDataRepository<Detient>, DetientManager>();
            builder.Services.AddScoped<IDataRepository<Disposer>, DisposerManager>();
            builder.Services.AddScoped<IDataRepository<DonneesConstante>, DonneesConstanteManager>();
            builder.Services.AddScoped<IDataRepository<Effectuer>, EffectuerManager>();
            builder.Services.AddScoped<IDataRepository<Enregistrer>, EnregistrerManager>();
            builder.Services.AddScoped<IDataRepository<Equipement>, EquipementManager>();
            builder.Services.AddScoped<IDataRepository<EquipementAccessoire>, EquipementAccessoireManager>();
            builder.Services.AddScoped<IDataRepository<EquipementMotoOption>, EquipementMotoOptionManager>();
            builder.Services.AddScoped<IDataRepository<EquipementOption>, EquipementOptionManager>();
            builder.Services.AddScoped<IDataRepository<EstDans>, EstDanssManager>();
            builder.Services.AddScoped<IDataRepository<Estimer>, EstimerManager>();
            builder.Services.AddScoped<IDataRepository<Favoris>, FavorisManager>();
            builder.Services.AddScoped<IDataRepository<GammeMoto>, GammeMotoManager>();
            builder.Services.AddScoped<IDataRepository<Illustrer>, IllustrerManager>();
            builder.Services.AddScoped<IDataRepository<Moto>, MotoManager>();
            builder.Services.AddScoped<IDataRepository<MotoDisponible>, MotoDisponibleManager>();
            builder.Services.AddScoped<IDataRepository<MoyenDePaiement>, MoyenDePaiementManager>();
            builder.Services.AddScoped<IDataRepository<Pack>, PackManager>();
            builder.Services.AddScoped<IDataRepository<PeutContenir>, PeutContenirManager>();
            builder.Services.AddScoped<IDataRepository<PeutEquiper>, PeutEquiperManager>();
            builder.Services.AddScoped<IDataRepository<PeutUtiliser>, PeutUtiliserManager>();
            builder.Services.AddScoped<IDataRepository<Photo>, PhotoManager>();
            builder.Services.AddScoped<IDataRepository<Posseder>, PossederManager>();
            builder.Services.AddScoped<IDataRepository<Presente>, PresenteManager>();
            builder.Services.AddScoped<IDataRepository<PriseRendezvous>, PriseRendezvousManager>();
            builder.Services.AddScoped<IDataRepository<Provenance>, ProvenanceManager>();
            builder.Services.AddScoped<IDataRepository<RepriseMoto>, RepriseMotoManager>();
            builder.Services.AddScoped<IDataRepository<Reservation>, ReservationManager>();
            builder.Services.AddScoped<IDataRepository<Segement>, SegementManager>();
            builder.Services.AddScoped<IDataRepository<Stock>, StockManager>();
            builder.Services.AddScoped<IDataRepository<TailleEquipement>, TailleEquipementManager>();
            builder.Services.AddScoped<IDataRepository<Transation>, TransationManager>();
            builder.Services.AddScoped<IDataRepository<TypeEquipement>, TypeEquipementManager>();

            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<BMWDBContext>(options =>
              options.UseNpgsql(builder.Configuration.GetConnectionString("BMWDBContextRemote")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //}
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}