using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SAE_API.Models.EntityFramework;

namespace SAE_API.Models.EntityFramework
{
    public partial class BMWDBContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public BMWDBContext() { }
        public BMWDBContext(DbContextOptions<BMWDBContext> options)
            : base(options) { }

        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<APourCouleur> APourCouleurs { get; set; }
        public virtual DbSet<APourValeur> APourValeurs { get; set; }
        public virtual DbSet<CarteBancaire> CartesBancaires { get; set; }
        public virtual DbSet<Coloris> Coloris { get; set; }
        public virtual DbSet<Concessionnaire> Concessionnaires { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<Effectuer> Effectuers { get; set; }
        public virtual DbSet<EquipementAccessoire> EquipementAccessoires { get; set; }
        public virtual DbSet<EquipementMotoOption> EquipementMotoOptions { get; set; }
        public virtual DbSet<EstDans> EstDanss { get; set; } // Note: Vérifie le pluriel pour cette entité
        public virtual DbSet<Favoris> Favoriss { get; set; }
        public virtual DbSet<Illustrer> Illustrers { get; set; }
        public virtual DbSet<Pack> Packs { get; set; }
        public virtual DbSet<PeutUtiliser> PeutUtilisers { get; set; }
        public virtual DbSet<Presente> Presentes { get; set; } // Supposé en fonction du nom
        public virtual DbSet<PriseRendezvous> PriseRendezvouss { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Segement> Segements { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<TailleEquipement> TailleEquipements { get; set; }
        public virtual DbSet<Transation> Transations { get; set; }
        public virtual DbSet<APourTaille> APourTailles { get; set; }
        public virtual DbSet<CaracteristiqueMoto> CaracteristiqueMotos { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<ConfigurationMoto> ConfigurationMotos { get; set; }
        public virtual DbSet<CompteClient> CompteClients { get; set; }
        public virtual DbSet<CouleurEquipement> CouleurEquipements { get; set; }
        public virtual DbSet<Equipement> Equipements { get; set; }
        public virtual DbSet<EquipementOption> EquipementOptions { get; set; }
        public virtual DbSet<GammeMoto> GammeMotos { get; set; }
        public virtual DbSet<Moto> Motos { get; set; }
        public virtual DbSet<MoyenDePaiement> MoyensDePaiement { get; set; }
        public virtual DbSet<PeutContenir> PeutContenirs { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Provenance> Provenances { get; set; }
        public virtual DbSet<Acquerir> Acquerirs { get; set; }

        //Je me suis arrete la 
        public virtual DbSet<AChoisi> Achoisis { get; set; }
        public virtual DbSet<AChoisiOption> AChoisiOptions { get; set; }
        public virtual DbSet<CategorieCaracteristiqueMoto> CategorieCaracteristiqueMotos { get; set; }
        public virtual DbSet<Commander> Commanders { get; set; }
        public virtual DbSet<CompteAdmin> CompteAdmins { get; set; }
        public virtual DbSet<CompteClientPrive> CompteClientPrives { get; set; }
        public virtual DbSet<CompteClientProfessionnel> CompteClientProfessionnels { get; set; }
        public virtual DbSet<DateLivraison> DateLivraisons { get; set; }
        public virtual DbSet<DemanderContact> DemanderContacts { get; set; }
        public virtual DbSet<Detient> Detients { get; set; }
        public virtual DbSet<Disposer> Disposers { get; set; }
        public virtual DbSet<DonneesConstante> DonneesConstantes { get; set; }
        public virtual DbSet<Enregistrer> Enregistrers { get; set; }
        public virtual DbSet<Estimer> Estimers { get; set; }
        public virtual DbSet<MotoDisponible> MotoDisponibles { get; set; }
        public virtual DbSet<PeutEquiper> PeutEquipers { get; set; }
        public virtual DbSet<Posseder> Posseders { get; set; }
        public virtual DbSet<RepriseMoto> RepriseMotos { get; set; }
        public virtual DbSet<TypeEquipement> TypeEquipements { get; set; }




        // Ajoutez d'autres DbSet pour les autres entités de votre modèle

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=SAE_BMW; uid=postgres; password=postgres;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurez ici les relations, les clés primaires composées, etc.

            // TABLE CATEGORIE CARACTERISTIQUE MOTO
            // C'EST BON
            modelBuilder.Entity<CategorieCaracteristiqueMoto>(entity =>
            {
                entity.ToTable("t_e_categoriecaracteristiquemoto_ccm");

                entity.HasKey(e => e.IdCategorieCaracteristiqueMoto)
                    .HasName("pk_categoriecaracteristiquemot");

                entity.Property(e => e.NomCategorieCaracteristiqueMoto)
                    .HasMaxLength(50)
                    .HasColumnName("ccm_nomcategoriecaracteristiquemoto");

                entity.HasMany(d => d.CaracteristiquesMotoCategorieCaracteristiqueMoto)
                    .WithOne(p => p.CategorieCaracteristiqueMotoCaracteristiqueMoto)
                    .HasForeignKey(d => d.IdCategorieCaracteristiqueMoto)
                    .HasConstraintName("ccm_nomcategoriecaracteristiquemoto");
            });

            // TABLE CARACTERISTIQUE MOTO
            // C'EST BON
            modelBuilder.Entity<CaracteristiqueMoto>(entity =>
            {
                entity.ToTable("t_e_caracteristiquemoto_crm");

                entity.HasKey(e => e.IdCaracteristiqueMoto)
                    .HasName("pk_caracteristiquemoto");

                entity.Property(e => e.IdCategorieCaracteristiqueMoto)
                    .IsRequired()
                    .HasColumnName("crm_idcategoriecaracteristiquemoto");

                entity.Property(e => e.NomCaracteristiqueMoto)
                    .HasMaxLength(100)
                    .HasColumnName("crm_nomcaracteristiquemoto");

                entity.Property(e => e.ValeurCaracteristiqueMoto)
                    .HasMaxLength(100)
                    .HasColumnName("crm_valeurcaracteristiquemoto");

                entity.HasOne(d => d.CategorieCaracteristiqueMotoCaracteristiqueMoto)
                    .WithMany(p => p.CaracteristiquesMotoCategorieCaracteristiqueMoto)
                    .HasForeignKey(d => d.IdCategorieCaracteristiqueMoto)
                    .HasConstraintName("fk_caracter_apourcate_categori");

                entity.HasMany(d => d.APourValeurCaracteristiqueMoto)
                    .WithOne(p => p.CaracteristiqueMotoPourValeur)
                    .HasForeignKey(d => d.IdCaracteristiqueMoto)
                    .HasConstraintName("fk_apourval_apourvale_caracter");
            });

            //TABLE Photo
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("t_e_photo_pho");

                entity.HasKey(e => e.PhotoId)
                    .HasName("PK_PHOTO");

                entity.Property(e => e.PhotoId).ValueGeneratedOnAdd();

                entity.Property(e => e.LienPhoto)
                    .HasColumnName("pho_lienphoto")
                    .HasMaxLength(1000);

                // Configuration de la relation avec Coloris
                entity.HasOne(p => p.ColorisPhoto)
                    .WithOne(c => c.PhotoColoris)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_COLORIS_PHOTO");

                // Configuration de la relation avec Presente
                entity.HasMany(p => p.PresentePhoto)
                    .WithOne(pr => pr.PhotoPresente)
                    .HasForeignKey(pr => pr.IdPhoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PRESENTE_PHOTO");

                // Configuration de la relation avec Illustrer
                entity.HasMany(p => p.IllustrerPhoto)
                    .WithOne(il => il.PhotoIllustrer)
                    .HasForeignKey(il => il.IdPhoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ILLUSTRER_PHOTO");

                // Configuration de la relation avec EquipementMotoOption
                entity.HasMany(p => p.EquipementMotoOptionPhoto)
                    .WithOne(emo => emo.PhotoEquipementMotoOption)
                    .HasForeignKey(emo => emo.IdPhoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_EQUIPEMENTMOTOOPTION_PHOTO");
            });

            // TABLE COLORIS
            // C'EST BON
            modelBuilder.Entity<Coloris>(entity =>
            {
                entity.ToTable("t_e_coloris_col");

                entity.HasKey(e => e.IdColoris)
                    .HasName("pk_t_e_coloris_col");

                entity.Property(e => e.NomColoris)
                    .HasMaxLength(50)
                    .HasColumnName("col_nomcoloris");

                entity.Property(e => e.DescriptionColoris)
                    .HasMaxLength(500)
                    .HasColumnName("col_descriptioncoloris");

                entity.Property(e => e.PrixColoris)
                    .HasColumnName("col_prixcoloris");

                entity.Property(e => e.TypeColoris)
                    .HasColumnName("col_typecoloris");

                entity.HasOne(d => d.PhotoColoris)
                    .WithOne(p => p.ColorisPhoto)
                    .HasForeignKey<Coloris>(d => d.IdPhoto)
                    .HasConstraintName("fk_t_e_coloris_col_refleter_photo");

                entity.HasMany(d => d.PeutContenirColoris)
                    .WithOne(p => p.ColorisPeutContenir)
                    .HasForeignKey(d => d.IdColoris)
                    .HasConstraintName("fk_peutcont_peutconte_t_e_coloris_col");

                entity.HasMany(d => d.ConfigurationsMotoColoris)
                    .WithOne(p => p.ColorisConfigurationMoto)
                    .HasForeignKey(d => d.IdColoris)
                    .HasConstraintName("fk_configur_disponibl_t_e_coloris_col");
            });

            // TABLE DATE LIVRAISON
            // C'EST BON
            modelBuilder.Entity<DateLivraison>(entity =>
            {
                entity.ToTable("t_e_datelivraison_dlv");

                entity.HasKey(e => e.IdDateLivraison)
                    .HasName("pk_datelivraison");

                entity.Property(e => e.Date)
                    .HasColumnName("dlv_date")
                    .HasColumnType("date");

                entity.HasMany(d => d.RepriseMotoDateLivraison)
                    .WithOne(p => p.DateLivraisonRepriseMoto)
                    .HasForeignKey(d => d.IdDateLivraison)
                    .HasConstraintName("fk_reprisem_estprevue_datelivr");
            });


            //TABLE GammeMoto
            modelBuilder.Entity<GammeMoto>(entity =>
            {
                entity.HasKey(e => e.IdGammeMoto)
                    .HasName("PK_GAMMEMOTO");

                entity.Property(e => e.NomGammeMoto)
                    .HasColumnName("gmm_nom")
                    .HasMaxLength(50);

                // Configuration de la relation one-to-many avec Moto
                entity.HasMany(gm => gm.MotoGammeMoto)
                    .WithOne(m => m.GammeMotoMoto)
                    .HasForeignKey(m => m.IdGammeMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_MOTO_GAMMEMOTO");
            });

            //TABLE MoyenDePayement
            modelBuilder.Entity<MoyenDePaiement>(entity =>
            {
                entity.HasKey(e => e.IdMoyenDePaiement)
                    .HasName("PK_MOYENDEPAIEMENT");

                entity.Property(e => e.LibelleMoyenDePaiement)
                    .HasColumnName("mdp_libellemoyendepaiement")
                    .HasMaxLength(50);

                // Configuration de la relation avec Estimer
                entity.HasMany(mdp => mdp.EstimationMoyenDePaiement)
                    .WithOne(est => est.MoyenDePaiementEstimer)
                    .HasForeignKey(est => est.IdMoyenDePaiement)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ESTIMER_MOYENDEPAIEMENT");
            });

            //TABLE Pack
            modelBuilder.Entity<Pack>(entity =>
            {
                entity.HasKey(e => e.PackId)
                    .HasName("PK_PACK");

                entity.Property(e => e.NomPack)
                    .HasColumnName("pck_nompack")
                    .HasMaxLength(30);

                entity.Property(e => e.DescriptionPack)
                    .HasColumnName("pck_descriptionpack")
                    .HasMaxLength(500);

                entity.Property(e => e.PrixPack)
                    .HasColumnName("pck_prixpack")
                    .HasColumnType("numeric");

                // Configuration de la relation avec AChoisi
                entity.HasMany(p => p.ChoisiPack)
                    .WithOne(a => a.PackChoisi)
                    .HasForeignKey(a => a.IDPack)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela selon les besoins de ta logique métier
                    .HasConstraintName("FK_ACHOISI_PACK");

                // Configuration de la relation avec PeutEquiper
                entity.HasMany(p => p.PeutEquiperPack)
                    .WithOne(pe => pe.PackPeutEquiper)
                    .HasForeignKey(pe => pe.IdPack)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTEQUIPER_PACK");

                // Configuration de la relation avec PeutUtiliser
                entity.HasMany(p => p.PeutUtiliserPack)
                    .WithOne(pu => pu.PackPeutUtiliser)
                    .HasForeignKey(pu => pu.IdPack)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTUTILISER_PACK");
            });

            // TABLE Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.IdStock)
                    .HasName("PK_STOCK");

                // Configuration de la relation entre Stock et Concessionnaire
                entity.HasMany(s => s.ConcessionnaireStock)
                    .WithOne(c => c.StockConcessionnaire)
                    .HasForeignKey(c => c.IdStock) // Assure-toi que Concessionnaire contient une propriété IdStock
                    .HasConstraintName("FK_CONCESSIONNAIRE_STOCK");

                // Configuration de la relation entre Stock et Concessionnaire
                entity.HasMany(s => s.EstDansStock)
                    .WithOne(c => c.StockEstDans)
                    .HasForeignKey(c => c.IdStock) // Assure-toi que Concessionnaire contient une propriété IdStock
                    .HasConstraintName("FK_ESTDANS_STOCK");

                // Configuration de la relation entre Stock et Disposer
                // À noter : Cela suppose que tu aies une table Disposer qui a une relation avec Stock
                entity.HasMany(s => s.DisposerStock)
                    .WithOne(d => d.StockDisposer)
                    .HasForeignKey(d => d.IdStock) // Assure-toi que Disposer contient une propriété IdStock
                    .HasConstraintName("FK_DISPOSER_STOCK");
            });

            // TABLE COLLECTION
            // C'EST BON
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("t_e_collection_clt");

                entity.HasKey(e => e.IdCollection)
                    .HasName("pk_collection");

                entity.Property(e => e.NomCollection)
                    .HasMaxLength(50)
                    .HasColumnName("clt_nomcollection");

                entity.HasMany(d => d.EquipementCollection)
                    .WithOne(p => p.CollectionEquipement)
                    .HasForeignKey(d => d.IdCollection)
                    .HasConstraintName("fk_equipeme_a_pour_co_collecti");
            });

            // TABLE COULEUR EQUIPEMENT
            // C'EST BON
            modelBuilder.Entity<CouleurEquipement>(entity =>
            {
                entity.ToTable("t_e_couleurequipement_ceq");

                entity.HasKey(e => e.IdCouleurEquipement)
                    .HasName("pk_t_e_couleurequipement_ceq");

                entity.Property(e => e.NomCouleurEquipement)
                    .HasColumnName("ceq_nomcouleurequipement")
                    .HasMaxLength(50);

                entity.HasMany(d => d.APourCouleurCouleurEquipement)
                    .WithOne(p => p.CouleurEquipementAPourCouleur)
                    .HasForeignKey(d => d.IdCouleurEquipement)
                    .HasConstraintName("fk_a_pour_c_a_pour_co_couleure");

                entity.HasMany(d => d.PresenteCouleurEquipement)
                    .WithOne(p => p.CouleurEquipementPresente)
                    .HasForeignKey(d => d.IdCouleurEquipement)
                    .HasConstraintName("fk_presente_presente3_couleure");
            });

            // TABLE TailleEquipement
            modelBuilder.Entity<TailleEquipement>(entity =>
            {
                entity.HasKey(e => e.IdTailleEquipement)
                    .HasName("PK_TAILLEEQUIPEMENT");

                entity.Property(e => e.NomTailleEquipement)
                    .HasColumnName("tel_nomtailleequipement")
                    .HasMaxLength(50);

                entity.HasMany(e => e.APourTailleTailleEquipement)
                    .WithOne(e => e.TailleEquipementAPourTaille)
                    .HasForeignKey(e => e.IdTailleEquipement)
                    .HasConstraintName("FK_EQUIPEMENT_TAILLEEQUIPEMENT");
            });

            // TABLE TYPEEQUIPEMENT
            modelBuilder.Entity<TypeEquipement>(entity =>
            {
                entity.HasKey(e => e.IdTypeEquipement)
                    .HasName("pk_typeequipement");

                entity.Property(e => e.IdSurTypeEquipement)
                    .HasColumnName("typ_idsurtypeequipement");

                entity.Property(e => e.NomTypeEquipement)
                    .HasColumnName("typ_nomtypeequipement")
                    .HasMaxLength(50);

                entity.HasMany(e => e.TypeEquipementTypeEquipement)
                    .WithOne(e => e.TypeEquipements)
                    .HasForeignKey(e => e.IdSurTypeEquipement)
                    .HasConstraintName("fk_typeequipement_surtypeequipement");

                // Configuration de la relation avec Equipement
                entity.HasMany(e => e.EquipementTypeEquipement)
                    .WithOne(e => e.TypeEquipementEquipement)
                    .HasForeignKey(e => e.IdTypeEquipement)
                    .HasConstraintName("fk_equipement_typeequipement");
            });

            // TABLE Segement
            modelBuilder.Entity<Segement>(entity =>
            {
                entity.HasKey(e => e.IdSegement)
                    .HasName("PK_SEGEMENT");

                entity.Property(e => e.NomSegement)
                    .HasColumnName("seg_nomsegement")
                    .HasMaxLength(50);

                // Si Segement a une relation one-to-many avec Equipement,
                // où un Segement peut avoir plusieurs équipements associés :
                entity.HasMany(s => s.EquipementSegement)
                    .WithOne(e => e.SegementEquipement)
                    .HasForeignKey(e => e.IdSegment) // Assure-toi que la propriété IdSegement est définie dans Equipement
                    .HasConstraintName("FK_EQUIPEMENT_SEGEMENT");
            });

            //TABLE EQUIPEMENT
            modelBuilder.Entity<Equipement>(entity =>
            {
                entity.HasKey(e => e.IdEquipement)
                    .HasName("PK_EQUIPEMENT");

                entity.Property(e => e.NomEquipement)
                    .HasColumnName("eqp_nomequipement")
                    .HasMaxLength(100);

                entity.Property(e => e.DescriptionEquipement)
                    .HasColumnName("eqp_descriptionequipement")
                    .HasMaxLength(500);

                entity.Property(e => e.DetailEquipement)
                    .HasColumnName("eqp_detailequipement")
                    .HasMaxLength(500);

                entity.Property(e => e.DureeEquipement)
                    .HasColumnName("eqp_dureeequipement")
                    .HasMaxLength(100);

                entity.Property(e => e.PrixEquipement)
                    .HasColumnName("eqp_prixequipement")
                    .HasColumnType("numeric");

                entity.Property(e => e.Sexe)
                    .HasColumnName("eqp_sexe")
                    .HasMaxLength(1)
                    .IsRequired();

                // Relation avec Segement
                entity.HasOne(e => e.SegementEquipement)
                    .WithMany(s => s.EquipementSegement)
                    .HasForeignKey(e => e.IdSegment)
                    .HasConstraintName("FK_EQUIPEMENT_SEGMENT");

                // Relation avec Collection
                entity.HasOne(e => e.CollectionEquipement)
                    .WithMany(c => c.EquipementCollection)
                    .HasForeignKey(e => e.IdCollection)
                    .HasConstraintName("FK_EQUIPEMENT_COLLECTION");

                // Relation avec TypeEquipement
                entity.HasOne(e => e.TypeEquipementEquipement)
                    .WithMany(te => te.EquipementTypeEquipement)
                    .HasForeignKey(e => e.IdTypeEquipement)
                    .HasConstraintName("FK_EQUIPEMENT_TYPEEQUIPEMENT");

                // Relations avec Presente, APourTaille, APourCouleur, Commander, Detient, Disposer
                entity.HasMany(e => e.PresenteEquipement)
                    .WithOne(p => p.EquipementPresente)
                    .HasForeignKey(p => p.IdEquipement)
                    .HasConstraintName("FK_PRESENTE_EQUIPEMENT");

                entity.HasMany(e => e.APourTailleEquipement)
                    .WithOne(at => at.EquipementAPourTaille)
                    .HasForeignKey(at => at.IdEquipement)
                    .HasConstraintName("FK_APOURTAILLE_EQUIPEMENT");

                entity.HasMany(e => e.APourCouleurEquipement)
                    .WithOne(ac => ac.EquipementAPourCouleur)
                    .HasForeignKey(ac => ac.IdEquipement)
                    .HasConstraintName("FK_APOURCOULEUR_EQUIPEMENT");

                entity.HasMany(e => e.CommanderEquipement)
                    .WithOne(c => c.EquipementCommander)
                    .HasForeignKey(c => c.IdEquipement)
                    .HasConstraintName("FK_COMMANDER_EQUIPEMENT");

                entity.HasMany(e => e.DetientEquipement)
                    .WithOne(d => d.EquipementDetient)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("FK_DETIENT_EQUIPEMENT");

                entity.HasMany(e => e.DisposerEquipement)
                    .WithOne(dp => dp.EquipementDisposer)
                    .HasForeignKey(dp => dp.IdEquipement)
                    .HasConstraintName("FK_DISPOSER_EQUIPEMENT");
            });

            // TABLE DISPOSER
            // C'EST BON
            modelBuilder.Entity<Disposer>(entity =>
            {
                entity.ToTable("t_e_disposer_dsp");

                entity.HasKey(e => new { e.IdEquipement, e.IdStock })
                    .HasName("pk_disposer");

                entity.Property(e => e.IdStock)
                    .HasColumnName("dsp_idstock");

                entity.Property(e => e.IdEquipement)
                    .HasColumnName("dsp_idequipement");

                entity.Property(e => e.QuantiteStock)
                    .HasColumnName("dsp_quantitestock");

                entity.HasOne(d => d.StockDisposer)
                    .WithMany(p => p.DisposerStock)
                    .HasForeignKey(d => d.IdStock)
                    .HasConstraintName("fk_disposer_disposer_stock");

                entity.HasOne(d => d.EquipementDisposer)
                    .WithMany(p => p.DisposerEquipement)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("fk_disposer_disposer2_equipeme");
            });

            // TABLE Presente
            modelBuilder.Entity<Presente>(entity =>
            {
                entity.HasKey(e => new { e.IdPhoto, e.IdEquipement, e.IdCouleurEquipement })
                    .HasName("PK_PRESENTE");

                // Configuration des relations avec Photo
                entity.HasOne(p => p.PhotoPresente)
                    .WithMany(photo => photo.PresentePhoto)
                    .HasForeignKey(p => p.IdPhoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PRESENTE_PHOTO");

                // Configuration des relations avec Equipement
                entity.HasOne(p => p.EquipementPresente)
                    .WithMany(equip => equip.PresenteEquipement)
                    .HasForeignKey(p => p.IdEquipement)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PRESENTE_EQUIPEMENT");

                // Configuration des relations avec CouleurEquipement
                entity.HasOne(p => p.CouleurEquipementPresente)
                    .WithMany(couleur => couleur.PresenteCouleurEquipement)
                    .HasForeignKey(p => p.IdCouleurEquipement)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PRESENTE_COULEUREQUIPEMENT");
            });


            // TABLE A POUR TAILLE
            // C'EST BON
            modelBuilder.Entity<APourTaille>(entity =>
            {
                entity.HasKey(e => new { e.IdEquipement, e.IdTailleEquipement })
                        .HasName("pk_a_pour_taille");

                entity.HasOne(d => d.EquipementAPourTaille)
                    .WithMany(p => p.APourTailleEquipement)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("fk_a_pour_t_a_pour_ta_equipeme")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TailleEquipementAPourTaille)
                    .WithMany(p => p.APourTailleTailleEquipement)
                    .HasForeignKey(d => d.IdTailleEquipement)
                    .HasConstraintName("fk_a_pour_t_a_pour_ta_tailleeq")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TABLE COMPTE CLIENT
            // C'EST BON
            modelBuilder.Entity<CompteClient>(entity =>
            {
                entity.ToTable("t_e_compteclient_ccl");

                entity.HasKey(e => e.IdCompteClient)
                    .HasName("pk_compteclient");

                entity.Property(e => e.NomClient)
                    .HasMaxLength(20)
                    .HasColumnName("ccl_nomclient");

                entity.Property(e => e.PrenomClient)
                    .HasMaxLength(20)
                    .HasColumnName("ccl_prenomclient");

                entity.Property(e => e.CiviliteClient)
                    .HasMaxLength(3)
                    .HasColumnName("ccl_civiliteclient");

                entity.Property(e => e.NumeroClient)
                    .HasMaxLength(20)
                    .HasColumnName("ccl_numeroclient");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("ccl_email");

                entity.Property(e => e.DatenaissanceClient)
                    .HasColumnType("date")
                    .HasColumnName("ccl_datenaissanceclient");

                entity.Property(e => e.Password)
                    .HasColumnName("ccl_password");

                entity.Property(e => e.ClientRole)
                    .HasColumnName("ccl_clientrole");

                entity.HasMany(d => d.EnregistrerCompteClient)
                    .WithOne(p => p.CompteClientEnregistrer)
                    .HasForeignKey(d => d.IdCompteClient)
                    .HasConstraintName("fk_enregist_enregistr_comptecl");

                entity.HasMany(d => d.EffectuerCompteClient)
                    .WithOne(p => p.CompteClientEffectuer)
                    .HasForeignKey(d => d.IdCompteClient)
                    .HasConstraintName("fk_t_e_effectuer_eff_t_e_effectuer_eff2_compte");

                entity.HasOne(d => d.AdresseCompteClient)
                    .WithOne(p => p.ClientAdresse)
                    .HasConstraintName("fk_adresse_sesitue_comptecl");

                // HERITAGE
                //entity.HasDiscriminator<string>("CompteType")
                //   .HasValue<CompteClientPrive>("CompteClientPrive")
                //   .HasValue<CompteClientProfessionnel>("CompteClientProfessionnel");
            });

            // TABLE COMPTE ADMIN
            //C'EST BON
            modelBuilder.Entity<CompteAdmin>(entity =>
            {
                entity.ToTable("t_e_compteadmin_cad");

                entity.HasKey(e => e.IdCompteClient)
                    .HasName("pk_compteadmin");

                entity.HasOne(d => d.CompteClientCompteAdmin)
                    .WithOne(p => p.CompteAdminCompteClient)
                    .HasForeignKey<CompteAdmin>(d => d.IdCompteClient);
            });


            // TABLE CLIENT PRIVE
            modelBuilder.Entity<CompteClientPrive>(entity =>
            {
                entity.ToTable("t_e_compteclientprive_cpp");

                entity.HasKey(e => e.IdCompteClient)
                    .HasName("pk_t_e_compteclientprive_cpp");


                entity.HasOne(d => d.CompteClientCompteClientPrive)
                    .WithOne(p => p.CompteClientPriveCompteClient)
                    .HasForeignKey<CompteClientPrive>(e => e.IdCompteClient)
                    .HasConstraintName("fk_comptecl_est_un_co_comptecl");
            });

            // TABLE CLIENT PROFESSIONNEL
            modelBuilder.Entity<CompteClientProfessionnel>(entity =>
            {
                entity.ToTable("t_e_compteclientprofessionnel_cpp");

                entity.HasKey(e => e.IdCompteClient)
                    .HasName("pk_compteclientprofessionnel");

                entity.Property(e => e.NomCompagnie)
                    .HasColumnName("cpp_nomcompagnie")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CompteClientCompteClientProfessionnel)
                    .WithOne(p => p.CompteClientProfessionnelCompteClient)
                    .HasForeignKey<CompteClientProfessionnel>(e => e.IdCompteClient)
                    .HasConstraintName("fk_comptecl_est_un_co_comptecl");
            });

            // TABLE TRANSATION
            modelBuilder.Entity<Transation>(entity =>
            {
                entity.HasKey(e => e.IdTransaction)
                    .HasName("PK_TRANSATION");

                entity.Property(e => e.IdCompteClient)
                    .HasColumnName("tra_idcompteclient")
                    .IsRequired(); // Annotation [Required] traduit en IsRequired dans le modèle Fluent API

                entity.Property(e => e.Montant)
                    .HasColumnName("tra_montant")
                    .HasColumnType("numeric")
                    .IsRequired(); // Annotation [Required] traduit en IsRequired dans le modèle Fluent API

                entity.Property(e => e.TypeDePayment)
                    .HasColumnName("tra_typdepayment")
                    .HasMaxLength(10)
                    .IsRequired(); // Annotation [Required] traduit en IsRequired dans le modèle Fluent API

                entity.Property(e => e.TypeDeTransaction)
                    .HasColumnName("tra_typedetransaction")
                    .HasMaxLength(10)
                    .IsRequired(); // Annotation [Required] traduit en IsRequired dans le modèle Fluent API

                // Configuration de la relation avec CompteClient
                entity.HasOne(d => d.CompteClientTransation)
                    .WithMany(p => p.TransationCompteClient) // Assure-toi que la propriété Transations existe dans CompteClient
                    .HasForeignKey(d => d.IdCompteClient)
                    .OnDelete(DeleteBehavior.Cascade) // ou .Restrict selon les règles métiers
                    .HasConstraintName("FK_TRANSATION_COMPTECLIENT");
            });


            // TABLE ADRESSE
            // C'EST BON
            modelBuilder.Entity<Adresse>(entity =>
            {

                entity.HasKey(e => e.IdAdresse)
                    .HasName("pk_adresse");

                entity.HasOne(d => d.ClientAdresse)
                    .WithOne(p => p.AdresseCompteClient)
                    .HasForeignKey<Adresse>(d => d.IdCompteClient)
                    .HasConstraintName("fk_adresse_sesitue_comptecl")
                    .IsRequired(false);

                entity.HasMany(d => d.ConcessionnairesAdresse)
                    .WithOne(p => p.AdresseConcessionnaire)
                    .HasForeignKey(e => e.IdConcessionnaire)
                    .HasConstraintName("fk_adresse_situer_concessi");
            });

            // TABLE CARTE BANCAIRE
            // C'EST BON
            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.ToTable("t_e_cartebancaire_cbt");

                entity.HasKey(e => e.IdCb)
                    .HasName("pk_cartebancaire");

                entity.Property(e => e.NomCarte)
                    .HasMaxLength(50)
                    .HasColumnName("cbt_nomcarte");

                entity.Property(e => e.NumeroCb)
                    .HasMaxLength(200)
                    .HasColumnName("cbt_numerocb");

                entity.Property(e => e.MoisExpiration)
                    .HasColumnName("cbt_moisexpiration");

                entity.Property(e => e.AnneeExpiration)
                    .HasColumnName("cbt_anneeexpiration");

                entity.Property(e => e.CryptoCb)
                    .HasMaxLength(200)
                    .HasColumnName("cbt_cryptocb");

                entity.HasMany(d => d.AcquisCB)
                    .WithOne(p => p.CBAcquis)
                    .HasForeignKey(d => d.IdCb)
                    .HasConstraintName("fk_acquerir_acquerir_cartebancaire");
            });

            // TABLE ACQUERIR
            // C'EST BON
            modelBuilder.Entity<Acquerir>(entity =>
            {
                entity.HasKey(e => new { e.IdCb, e.IdCompteClient })
                      .HasName("pk_acquerir");

                entity.HasOne(d => d.CBAcquis)
                    .WithMany(p => p.AcquisCB)
                    .HasForeignKey(d => d.IdCb)
                    .HasConstraintName("fk_acquerir_acquerir_cartebancaire")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ComptesAcquis)
                    .WithMany(p => p.AcquisComptes)
                    .HasForeignKey(d => d.IdCompteClient)
                    .HasConstraintName("fk_acquerir_acquerir2_configur")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TABLE COMMANDE
            // C'EST BON
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.ToTable("t_e_commande_cmd");

                entity.HasKey(e => e.IdCommande)
                    .HasName("pk_commande");

                entity.Property(e => e.PrixFraisLivraison)
                    .HasColumnName("cmd_prixfraislivraison");

                entity.Property(e => e.DateCommande)
                    .HasColumnType("date")
                    .HasColumnName("cmd_date");

                entity.Property(e => e.PrixTotal)
                    .HasColumnName("cmd_prixtotal");

                entity.HasMany(d => d.EffectuerCommande)
                    .WithOne(p => p.CommandeEffectuer)
                    .HasForeignKey(d => d.IdCommande)
                    .HasConstraintName("fk_t_e_effectuer_eff_t_e_effectuer_eff_commande");

                entity.HasMany(d => d.ProvenanceCommande)
                    .WithOne(p => p.CommandeProvenance)
                    .HasForeignKey(d => d.IdCommande)
                    .HasConstraintName("fk_provenance_provenance_commande");
            });

            // TABLE EFFECTUER
            // C'EST BON
            modelBuilder.Entity<Effectuer>(entity =>
            {
                entity.ToTable("t_e_effectuer_eff");

                entity.HasKey(e => new { e.IdCompteClient, e.IdCommande })
                    .HasName("PK_effectuer");

                entity.Property(e => e.IdCompteClient)
                    .HasColumnName("eff_idcompteclient");

                entity.Property(e => e.IdCommande)
                    .HasColumnName("eff_idcommande");

                entity.HasOne(d => d.CompteClientEffectuer)
                    .WithMany(p => p.EffectuerCompteClient)
                    .HasForeignKey(d => d.IdCompteClient)
                    .HasConstraintName("fk_t_e_effectuer_eff_t_e_effectuer_eff2_compte");

                entity.HasOne(d => d.CommandeEffectuer)
                    .WithMany(p => p.EffectuerCommande)
                    .HasForeignKey(d => d.IdCommande)
                    .HasConstraintName("fk_t_e_effectuer_eff_t_e_effectuer_eff_commande");
            });


            // TABLE CONCESSIONNAIRE
            // C'EST BON
            modelBuilder.Entity<Concessionnaire>(entity =>
            {
                entity.ToTable("t_e_concessionnaire_csn");

                entity.HasKey(e => e.IdConcessionnaire)
                    .HasName("pk_concessionnaire");

                entity.Property(e => e.IdAdresse)
                    .HasColumnName("csn_idadresse");

                entity.Property(e => e.IdStock)
                    .HasColumnName("csn_idstock");

                entity.Property(e => e.NomConcessionnaire)
                    .HasColumnName("csn_nom")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AdresseConcessionnaire)
                    .WithMany(p => p.ConcessionnairesAdresse)
                    .HasForeignKey(d => d.IdAdresse)
                    .HasConstraintName("fk_adresse_situer_concessi");

                entity.HasOne(d => d.StockConcessionnaire)
                    .WithMany(p => p.ConcessionnaireStock)
                    .HasForeignKey(d => d.IdStock)
                    .HasConstraintName("fk_concessi_stocker_stock");

                entity.HasMany(d => d.FavorisConcessionnaire)
                    .WithOne(p => p.ConcessionnaireFavoris)
                    .HasForeignKey(d => d.IdConcessionnaire)
                    .HasConstraintName("fk_favoris_favoris2_concessi");

                entity.HasMany(d => d.ProvenanceConcessionnaire)
                    .WithOne(p => p.ConcessionnaireProvenance)
                    .HasForeignKey(d => d.IdConcessionnaire)
                    .HasConstraintName("fk_provenance_provenance2_concessi");

                entity.HasMany(d => d.PriseRendezvousConcessionnaire)
                    .WithOne(p => p.ConcessionnairePriseRendezvous)
                    .HasForeignKey(d => d.IdConcessionnaire)
                    .HasConstraintName("fk_priseren_enregistr_concessi");

            });

            //TABLE EquipementMotoOption
            modelBuilder.Entity<EquipementMotoOption>(entity =>
            {
                entity.HasKey(e => e.IdEquipementMoto)
                    .HasName("PK_EQUIPEMENTMOTOOPTION");

                entity.Property(e => e.NomEquipement)
                    .HasColumnName("eqm_nomequipement")
                    .HasMaxLength(50);

                entity.Property(e => e.DescriptionEquipementMoto)
                    .HasColumnName("eqm_descriptionequipementmoto")
                    .HasMaxLength(500);

                entity.Property(e => e.PrixEquipementMoto)
                    .HasColumnName("eqm_prixequipementmoto");

                entity.Property(e => e.EquipementOrigine)
                    .HasColumnName("eqm_equipementorigine");

                // Configuration de la relation avec Photo
                entity.HasOne(em => em.PhotoEquipementMotoOption)
                    .WithMany(p => p.EquipementMotoOptionPhoto)
                    .HasForeignKey(em => em.IdPhoto)
                    
                    .HasConstraintName("FK_EQUIPEMENTMOTOOPTION_PHOTO");

                // Configuration de la relation avec AChoisiOption
                entity.HasMany(em => em.AChoisiOptionsEquipementMoto)
                    .WithOne(aco => aco.EquipementMotoChoisiOption)
                    .HasForeignKey(aco => aco.IdEquipementMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ACHOISIOPTION_EQUIPEMENTMOTOOPTION");

                // Configuration de la relation avec Posseder
                entity.HasMany(em => em.PossederEquipementMotoOption)
                    .WithOne(pos => pos.EquipementMotoOptionPosseder)
                    .HasForeignKey(pos => pos.IdEquipementMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_POSSEDER_EQUIPEMENTMOTOOPTION");

                entity.HasMany(em => em.PeutUtiliserEquipementMotoOption)
                    .WithOne(pu => pu.EquipementMotoOptionPeutUtiliser)
                    .HasForeignKey(pu => pu.IdEquipementMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTUTILISER_EQUIPEMENTMOTOOPTION");

                // HERITAGE
                //entity.HasDiscriminator<string>("EquipementType")
                //   .HasValue<EquipementOption>("EquipementOption")
                //   .HasValue<EquipementAccessoire>("EquipementAccessoire");
            });

            // TABLE Equipement Option
            modelBuilder.Entity<EquipementOption>(entity =>
            {
               
                entity.ToTable("t_e_equipementoption_eqo");


                entity.HasKey(e => e.IdEquipementMoto)
                        .HasName("pk_equipementoption");

                entity.HasOne(d => d.EquipementMotoOptionEquipementOption)
                    .WithOne(p => p.EquipementOptionEquipementMotoOption)
                    .HasForeignKey<EquipementOption>(d => d.IdEquipementMoto)
                    .HasConstraintName("fk_equipeme_est2_equipeme");

            });

            // TABLE EquipementAccessoire
            modelBuilder.Entity<EquipementAccessoire>(entity =>
            {
                entity.ToTable("t_e_equipementaccessoire_eqa");

                entity.HasKey(e => e.IdEquipementMoto)
                    .HasName("pk_equipementaccessoire");

                entity.HasOne(d => d.EquipementMotoOptionEquipementAccessoire)
                    .WithOne(p => p.EquipementAccessoireEquipementMotoOption)
                    .HasForeignKey<EquipementAccessoire>(d => d.IdEquipementMoto)
                    .HasConstraintName("fk_equipeme_est_equipeme");

            });

            // TABLE Provenance
            modelBuilder.Entity<Provenance>(entity =>
            {
                entity.HasKey(e => new { e.IdCommande, e.IdConcessionnaire })
                    .HasName("PK_PROVENANCE");

                // Configuration de la relation avec Commande
                entity.HasOne(p => p.CommandeProvenance)
                    .WithMany(c => c.ProvenanceCommande)
                    .HasForeignKey(p => p.IdCommande)
                    .OnDelete(DeleteBehavior.Cascade) // ou .Restrict selon tes besoins
                    .HasConstraintName("FK_PROVENANCE_COMMANDE");

                // Configuration de la relation avec Concessionnaire
                entity.HasOne(p => p.ConcessionnaireProvenance)
                    .WithMany(con => con.ProvenanceConcessionnaire)
                    .HasForeignKey(p => p.IdConcessionnaire)
                    .OnDelete(DeleteBehavior.Cascade) // ou .Restrict selon tes besoins
                    .HasConstraintName("FK_PROVENANCE_CONCESSIONNAIRE");
            });

            //TABLE Favoris
            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.IdCompteClient, e.IdConcessionnaire })
                    .HasName("PK_FAVORIS");

                // Configuration de la relation avec CompteClient
                entity.HasOne(f => f.CompteClientFavoris)
                    .WithMany(cc => cc.FavorisCompteClient)
                    .HasForeignKey(f => f.IdCompteClient)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela selon les besoins de ta logique métier
                    .HasConstraintName("FK_FAVORIS_COMPTECLIENT");

                // Configuration de la relation avec Concessionnaire
                entity.HasOne(f => f.ConcessionnaireFavoris)
                    .WithMany(con => con.FavorisConcessionnaire)
                    .HasForeignKey(f => f.IdConcessionnaire)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela selon les besoins de ta logique métier
                    .HasConstraintName("FK_FAVORIS_CONCESSIONNAIRE");
            });


            //TABLE Moto
            modelBuilder.Entity<Moto>(entity =>
            {
                entity.HasKey(e => e.MotoId)
                    .HasName("PK_MOTO");

                entity.Property(e => e.NomMoto)
                    .HasColumnName("mto_nommoto")
                    .HasMaxLength(50);

                entity.Property(e => e.DescriptionMoto)
                    .HasColumnName("mto_descriptionmoto")
                    .HasMaxLength(500);

                entity.Property(e => e.PrixMoto)
                    .HasColumnName("mto_prixmoto");

                // Relation avec GammeMoto
                entity.HasOne(m => m.GammeMotoMoto)
                    .WithMany(gm => gm.MotoGammeMoto)
                    .HasForeignKey(m => m.IdGammeMoto)
                    .OnDelete(DeleteBehavior.Restrict) // ou .Cascade selon tes besoins
                    .HasConstraintName("FK_MOTO_GAMMEMOTO");

                // Relations avec APourValeur
                entity.HasMany(m => m.APourValeurMoto)
                    .WithOne(apv => apv.MotoAPourValeur)
                    .HasForeignKey(apv => apv.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_APOURVALEUR");

                // Relations avec PeutContenir
                entity.HasMany(m => m.PeutContenirMoto)
                    .WithOne(pc => pc.MotoPeutContenir)
                    .HasForeignKey(pc => pc.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_PEUTCONTENIR");

                // Relations avec PeutEquiper
                entity.HasMany(m => m.PeutEquiperMoto)
                    .WithOne(pe => pe.MotoPeutEquiper)
                    .HasForeignKey(pe => pe.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_PEUTEQUIPER");

                // Relations avec ConfigurationMoto
                entity.HasMany(m => m.ConfigurationMotoMoto)
                    .WithOne(cm => cm.MotoConfigurationMoto)
                    .HasForeignKey(cm => cm.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_CONFIGURATIONMOTO");

                // Relation one-to-one avec MotoDisponible
                entity.HasOne(m => m.MotoDisponibleMoto)
                    .WithOne(md => md.MotoMotoDisponible)
                    .HasForeignKey<MotoDisponible>(md => md.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_MOTODISPONIBLE");

                // Relations avec Posseder
                entity.HasMany(m => m.PossederMoto)
                    .WithOne(p => p.MotoPosseder)
                    .HasForeignKey(p => p.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_POSSEDER");

                // Relations avec EstDans
                entity.HasMany(m => m.EstDansMoto)
                    .WithOne(p => p.MotoEstDans)
                    .HasForeignKey(p => p.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTO_POSSEDER");
            });


            //TABLE Illustrer
            modelBuilder.Entity<Illustrer>(entity =>
            {
                entity.HasKey(e => new { e.IdMoto, e.IdPhoto })
                    .HasName("PK_ILLUSTRER");

                // Configuration de la relation avec Moto
                entity.HasOne(il => il.MotoIllustrer)
                    .WithMany(m => m.IllustrerMoto)
                    .HasForeignKey(il => il.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ILLUSTRER_MOTO");

                // Configuration de la relation avec Photo
                entity.HasOne(il => il.PhotoIllustrer)
                    .WithMany(p => p.IllustrerPhoto)
                    .HasForeignKey(il => il.IdPhoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ILLUSTRER_PHOTO");
            });

            //TABLE PeutEquiper
            modelBuilder.Entity<PeutEquiper>(entity =>
            {
                entity.HasKey(e => new { e.IdPack, e.IdMoto })
                    .HasName("PK_PEUTEQUIPER");

                // Configuration de la relation avec Pack
                entity.HasOne(pe => pe.PackPeutEquiper)
                    .WithMany(pack => pack.PeutEquiperPack)
                    .HasForeignKey(pe => pe.IdPack)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela en fonction de la logique de suppression voulue
                    .HasConstraintName("FK_PEUTEQUIPER_PACK");

                // Configuration de la relation avec Moto
                entity.HasOne(pe => pe.MotoPeutEquiper)
                    .WithMany(moto => moto.PeutEquiperMoto)
                    .HasForeignKey(pe => pe.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste cela en fonction de la logique de suppression voulue
                    .HasConstraintName("FK_PEUTEQUIPER_MOTO");
            });

            // TABLE COMMANDER
            // C'EST BON
            modelBuilder.Entity<Commander>(entity =>
            {
                entity.ToTable("t_e_commander_cmdr");

                entity.HasKey(e => e.IdCommande)
                    .HasName("pk_commander");

                entity.Property(e => e.IdCommande)
                    .HasColumnName("cmdr_idcommande");

                entity.Property(e => e.IdEquipement)
                    .HasColumnName("cmdr_idequipement");

                entity.Property(e => e.IdConfigurationMoto)
                    .HasColumnName("cmdr_idconfigurationmoto")
                    .IsRequired(false);

                entity.Property(e => e.QteCommande)
                    .HasColumnName("cmdr_qtecommande");

                entity.HasOne(d => d.EquipementCommander)
                    .WithMany(p => p.CommanderEquipement)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("fk_commande_commander_equipeme");

                entity.HasOne(d => d.ConfigurationMotoCommander)
                    .WithOne(p => p.CommanderConfigurationMoto)
                    .HasForeignKey<Commander>(d => d.IdConfigurationMoto)
                    .HasConstraintName("fk_commande_commander_configurationmoto");
            });

            //TABLE PeutUtiliser
            modelBuilder.Entity<PeutUtiliser>(entity =>
            {
                entity.HasKey(e => new { e.IdPack, e.IdEquipementMoto })
                    .HasName("PK_PEUTUTILISER");

                // Configuration de la relation avec Pack
                entity.HasOne(pu => pu.PackPeutUtiliser)
                    .WithMany(pack => pack.PeutUtiliserPack)
                    .HasForeignKey(pu => pu.IdPack)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTUTILISER_PACK");

                // Configuration de la relation avec Equipement
                entity.HasOne(pu => pu.EquipementMotoOptionPeutUtiliser)
                    .WithMany(equip => equip.PeutUtiliserEquipementMotoOption)
                    .HasForeignKey(pu => pu.IdEquipementMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTUTILISER_EQUIPEMENT");
            });

            // TABLE Posseder
            modelBuilder.Entity<Posseder>(entity =>
            {
                entity.HasKey(e => new { e.IdMoto, e.IdEquipementMoto })
                    .HasName("PK_POSSEDER");

                // Configuration de la relation avec Moto
                entity.HasOne(p => p.MotoPosseder)
                    .WithMany(m => m.PossederMoto)
                    .HasForeignKey(p => p.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSSEDER_MOTO");

                // Configuration de la relation avec EquipementMotoOption
                entity.HasOne(p => p.EquipementMotoOptionPosseder)
                    .WithMany(emo => emo.PossederEquipementMotoOption)
                    .HasForeignKey(p => p.IdEquipementMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_POSSEDER_EQUIPEMENTMOTOOPTION");
            });

            // TABLE PriseRendezvous
            modelBuilder.Entity<PriseRendezvous>(entity =>
            {
                entity.ToTable("t_e_priserendezvous_prv");

                entity.HasKey(e => e.IdReservationOffre)
                    .HasName("PK_PRISERENDEZVOUS");

                
                entity.Property(e => e.NomReservation)
                    .HasColumnName("prv_nomreservation")
                    .HasMaxLength(50);

                entity.Property(e => e.PrenomReservation)
                    .HasColumnName("prv_prenomreservation")
                    .HasMaxLength(50);

                entity.Property(e => e.CiviliteReservation)
                    .HasColumnName("prv_civilreservation")
                    .HasMaxLength(3);

                entity.Property(e => e.EmailReservation)
                    .HasColumnName("prv_emailreservation")
                    .HasMaxLength(100);

                entity.Property(e => e.TelephoneReservation)
                    .HasColumnName("prv_telephonereservation")
                    .HasMaxLength(100);

                entity.Property(e => e.VilleReservation)
                    .HasColumnName("prv_villereservation")
                    .HasMaxLength(50);

                entity.Property(e => e.TypeDePermis)
                    .HasColumnName("prv_typedepermis");

                // Configuration de la relation avec Concessionnaire
                entity.HasOne(prv => prv.ConcessionnairePriseRendezvous)
                    .WithMany(c => c.PriseRendezvousConcessionnaire)
                    .HasForeignKey(prv => prv.IdConcessionnaire)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PRISERENDEZVOUS_CONCESSIONNAIRE");
            });

            // TABLE Reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("t_e_reservation_res");

                entity.HasKey(e => e.IdReservationOffre)
                    .HasName("PK_RESERVATION");

                entity.Property(e => e.IdConcessionnaire)
                    .HasColumnName("res_idconcessionnaire");

                entity.Property(e => e.FinancementReservationOffre)
                    .HasColumnName("res_financementreservationoffre")
                    .HasMaxLength(20);

                entity.Property(e => e.AssuranceReservation)
                    .HasColumnName("res_assurancereservation")
                    .HasMaxLength(50);

                entity.HasOne(r => r.PriseRendezvousReservation)
                    .WithOne(dc => dc.ReservationPriseRendezvous)
                    .HasForeignKey<Reservation>(dc => dc.IdReservationOffre)
                    .HasConstraintName("fk_reservat_heritage__priseren");
            });

            // TABLE RepriseMoto
            modelBuilder.Entity<RepriseMoto>(entity =>
            {
                entity.HasKey(e => e.IdEstimationMoto)
                    .HasName("PK_REPRISEMOTO");

                entity.Property(e => e.MarqueEstimationMoto)
                    .HasColumnName("rpm_marqueestimationmoto")
                    .HasMaxLength(10);

                entity.Property(e => e.ModeleEstimationMoto)
                    .HasColumnName("rpm_modeleestimationmoto")
                    .HasMaxLength(10);

                entity.Property(e => e.MoisImmatriculation)
                    .HasColumnName("rpm_moisimmatriculation");

                entity.Property(e => e.AnneImmatriculation)
                    .HasColumnName("rpm_anneimmatriculation");

                entity.Property(e => e.PrixEstimationMoto)
                    .HasColumnName("rpm_prixestimationmoto")
                    .HasColumnType("numeric");

                entity.Property(e => e.KilometrageEstimationMoto)
                    .HasColumnName("rpm_kilometrageestimationmoto")
                    .HasColumnType("numeric");

                entity.Property(e => e.VersionEstimationMoto)
                    .HasColumnName("rpm_versionestimationmoto")
                    .HasMaxLength(5);

                // Relations avec DateLivraison
                entity.HasOne(rpm => rpm.DateLivraisonRepriseMoto)
                    .WithMany(dl => dl.RepriseMotoDateLivraison)
                    .HasForeignKey(rpm => rpm.IdDateLivraison)
                    .HasConstraintName("FK_REPRISEMOTO_DATELIVRAISON");

                // Relations avec CompteClient
                entity.HasOne(rpm => rpm.CompteClientRepriseMoto)
                    .WithMany(cc => cc.RepriseMotoCompteClient)
                    .HasForeignKey(rpm => rpm.IdCompteClient)
                    .HasConstraintName("FK_REPRISEMOTO_COMPTECLIENT");

                // Relations avec Estimer
                entity.HasOne(rpm => rpm.EstimerRepriseMoto)
                    .WithOne(e => e.RepriseMotoEstimer)
                    .HasForeignKey<Estimer>(e => e.IdEstimationMoto)
                    .HasConstraintName("FK_ESTIMER_REPRISEMOTO");
            });

            // TABLE CONFIGURATION MOTO
            // C'EST BON
            modelBuilder.Entity<ConfigurationMoto>(entity =>
            {
                entity.ToTable("t_e_configurationmoto_cfm");

                entity.HasKey(e => e.IdConfigurationMoto)
                    .HasName("pk_configurationmoto");

                entity.Property(e => e.IdReservationOffre)
                    .HasColumnName("cfm_idreservationoffre");

                entity.Property(e => e.IdMoto)
                    .HasColumnName("cfm_idmoto");

                entity.Property(e => e.IdColoris)
                    .HasColumnName("cfm_idcoloris");

                entity.Property(e => e.PrixTotalConfiguration)
                    .HasColumnName("cfm_prixtotalconfiguration")
                    .HasColumnType("numeric");

                entity.Property(e => e.DateConfiguration)
                    .HasColumnName("cfm_dateconfiguration")
                    .HasColumnType("date");

                entity.HasOne(d => d.MotoConfigurationMoto)
                    .WithMany(p => p.ConfigurationMotoMoto)
                    .HasForeignKey(d => d.IdMoto)
                    .HasConstraintName("fk_configur_estconfig_moto");

                entity.HasOne(d => d.ColorisConfigurationMoto)
                    .WithMany(p => p.ConfigurationsMotoColoris)
                    .HasForeignKey(d => d.IdColoris)
                    .HasConstraintName("fk_configur_disponibl_t_e_coloris_col");

                entity.HasMany(d => d.EnregistrerConfigurationMoto)
                    .WithOne(p => p.ConfigurationMotoEnregistrer)
                    .HasForeignKey(d => d.IdConfigurationMoto)
                    .HasConstraintName("fk_enregist_enregistr_configur");

                entity.HasMany(d => d.AChoisiConfigurationMoto)
                    .WithOne(p => p.ConfigurationMotoChoisi)
                    .HasForeignKey(d => d.IDConfigurationMoto)
                    .HasConstraintName("fk_achoisi_achoisi2_configur");

                entity.HasMany(d => d.AChoisiOptionsConfigurationMoto)
                    .WithOne(p => p.ConfigurationMotoChoisiOption)
                    .HasForeignKey(d => d.IdConfigurationMoto)
                    .HasConstraintName("fk_achoisio_achoisiop_configur");
            });

            // TABLE A CHOISI
            // C'EST BON
            modelBuilder.Entity<AChoisi>(entity =>
            {
                // KEY
                entity.HasKey(e => new { e.IDPack, e.IDConfigurationMoto })
                      .HasName("pk_achoisi");

                // RELATIONSHIP BETWEEN TABLES

                entity.HasOne(d => d.PackChoisi)
                    .WithMany(p => p.ChoisiPack)
                    .HasForeignKey(d => d.IDPack)
                    .HasConstraintName("fk_achoisi_achoisi_pack")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ConfigurationMotoChoisi)
                    .WithMany(p => p.AChoisiConfigurationMoto)
                    .HasForeignKey(d => d.IDConfigurationMoto)
                    .HasConstraintName("fk_achoisi_achoisi2_configur");

            });

            // TABLE DEMANDER CONTACT
            // C'EST BON
            modelBuilder.Entity<DemanderContact>(entity =>
            {
                entity.ToTable("t_e_demandercontact_dmc");

                entity.HasKey(e => e.IdReservationOffre)
                    .HasName("pk_demandercontact");

                entity.Property(e => e.ObjetDeLaDemande)
                    .HasColumnName("dmc_objetdelademande")
                    .HasMaxLength(100);

                entity.Property(e => e.Objet)
                    .HasColumnName("dmc_objet")
                    .HasMaxLength(50);

                entity.HasOne(d => d.PriseRendezvousDemanderContact)
                    .WithOne(p => p.DemanderContactPriseRendezVous)
                    .HasForeignKey<DemanderContact>(d => d.IdReservationOffre)
                    .HasConstraintName("fk_demander_heritage__priseren");
            });

            // TABLE DETIENT
            // C'EST BON
            modelBuilder.Entity<Detient>(entity =>
            {
                entity.ToTable("t_e_detient_det");

                entity.HasKey(e => new { e.IdEquipement, e.IdConcessionnaire })
                    .HasName("pk_detient");

                entity.Property(e => e.IdEquipement)
                    .HasColumnName("det_idequipement");

                entity.Property(e => e.IdConcessionnaire)
                    .HasColumnName("det_idconcessionnaire");

                entity.HasOne(d => d.EquipementDetient)
                    .WithMany(p => p.DetientEquipement)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("fk_detient_detient_equipement");

                entity.HasOne(d => d.ConcessionnaireDetient)
                    .WithMany(p => p.DetientConcessionnaire)
                    .HasForeignKey(d => d.IdConcessionnaire)
                    .HasConstraintName("fk_detient_detient2_concessi");
            });

            //table enregistrer

            modelBuilder.Entity<Enregistrer>(entity =>
            {
                entity.HasKey(e => new { e.IdConfigurationMoto, e.IdCompteClient })
                    .HasName("PK_ENREGISTRER");

                entity.Property(e => e.NomConfiguration)
                    .HasColumnName("enr_nomconfiguration")
                    .HasMaxLength(50);

                // Configuration de la relation avec ConfigurationMoto
                entity.HasOne(e => e.ConfigurationMotoEnregistrer)
                    .WithMany(cm => cm.EnregistrerConfigurationMoto)
                    .HasForeignKey(e => e.IdConfigurationMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajustez selon les besoins de votre logique métier
                    .HasConstraintName("FK_ENREGISTRER_CONFIGURATIONMOTO");

                // Configuration de la relation avec CompteClient
                entity.HasOne(e => e.CompteClientEnregistrer)
                    .WithMany(cc => cc.EnregistrerCompteClient)
                    .HasForeignKey(e => e.IdCompteClient)
                    .OnDelete(DeleteBehavior.Cascade) // Ajustez selon les besoins de votre logique métier
                    .HasConstraintName("FK_ENREGISTRER_COMPTECLIENT");
            });

            //TABLE EstDans
            modelBuilder.Entity<EstDans>(entity =>
            {
                entity.HasKey(e => new { e.IdMoto, e.IdStock })
                    .HasName("PK_ESTDANS");

                entity.Property(e => e.QuantiteStockDisponible)
                    .HasColumnName("esd_quantitestockdisponible");

                entity.Property(e => e.QuantiteStockMoto)
                    .HasColumnName("esd_quantitestockmoto");

                // Configuration de la relation avec Moto
                entity.HasOne(ed => ed.MotoEstDans)
                    .WithMany(m => m.EstDansMoto)
                    .HasForeignKey(ed => ed.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ESTDANS_MOTO");

                // Configuration de la relation avec Stock
                entity.HasOne(ed => ed.StockEstDans)
                    .WithMany(s => s.EstDansStock)
                    .HasForeignKey(ed => ed.IdStock)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ESTDANS_STOCK");
            });

            //TABLE Estimer
            modelBuilder.Entity<Estimer>(entity =>
            {
                entity.HasKey(e => new { e.IdCompteClient, e.IdEstimationMoto, e.IdMoyenDePaiement })
                    .HasName("PK_ESTIMER");

                // Configuration de la relation avec CompteClient
                entity.HasOne(e => e.CompteClientEstimer)
                    .WithMany(cc => cc.EstimerCompteClient)
                    .HasForeignKey(e => e.IdCompteClient)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ESTIMER_COMPTECLIENT");

                // La relation avec RepriseMoto n'est pas directement établie ici à cause de l'absence du modèle EstimationMoto dans votre description.
                // Supposons que IdEstimationMoto relie à RepriseMoto, configurez ainsi :
                entity.HasOne(e => e.RepriseMotoEstimer)
                    .WithOne(rm => rm.EstimerRepriseMoto)
                    .HasForeignKey<Estimer>(e => e.IdEstimationMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins
                    .HasConstraintName("FK_ESTIMER_REPRISEMOTO");

                // Configuration de la relation avec MoyenDePaiement
                entity.HasOne(e => e.MoyenDePaiementEstimer)
                    .WithMany(mdp => mdp.EstimationMoyenDePaiement)
                    .HasForeignKey(e => e.IdMoyenDePaiement)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_ESTIMER_MOYENDEPAIEMENT");
            });

            // TABLE A POUR VALEUR
            // C'EST BON
            modelBuilder.Entity<APourValeur>(entity =>
            {
                entity.HasKey(e => new { e.IdMoto, e.IdCaracteristiqueMoto })
                        .HasName("pk_apourvaleur");

                entity.HasOne(d => d.MotoAPourValeur)
                    .WithMany(p => p.APourValeurMoto)
                    .HasForeignKey(d => d.IdMoto)
                    .HasConstraintName("fk_apourval_apourvale_moto")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.CaracteristiqueMotoPourValeur)
                    .WithMany(p => p.APourValeurCaracteristiqueMoto)
                    .HasForeignKey(d => d.IdCaracteristiqueMoto)
                    .HasConstraintName("fk_apourval_apourvale_caracter")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TABLE A POUR COULEUR
            // C'EST BON
            modelBuilder.Entity<APourCouleur>(entity =>
            {
                entity.HasKey(e => new { e.IdEquipement, e.IdCouleurEquipement })
                    .HasName("pk_a_pour_couleur");

                entity.HasOne(d => d.EquipementAPourCouleur)
                    .WithMany(p => p.APourCouleurEquipement)
                    .HasForeignKey(d => d.IdEquipement)
                    .HasConstraintName("fk_a_pour_c_a_pour_co_equipeme")
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CouleurEquipementAPourCouleur)
                    .WithMany(p => p.APourCouleurCouleurEquipement)
                    .HasForeignKey(d => d.IdCouleurEquipement)
                    .HasConstraintName("fk_a_pour_c_a_pour_co_couleure")
                    .OnDelete(DeleteBehavior.Restrict);


            });

            //TABLE MotoDisponible
            modelBuilder.Entity<MotoDisponible>(entity =>
            {
                entity.HasKey(e => e.IdMoto)
                    .HasName("PK_MOTODISPONIBLE");

                entity.Property(e => e.PrixMoto)
                    .HasColumnName("mtd_prixmoto")
                    .HasColumnType("numeric");

                // Configuration de la relation avec Moto
                entity.HasOne(md => md.MotoMotoDisponible)
                    .WithOne(m => m.MotoDisponibleMoto)
                    .HasForeignKey<MotoDisponible>(md => md.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MOTODISPONIBLE_MOTO");
            });

            //TABLE PeutContenir
            modelBuilder.Entity<PeutContenir>(entity =>
            {
                entity.HasKey(e => new { e.IdColoris, e.IdMoto })
                    .HasName("PK_PEUTCONTENIR");

                // Configuration de la relation avec Coloris
                entity.HasOne(pc => pc.ColorisPeutContenir)
                    .WithMany(coloris => coloris.PeutContenirColoris)
                    .HasForeignKey(pc => pc.IdColoris)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTCONTENIR_COLORIS");

                // Configuration de la relation avec Moto
                entity.HasOne(pc => pc.MotoPeutContenir)
                    .WithMany(moto => moto.PeutContenirMoto)
                    .HasForeignKey(pc => pc.IdMoto)
                    .OnDelete(DeleteBehavior.Cascade) // Ajuste selon les besoins de ta logique métier
                    .HasConstraintName("FK_PEUTCONTENIR_MOTO");
            });

            // TABLE DONNEES CONSTANTE
            // C'EST BON
            modelBuilder.Entity<DonneesConstante>(entity =>
            {
                entity.ToTable("t_e_donneesconstante_dnc");

                entity.HasKey(e => e.IdDonnesConstante)
                    .HasName("pk_donneesconstante");

                entity.Property(e => e.FraisLivraisonCommande)
                    .HasColumnName("dnc_fraislivraisoncommande")
                    .HasColumnType("numeric");

                entity.Property(e => e.TVANormal)
                    .HasColumnName("dnc_tvanormal")
                    .HasColumnType("numeric");

                entity.Property(e => e.TVAIntermediaire)
                    .HasColumnName("dnc_tvaintermediaire")
                    .HasColumnType("numeric");

                entity.Property(e => e.TVAReduit)
                    .HasColumnName("dnc_tvareduit")
                    .HasColumnType("numeric");

                entity.Property(e => e.TVAParticulier)
                    .HasColumnName("dnc_tvaparticulier")
                    .HasColumnType("numeric");
            });


            // TABLE ACHOISI OPTION
            // C'EST BON
            modelBuilder.Entity<AChoisiOption>(entity =>
            {
                entity.HasKey(e => new { e.IdConfigurationMoto, e.IdEquipementMoto })
                      .HasName("pk_achoisioption");

                entity.HasOne(d => d.ConfigurationMotoChoisiOption)
                    .WithMany(p => p.AChoisiOptionsConfigurationMoto)
                    .HasForeignKey(d => d.IdConfigurationMoto)
                    .HasConstraintName("fk_achoisio_achoisiop_configur");

                entity.HasOne(d => d.EquipementMotoChoisiOption)
                    .WithMany(p => p.AChoisiOptionsEquipementMoto)
                    .HasForeignKey(d => d.IdEquipementMoto)
                    .HasConstraintName("fk_achoisio_achoisiop_equipeme")
                    .OnDelete(DeleteBehavior.Restrict);
            });



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
