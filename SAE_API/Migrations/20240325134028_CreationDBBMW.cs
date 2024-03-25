using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAE_API.Migrations
{
    public partial class CreationDBBMW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_cbt",
                columns: table => new
                {
                    cbt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cbt_nomcarte = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cbt_numerocb = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    cbt_moisexpiration = table.Column<int>(type: "integer", nullable: true),
                    cbt_anneeexpiration = table.Column<int>(type: "integer", nullable: true),
                    cbt_cryptocb = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cartebancaire", x => x.cbt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categoriecaracteristiquemoto_ccm",
                columns: table => new
                {
                    ccm_idcategoriecaracteristiquemoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ccm_nomcategoriecaracteristiquemoto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoriecaracteristiquemot", x => x.ccm_idcategoriecaracteristiquemoto);
                });

            migrationBuilder.CreateTable(
                name: "t_e_collection_clt",
                columns: table => new
                {
                    clt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clt_nomcollection = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collection", x => x.clt_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cmd_prixfraislivraison = table.Column<decimal>(type: "numeric", nullable: true),
                    cmd_date = table.Column<DateTime>(type: "date", nullable: false),
                    cmd_prixtotal = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commande", x => x.cmd_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compteclient_ccl",
                columns: table => new
                {
                    ccl_idcompteclient = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ccl_nomclient = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ccl_prenomclient = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ccl_civiliteclient = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    ccl_numeroclient = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ccl_email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ccl_datenaissanceclient = table.Column<DateTime>(type: "date", nullable: true),
                    ccl_password = table.Column<byte[]>(type: "bytea", nullable: true),
                    ccl_clientrole = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_compteclient", x => x.ccl_idcompteclient);
                });

            migrationBuilder.CreateTable(
                name: "t_e_couleurequipement_ceq",
                columns: table => new
                {
                    ceq_idcouleurequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ceq_nomcouleurequipement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_e_couleurequipement_ceq", x => x.ceq_idcouleurequipement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_datelivraison_dlv",
                columns: table => new
                {
                    dlv_iddatelivraison = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dlv_date = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_datelivraison", x => x.dlv_iddatelivraison);
                });

            migrationBuilder.CreateTable(
                name: "t_e_donneesconstante_dnc",
                columns: table => new
                {
                    dnc_iddonneesconstante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dnc_fraislivraisoncommande = table.Column<decimal>(type: "numeric", nullable: true),
                    dnc_tvanormal = table.Column<decimal>(type: "numeric", nullable: true),
                    dnc_tvaintermediaire = table.Column<decimal>(type: "numeric", nullable: true),
                    dnc_tvareduit = table.Column<decimal>(type: "numeric", nullable: true),
                    dnc_tvaparticulier = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donneesconstante", x => x.dnc_iddonneesconstante);
                });

            migrationBuilder.CreateTable(
                name: "t_e_gammemoto_gmm",
                columns: table => new
                {
                    gmm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gmm_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAMMEMOTO", x => x.gmm_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_moyendepaiement_mdp",
                columns: table => new
                {
                    mdp_idmoyendepaiement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mdp_libellemoyendepaiement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOYENDEPAIEMENT", x => x.mdp_idmoyendepaiement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pack_pck",
                columns: table => new
                {
                    pck_idpack = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pck_idcoloris = table.Column<int>(type: "integer", nullable: true),
                    pck_nompack = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    pck_descriptionpack = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    pck_prixpack = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACK", x => x.pck_idpack);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pho",
                columns: table => new
                {
                    pho_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pho_lienphoto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHOTO", x => x.pho_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_segement_seg",
                columns: table => new
                {
                    seg_idsegement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    seg_nomsegement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEGEMENT", x => x.seg_idsegement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_stock_stk",
                columns: table => new
                {
                    stk_idstock = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCK", x => x.stk_idstock);
                });

            migrationBuilder.CreateTable(
                name: "t_e_tailleequipement_tel",
                columns: table => new
                {
                    tel_idtailleequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tel_nomtailleequipement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAILLEEQUIPEMENT", x => x.tel_idtailleequipement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeequipement_typ",
                columns: table => new
                {
                    typ_idtypeequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    typ_idsurtypeequipement = table.Column<int>(type: "integer", nullable: true),
                    typ_nomtypeequipement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeequipement", x => x.typ_idtypeequipement);
                    table.ForeignKey(
                        name: "fk_typeequipement_surtypeequipement",
                        column: x => x.typ_idsurtypeequipement,
                        principalTable: "t_e_typeequipement_typ",
                        principalColumn: "typ_idtypeequipement");
                });

            migrationBuilder.CreateTable(
                name: "t_e_caracteristiquemoto_crm",
                columns: table => new
                {
                    crm_idcaracteristiquemoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    crm_idcategoriecaracteristiquemoto = table.Column<int>(type: "integer", nullable: false),
                    crm_nomcaracteristiquemoto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    crm_valeurcaracteristiquemoto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caracteristiquemoto", x => x.crm_idcaracteristiquemoto);
                    table.ForeignKey(
                        name: "fk_caracter_apourcate_categori",
                        column: x => x.crm_idcategoriecaracteristiquemoto,
                        principalTable: "t_e_categoriecaracteristiquemoto_ccm",
                        principalColumn: "ccm_idcategoriecaracteristiquemoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_ads",
                columns: table => new
                {
                    ads_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ads_idcompteclient = table.Column<int>(type: "integer", nullable: true),
                    ads_idconcessionnaire = table.Column<int>(type: "integer", nullable: true),
                    ads_numero = table.Column<int>(type: "integer", nullable: true),
                    ads_rueclient = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ads_codepostal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ads_ville = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ads_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ads_typeadresse = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adresse", x => x.ads_id);
                    table.ForeignKey(
                        name: "fk_adresse_sesitue_comptecl",
                        column: x => x.ads_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient");
                });

            migrationBuilder.CreateTable(
                name: "t_e_compteadmin_cad",
                columns: table => new
                {
                    cad_idcompteclient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_compteadmin", x => x.cad_idcompteclient);
                    table.ForeignKey(
                        name: "FK_t_e_compteadmin_cad_t_e_compteclient_ccl_cad_idcompteclient",
                        column: x => x.cad_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compteclientprive_cpp",
                columns: table => new
                {
                    cpp_idcompteclient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_e_compteclientprive_cpp", x => x.cpp_idcompteclient);
                    table.ForeignKey(
                        name: "fk_comptecl_est_un_co_comptecl",
                        column: x => x.cpp_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compteclientprofessionnel_cpp",
                columns: table => new
                {
                    cpp_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    cpp_nomcompagnie = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_compteclientprofessionnel", x => x.cpp_idcompteclient);
                    table.ForeignKey(
                        name: "fk_comptecl_est_un_co_comptecl",
                        column: x => x.cpp_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_effectuer_eff",
                columns: table => new
                {
                    eff_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    eff_idcommande = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_effectuer", x => new { x.eff_idcompteclient, x.eff_idcommande });
                    table.ForeignKey(
                        name: "fk_t_e_effectuer_eff_t_e_effectuer_eff_commande",
                        column: x => x.eff_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_t_e_effectuer_eff_t_e_effectuer_eff2_compte",
                        column: x => x.eff_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_transation_tra",
                columns: table => new
                {
                    tra_idtransaction = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tra_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    tra_montant = table.Column<decimal>(type: "numeric", nullable: false),
                    tra_typdepayment = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tra_typedetransaction = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSATION", x => x.tra_idtransaction);
                    table.ForeignKey(
                        name: "FK_TRANSATION_COMPTECLIENT",
                        column: x => x.tra_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_acquerir_acq",
                columns: table => new
                {
                    acq_idcb = table.Column<int>(type: "integer", nullable: false),
                    acq_idcompteclient = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_acquerir", x => new { x.acq_idcb, x.acq_idcompteclient });
                    table.ForeignKey(
                        name: "fk_acquerir_acquerir_cartebancaire",
                        column: x => x.acq_idcb,
                        principalTable: "t_e_cartebancaire_cbt",
                        principalColumn: "cbt_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_acquerir_acquerir2_configur",
                        column: x => x.acq_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reprisemoto_rpm",
                columns: table => new
                {
                    rpm_idestimationmoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rpm_iddatelivraison = table.Column<int>(type: "integer", nullable: false),
                    rpm_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    rpm_marqueestimationmoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    rpm_modeleestimationmoto = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    rpm_moisimmatriculation = table.Column<int>(type: "integer", nullable: true),
                    rpm_anneimmatriculation = table.Column<int>(type: "integer", nullable: true),
                    rpm_prixestimationmoto = table.Column<decimal>(type: "numeric", nullable: true),
                    rpm_kilometrageestimationmoto = table.Column<decimal>(type: "numeric", nullable: true),
                    rpm_versionestimationmoto = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REPRISEMOTO", x => x.rpm_idestimationmoto);
                    table.ForeignKey(
                        name: "FK_REPRISEMOTO_COMPTECLIENT",
                        column: x => x.rpm_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REPRISEMOTO_DATELIVRAISON",
                        column: x => x.rpm_iddatelivraison,
                        principalTable: "t_e_datelivraison_dlv",
                        principalColumn: "dlv_iddatelivraison",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_moto_mto",
                columns: table => new
                {
                    mto_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mto_idgammemoto = table.Column<int>(type: "integer", nullable: false),
                    mto_nommoto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mto_descriptionmoto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    mto_prixmoto = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTO", x => x.mto_id);
                    table.ForeignKey(
                        name: "FK_MOTO_GAMMEMOTO",
                        column: x => x.mto_idgammemoto,
                        principalTable: "t_e_gammemoto_gmm",
                        principalColumn: "gmm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_coloris_col",
                columns: table => new
                {
                    col_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    col_idphoto = table.Column<int>(type: "integer", nullable: false),
                    col_nomcoloris = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    col_descriptioncoloris = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    col_prixcoloris = table.Column<decimal>(type: "numeric", nullable: true),
                    col_typecoloris = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_t_e_coloris_col", x => x.col_id);
                    table.ForeignKey(
                        name: "fk_t_e_coloris_col_refleter_photo",
                        column: x => x.col_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipementmotooption_eqm",
                columns: table => new
                {
                    eqm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eqm_idphoto = table.Column<int>(type: "integer", nullable: true),
                    eqm_nomequipement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    eqm_descriptionequipementmoto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    eqm_prixequipementmoto = table.Column<decimal>(type: "numeric", nullable: true),
                    eqm_equipementorigine = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPEMENTMOTOOPTION", x => x.eqm_id);
                    table.ForeignKey(
                        name: "FK_EQUIPEMENTMOTOOPTION_PHOTO",
                        column: x => x.eqm_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipement_eqp",
                columns: table => new
                {
                    eqp_idequipement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eqp_idsegment = table.Column<int>(type: "integer", nullable: false),
                    eqp_idcollection = table.Column<int>(type: "integer", nullable: false),
                    eqp_idtypeequipement = table.Column<int>(type: "integer", nullable: false),
                    eqp_nomequipement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    eqp_descriptionequipement = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    eqp_detailequipement = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    eqp_dureeequipement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    eqp_prixequipement = table.Column<decimal>(type: "numeric", nullable: true),
                    eqp_sexe = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPEMENT", x => x.eqp_idequipement);
                    table.ForeignKey(
                        name: "FK_EQUIPEMENT_COLLECTION",
                        column: x => x.eqp_idcollection,
                        principalTable: "t_e_collection_clt",
                        principalColumn: "clt_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EQUIPEMENT_SEGMENT",
                        column: x => x.eqp_idsegment,
                        principalTable: "t_e_segement_seg",
                        principalColumn: "seg_idsegement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EQUIPEMENT_TYPEEQUIPEMENT",
                        column: x => x.eqp_idtypeequipement,
                        principalTable: "t_e_typeequipement_typ",
                        principalColumn: "typ_idtypeequipement",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_concessionnaire_csn",
                columns: table => new
                {
                    csn_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    csn_idadresse = table.Column<int>(type: "integer", nullable: false),
                    csn_idstock = table.Column<int>(type: "integer", nullable: false),
                    csn_nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_concessionnaire", x => x.csn_id);
                    table.ForeignKey(
                        name: "fk_adresse_situer_concessi",
                        column: x => x.csn_idadresse,
                        principalTable: "t_e_adresse_ads",
                        principalColumn: "ads_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_concessi_stocker_stock",
                        column: x => x.csn_idstock,
                        principalTable: "t_e_stock_stk",
                        principalColumn: "stk_idstock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_estimer_est",
                columns: table => new
                {
                    est_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    est_idestimationmoto = table.Column<int>(type: "integer", nullable: false),
                    est_idmoyendepaiement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTIMER", x => new { x.est_idcompteclient, x.est_idestimationmoto, x.est_idmoyendepaiement });
                    table.ForeignKey(
                        name: "FK_ESTIMER_COMPTECLIENT",
                        column: x => x.est_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ESTIMER_MOYENDEPAIEMENT",
                        column: x => x.est_idmoyendepaiement,
                        principalTable: "t_e_moyendepaiement_mdp",
                        principalColumn: "mdp_idmoyendepaiement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ESTIMER_REPRISEMOTO",
                        column: x => x.est_idestimationmoto,
                        principalTable: "t_e_reprisemoto_rpm",
                        principalColumn: "rpm_idestimationmoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_apourvaleur_apv",
                columns: table => new
                {
                    apv_idmoto = table.Column<int>(type: "integer", nullable: false),
                    apv_idcaracteristiquemoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_apourvaleur", x => new { x.apv_idmoto, x.apv_idcaracteristiquemoto });
                    table.ForeignKey(
                        name: "fk_apourval_apourvale_caracter",
                        column: x => x.apv_idcaracteristiquemoto,
                        principalTable: "t_e_caracteristiquemoto_crm",
                        principalColumn: "crm_idcaracteristiquemoto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_apourval_apourvale_moto",
                        column: x => x.apv_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_motodisponible_mtd",
                columns: table => new
                {
                    mtd_idmoto = table.Column<int>(type: "integer", nullable: false),
                    mtd_prixmoto = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTODISPONIBLE", x => x.mtd_idmoto);
                    table.ForeignKey(
                        name: "FK_MOTODISPONIBLE_MOTO",
                        column: x => x.mtd_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_peutequiper_peq",
                columns: table => new
                {
                    peq_idpack = table.Column<int>(type: "integer", nullable: false),
                    peq_idmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEUTEQUIPER", x => new { x.peq_idpack, x.peq_idmoto });
                    table.ForeignKey(
                        name: "FK_PEUTEQUIPER_MOTO",
                        column: x => x.peq_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEUTEQUIPER_PACK",
                        column: x => x.peq_idpack,
                        principalTable: "t_e_pack_pck",
                        principalColumn: "pck_idpack",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_estdans_esd",
                columns: table => new
                {
                    esd_idmoto = table.Column<int>(type: "integer", nullable: false),
                    esd_idstock = table.Column<int>(type: "integer", nullable: false),
                    esd_quantitestockdisponible = table.Column<int>(type: "integer", nullable: true),
                    esd_quantitestockmoto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTDANS", x => new { x.esd_idmoto, x.esd_idstock });
                    table.ForeignKey(
                        name: "FK_ESTDANS_MOTO",
                        column: x => x.esd_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ESTDANS_STOCK",
                        column: x => x.esd_idstock,
                        principalTable: "t_e_stock_stk",
                        principalColumn: "stk_idstock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_illustrer_ilr",
                columns: table => new
                {
                    ilr_idmoto = table.Column<int>(type: "integer", nullable: false),
                    ilr_idphoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILLUSTRER", x => new { x.ilr_idmoto, x.ilr_idphoto });
                    table.ForeignKey(
                        name: "FK_ILLUSTRER_MOTO",
                        column: x => x.ilr_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ILLUSTRER_PHOTO",
                        column: x => x.ilr_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_configurationmoto_cfm",
                columns: table => new
                {
                    cfm_idconfigurationmoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cfm_idreservationoffre = table.Column<int>(type: "integer", nullable: true),
                    cfm_idmoto = table.Column<int>(type: "integer", nullable: false),
                    cfm_idcoloris = table.Column<int>(type: "integer", nullable: false),
                    cfm_prixtotalconfiguration = table.Column<decimal>(type: "numeric", nullable: true),
                    cfm_dateconfiguration = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configurationmoto", x => x.cfm_idconfigurationmoto);
                    table.ForeignKey(
                        name: "fk_configur_disponibl_t_e_coloris_col",
                        column: x => x.cfm_idcoloris,
                        principalTable: "t_e_coloris_col",
                        principalColumn: "col_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_configur_estconfig_moto",
                        column: x => x.cfm_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_peutcontenir_ptc",
                columns: table => new
                {
                    ptc_idcoloris = table.Column<int>(type: "integer", nullable: false),
                    ptc_idmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEUTCONTENIR", x => new { x.ptc_idcoloris, x.ptc_idmoto });
                    table.ForeignKey(
                        name: "FK_PEUTCONTENIR_COLORIS",
                        column: x => x.ptc_idcoloris,
                        principalTable: "t_e_coloris_col",
                        principalColumn: "col_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEUTCONTENIR_MOTO",
                        column: x => x.ptc_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipementaccessoire_eqa",
                columns: table => new
                {
                    eqa_idequipementmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipementaccessoire", x => x.eqa_idequipementmoto);
                    table.ForeignKey(
                        name: "fk_equipeme_est_equipeme",
                        column: x => x.eqa_idequipementmoto,
                        principalTable: "t_e_equipementmotooption_eqm",
                        principalColumn: "eqm_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_equipementoption_eqo",
                columns: table => new
                {
                    eqo_idequipementmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipementoption", x => x.eqo_idequipementmoto);
                    table.ForeignKey(
                        name: "fk_equipeme_est2_equipeme",
                        column: x => x.eqo_idequipementmoto,
                        principalTable: "t_e_equipementmotooption_eqm",
                        principalColumn: "eqm_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_peututiliser_put",
                columns: table => new
                {
                    put_idpack = table.Column<int>(type: "integer", nullable: false),
                    put_idequipementmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEUTUTILISER", x => new { x.put_idpack, x.put_idequipementmoto });
                    table.ForeignKey(
                        name: "FK_PEUTUTILISER_EQUIPEMENT",
                        column: x => x.put_idequipementmoto,
                        principalTable: "t_e_equipementmotooption_eqm",
                        principalColumn: "eqm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEUTUTILISER_PACK",
                        column: x => x.put_idpack,
                        principalTable: "t_e_pack_pck",
                        principalColumn: "pck_idpack",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_posseder_pos",
                columns: table => new
                {
                    pos_idmoto = table.Column<int>(type: "integer", nullable: false),
                    pos_idequipementmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSSEDER", x => new { x.pos_idmoto, x.pos_idequipementmoto });
                    table.ForeignKey(
                        name: "FK_POSSEDER_EQUIPEMENTMOTOOPTION",
                        column: x => x.pos_idequipementmoto,
                        principalTable: "t_e_equipementmotooption_eqm",
                        principalColumn: "eqm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSSEDER_MOTO",
                        column: x => x.pos_idmoto,
                        principalTable: "t_e_moto_mto",
                        principalColumn: "mto_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_apourtaille_apt",
                columns: table => new
                {
                    apt_idequipement = table.Column<int>(type: "integer", nullable: false),
                    apt_idtailleequipement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_a_pour_taille", x => new { x.apt_idequipement, x.apt_idtailleequipement });
                    table.ForeignKey(
                        name: "fk_a_pour_t_a_pour_ta_equipeme",
                        column: x => x.apt_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_a_pour_t_a_pour_ta_tailleeq",
                        column: x => x.apt_idtailleequipement,
                        principalTable: "t_e_tailleequipement_tel",
                        principalColumn: "tel_idtailleequipement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_disposer_dsp",
                columns: table => new
                {
                    dsp_idstock = table.Column<int>(type: "integer", nullable: false),
                    dsp_idequipement = table.Column<int>(type: "integer", nullable: false),
                    dsp_quantitestock = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disposer", x => new { x.dsp_idequipement, x.dsp_idstock });
                    table.ForeignKey(
                        name: "fk_disposer_disposer_stock",
                        column: x => x.dsp_idstock,
                        principalTable: "t_e_stock_stk",
                        principalColumn: "stk_idstock",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_disposer_disposer2_equipeme",
                        column: x => x.dsp_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_presente_pre",
                columns: table => new
                {
                    pre_idphoto = table.Column<int>(type: "integer", nullable: false),
                    pre_idequipement = table.Column<int>(type: "integer", nullable: false),
                    pre_idcouleurequipement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRESENTE", x => new { x.pre_idphoto, x.pre_idequipement, x.pre_idcouleurequipement });
                    table.ForeignKey(
                        name: "FK_PRESENTE_COULEUREQUIPEMENT",
                        column: x => x.pre_idcouleurequipement,
                        principalTable: "t_e_couleurequipement_ceq",
                        principalColumn: "ceq_idcouleurequipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRESENTE_EQUIPEMENT",
                        column: x => x.pre_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRESENTE_PHOTO",
                        column: x => x.pre_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_apourcouleur_apc",
                columns: table => new
                {
                    apc_idequipement = table.Column<int>(type: "integer", nullable: false),
                    apc_idcouleurequipement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_a_pour_couleur", x => new { x.apc_idequipement, x.apc_idcouleurequipement });
                    table.ForeignKey(
                        name: "fk_a_pour_c_a_pour_co_couleure",
                        column: x => x.apc_idcouleurequipement,
                        principalTable: "t_e_couleurequipement_ceq",
                        principalColumn: "ceq_idcouleurequipement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_a_pour_c_a_pour_co_equipeme",
                        column: x => x.apc_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_detient_det",
                columns: table => new
                {
                    det_idequipement = table.Column<int>(type: "integer", nullable: false),
                    det_idconcessionnaire = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detient", x => new { x.det_idequipement, x.det_idconcessionnaire });
                    table.ForeignKey(
                        name: "fk_detient_detient_equipement",
                        column: x => x.det_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_detient_detient2_concessi",
                        column: x => x.det_idconcessionnaire,
                        principalTable: "t_e_concessionnaire_csn",
                        principalColumn: "csn_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_favoris_fav",
                columns: table => new
                {
                    fav_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    fav_idconcessionnaire = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORIS", x => new { x.fav_idcompteclient, x.fav_idconcessionnaire });
                    table.ForeignKey(
                        name: "FK_FAVORIS_COMPTECLIENT",
                        column: x => x.fav_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FAVORIS_CONCESSIONNAIRE",
                        column: x => x.fav_idconcessionnaire,
                        principalTable: "t_e_concessionnaire_csn",
                        principalColumn: "csn_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_priserendezvous_prv",
                columns: table => new
                {
                    prv_idreservationoffre = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    prv_idconcessionnaire = table.Column<int>(type: "integer", nullable: true),
                    prv_nomreservation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prv_prenomreservation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prv_civilreservation = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    prv_emailreservation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    prv_telephonereservation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    prv_villereservation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    prv_typedepermis = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRISERENDEZVOUS", x => x.prv_idreservationoffre);
                    table.ForeignKey(
                        name: "FK_PRISERENDEZVOUS_CONCESSIONNAIRE",
                        column: x => x.prv_idconcessionnaire,
                        principalTable: "t_e_concessionnaire_csn",
                        principalColumn: "csn_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_provenance_prv",
                columns: table => new
                {
                    prv_idcommande = table.Column<int>(type: "integer", nullable: false),
                    prv_idconcessionnaire = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVENANCE", x => new { x.prv_idcommande, x.prv_idconcessionnaire });
                    table.ForeignKey(
                        name: "FK_PROVENANCE_COMMANDE",
                        column: x => x.prv_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROVENANCE_CONCESSIONNAIRE",
                        column: x => x.prv_idconcessionnaire,
                        principalTable: "t_e_concessionnaire_csn",
                        principalColumn: "csn_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_achoisioption_aco",
                columns: table => new
                {
                    aco_idconfigurationmoto = table.Column<int>(type: "integer", nullable: false),
                    aco_idequipementmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achoisioption", x => new { x.aco_idconfigurationmoto, x.aco_idequipementmoto });
                    table.ForeignKey(
                        name: "fk_achoisio_achoisiop_configur",
                        column: x => x.aco_idconfigurationmoto,
                        principalTable: "t_e_configurationmoto_cfm",
                        principalColumn: "cfm_idconfigurationmoto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_achoisio_achoisiop_equipeme",
                        column: x => x.aco_idequipementmoto,
                        principalTable: "t_e_equipementmotooption_eqm",
                        principalColumn: "eqm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commander_cmdr",
                columns: table => new
                {
                    cmdr_idcommande = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cmdr_idequipement = table.Column<int>(type: "integer", nullable: false),
                    cmdr_idconfigurationmoto = table.Column<int>(type: "integer", nullable: true),
                    cmdr_qtecommande = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commander", x => x.cmdr_idcommande);
                    table.ForeignKey(
                        name: "fk_commande_commander_configurationmoto",
                        column: x => x.cmdr_idconfigurationmoto,
                        principalTable: "t_e_configurationmoto_cfm",
                        principalColumn: "cfm_idconfigurationmoto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_commande_commander_equipeme",
                        column: x => x.cmdr_idequipement,
                        principalTable: "t_e_equipement_eqp",
                        principalColumn: "eqp_idequipement",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_enregistrer_enr",
                columns: table => new
                {
                    enr_idconfigurationmoto = table.Column<int>(type: "integer", nullable: false),
                    enr_idcompteclient = table.Column<int>(type: "integer", nullable: false),
                    enr_nomconfiguration = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENREGISTRER", x => new { x.enr_idconfigurationmoto, x.enr_idcompteclient });
                    table.ForeignKey(
                        name: "FK_ENREGISTRER_COMPTECLIENT",
                        column: x => x.enr_idcompteclient,
                        principalTable: "t_e_compteclient_ccl",
                        principalColumn: "ccl_idcompteclient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ENREGISTRER_CONFIGURATIONMOTO",
                        column: x => x.enr_idconfigurationmoto,
                        principalTable: "t_e_configurationmoto_cfm",
                        principalColumn: "cfm_idconfigurationmoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_achoisi_ach",
                columns: table => new
                {
                    ach_idpack = table.Column<int>(type: "integer", nullable: false),
                    ach_idconfigurationmoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_achoisi", x => new { x.ach_idpack, x.ach_idconfigurationmoto });
                    table.ForeignKey(
                        name: "fk_achoisi_achoisi_pack",
                        column: x => x.ach_idpack,
                        principalTable: "t_e_pack_pck",
                        principalColumn: "pck_idpack",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_achoisi_achoisi2_configur",
                        column: x => x.ach_idconfigurationmoto,
                        principalTable: "t_e_configurationmoto_cfm",
                        principalColumn: "cfm_idconfigurationmoto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_demandercontact_dmc",
                columns: table => new
                {
                    dmc_idreservationoffre = table.Column<int>(type: "integer", nullable: false),
                    dmc_objetdelademande = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    dmc_objet = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_demandercontact", x => x.dmc_idreservationoffre);
                    table.ForeignKey(
                        name: "fk_demander_heritage__priseren",
                        column: x => x.dmc_idreservationoffre,
                        principalTable: "t_e_priserendezvous_prv",
                        principalColumn: "prv_idreservationoffre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_res",
                columns: table => new
                {
                    res_idreservationoffre = table.Column<int>(type: "integer", nullable: false),
                    res_idconcessionnaire = table.Column<int>(type: "integer", nullable: true),
                    res_financementreservationoffre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    res_assurancereservation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATION", x => x.res_idreservationoffre);
                    table.ForeignKey(
                        name: "fk_reservat_heritage__priseren",
                        column: x => x.res_idreservationoffre,
                        principalTable: "t_e_priserendezvous_prv",
                        principalColumn: "prv_idreservationoffre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_achoisioption_aco_aco_idequipementmoto",
                table: "t_e_achoisioption_aco",
                column: "aco_idequipementmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_ads_ads_idcompteclient",
                table: "t_e_adresse_ads",
                column: "ads_idcompteclient",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_apourtaille_apt_apt_idtailleequipement",
                table: "t_e_apourtaille_apt",
                column: "apt_idtailleequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_apourvaleur_apv_apv_idcaracteristiquemoto",
                table: "t_e_apourvaleur_apv",
                column: "apv_idcaracteristiquemoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_caracteristiquemoto_crm_crm_idcategoriecaracteristiquem~",
                table: "t_e_caracteristiquemoto_crm",
                column: "crm_idcategoriecaracteristiquemoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_coloris_col_col_idphoto",
                table: "t_e_coloris_col",
                column: "col_idphoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commander_cmdr_cmdr_idconfigurationmoto",
                table: "t_e_commander_cmdr",
                column: "cmdr_idconfigurationmoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commander_cmdr_cmdr_idequipement",
                table: "t_e_commander_cmdr",
                column: "cmdr_idequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_concessionnaire_csn_csn_idadresse",
                table: "t_e_concessionnaire_csn",
                column: "csn_idadresse");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_concessionnaire_csn_csn_idstock",
                table: "t_e_concessionnaire_csn",
                column: "csn_idstock");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_configurationmoto_cfm_cfm_idcoloris",
                table: "t_e_configurationmoto_cfm",
                column: "cfm_idcoloris");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_configurationmoto_cfm_cfm_idmoto",
                table: "t_e_configurationmoto_cfm",
                column: "cfm_idmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_detient_det_det_idconcessionnaire",
                table: "t_e_detient_det",
                column: "det_idconcessionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_disposer_dsp_dsp_idstock",
                table: "t_e_disposer_dsp",
                column: "dsp_idstock");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_effectuer_eff_eff_idcommande",
                table: "t_e_effectuer_eff",
                column: "eff_idcommande");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_enregistrer_enr_enr_idcompteclient",
                table: "t_e_enregistrer_enr",
                column: "enr_idcompteclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_eqp_eqp_idcollection",
                table: "t_e_equipement_eqp",
                column: "eqp_idcollection");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_eqp_eqp_idsegment",
                table: "t_e_equipement_eqp",
                column: "eqp_idsegment");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipement_eqp_eqp_idtypeequipement",
                table: "t_e_equipement_eqp",
                column: "eqp_idtypeequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_equipementmotooption_eqm_eqm_idphoto",
                table: "t_e_equipementmotooption_eqm",
                column: "eqm_idphoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_estimer_est_est_idestimationmoto",
                table: "t_e_estimer_est",
                column: "est_idestimationmoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_estimer_est_est_idmoyendepaiement",
                table: "t_e_estimer_est",
                column: "est_idmoyendepaiement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_favoris_fav_fav_idconcessionnaire",
                table: "t_e_favoris_fav",
                column: "fav_idconcessionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_moto_mto_mto_idgammemoto",
                table: "t_e_moto_mto",
                column: "mto_idgammemoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_peutcontenir_ptc_ptc_idmoto",
                table: "t_e_peutcontenir_ptc",
                column: "ptc_idmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_peutequiper_peq_peq_idmoto",
                table: "t_e_peutequiper_peq",
                column: "peq_idmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_peututiliser_put_put_idequipementmoto",
                table: "t_e_peututiliser_put",
                column: "put_idequipementmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_posseder_pos_pos_idequipementmoto",
                table: "t_e_posseder_pos",
                column: "pos_idequipementmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_presente_pre_pre_idcouleurequipement",
                table: "t_e_presente_pre",
                column: "pre_idcouleurequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_presente_pre_pre_idequipement",
                table: "t_e_presente_pre",
                column: "pre_idequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_priserendezvous_prv_prv_idconcessionnaire",
                table: "t_e_priserendezvous_prv",
                column: "prv_idconcessionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_provenance_prv_prv_idconcessionnaire",
                table: "t_e_provenance_prv",
                column: "prv_idconcessionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reprisemoto_rpm_rpm_idcompteclient",
                table: "t_e_reprisemoto_rpm",
                column: "rpm_idcompteclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reprisemoto_rpm_rpm_iddatelivraison",
                table: "t_e_reprisemoto_rpm",
                column: "rpm_iddatelivraison");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_transation_tra_tra_idcompteclient",
                table: "t_e_transation_tra",
                column: "tra_idcompteclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typeequipement_typ_typ_idsurtypeequipement",
                table: "t_e_typeequipement_typ",
                column: "typ_idsurtypeequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_achoisi_ach_ach_idconfigurationmoto",
                table: "t_j_achoisi_ach",
                column: "ach_idconfigurationmoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_acquerir_acq_acq_idcompteclient",
                table: "t_j_acquerir_acq",
                column: "acq_idcompteclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_apourcouleur_apc_apc_idcouleurequipement",
                table: "t_j_apourcouleur_apc",
                column: "apc_idcouleurequipement");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_estdans_esd_esd_idstock",
                table: "t_j_estdans_esd",
                column: "esd_idstock");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_illustrer_ilr_ilr_idphoto",
                table: "t_j_illustrer_ilr",
                column: "ilr_idphoto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_achoisioption_aco");

            migrationBuilder.DropTable(
                name: "t_e_apourtaille_apt");

            migrationBuilder.DropTable(
                name: "t_e_apourvaleur_apv");

            migrationBuilder.DropTable(
                name: "t_e_commander_cmdr");

            migrationBuilder.DropTable(
                name: "t_e_compteadmin_cad");

            migrationBuilder.DropTable(
                name: "t_e_compteclientprive_cpp");

            migrationBuilder.DropTable(
                name: "t_e_compteclientprofessionnel_cpp");

            migrationBuilder.DropTable(
                name: "t_e_demandercontact_dmc");

            migrationBuilder.DropTable(
                name: "t_e_detient_det");

            migrationBuilder.DropTable(
                name: "t_e_disposer_dsp");

            migrationBuilder.DropTable(
                name: "t_e_donneesconstante_dnc");

            migrationBuilder.DropTable(
                name: "t_e_effectuer_eff");

            migrationBuilder.DropTable(
                name: "t_e_enregistrer_enr");

            migrationBuilder.DropTable(
                name: "t_e_equipementaccessoire_eqa");

            migrationBuilder.DropTable(
                name: "t_e_equipementoption_eqo");

            migrationBuilder.DropTable(
                name: "t_e_estimer_est");

            migrationBuilder.DropTable(
                name: "t_e_favoris_fav");

            migrationBuilder.DropTable(
                name: "t_e_motodisponible_mtd");

            migrationBuilder.DropTable(
                name: "t_e_peutcontenir_ptc");

            migrationBuilder.DropTable(
                name: "t_e_peutequiper_peq");

            migrationBuilder.DropTable(
                name: "t_e_peututiliser_put");

            migrationBuilder.DropTable(
                name: "t_e_posseder_pos");

            migrationBuilder.DropTable(
                name: "t_e_presente_pre");

            migrationBuilder.DropTable(
                name: "t_e_provenance_prv");

            migrationBuilder.DropTable(
                name: "t_e_reservation_res");

            migrationBuilder.DropTable(
                name: "t_e_transation_tra");

            migrationBuilder.DropTable(
                name: "t_j_achoisi_ach");

            migrationBuilder.DropTable(
                name: "t_j_acquerir_acq");

            migrationBuilder.DropTable(
                name: "t_j_apourcouleur_apc");

            migrationBuilder.DropTable(
                name: "t_j_estdans_esd");

            migrationBuilder.DropTable(
                name: "t_j_illustrer_ilr");

            migrationBuilder.DropTable(
                name: "t_e_tailleequipement_tel");

            migrationBuilder.DropTable(
                name: "t_e_caracteristiquemoto_crm");

            migrationBuilder.DropTable(
                name: "t_e_moyendepaiement_mdp");

            migrationBuilder.DropTable(
                name: "t_e_reprisemoto_rpm");

            migrationBuilder.DropTable(
                name: "t_e_equipementmotooption_eqm");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_priserendezvous_prv");

            migrationBuilder.DropTable(
                name: "t_e_pack_pck");

            migrationBuilder.DropTable(
                name: "t_e_configurationmoto_cfm");

            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_cbt");

            migrationBuilder.DropTable(
                name: "t_e_couleurequipement_ceq");

            migrationBuilder.DropTable(
                name: "t_e_equipement_eqp");

            migrationBuilder.DropTable(
                name: "t_e_categoriecaracteristiquemoto_ccm");

            migrationBuilder.DropTable(
                name: "t_e_datelivraison_dlv");

            migrationBuilder.DropTable(
                name: "t_e_concessionnaire_csn");

            migrationBuilder.DropTable(
                name: "t_e_coloris_col");

            migrationBuilder.DropTable(
                name: "t_e_moto_mto");

            migrationBuilder.DropTable(
                name: "t_e_collection_clt");

            migrationBuilder.DropTable(
                name: "t_e_segement_seg");

            migrationBuilder.DropTable(
                name: "t_e_typeequipement_typ");

            migrationBuilder.DropTable(
                name: "t_e_adresse_ads");

            migrationBuilder.DropTable(
                name: "t_e_stock_stk");

            migrationBuilder.DropTable(
                name: "t_e_photo_pho");

            migrationBuilder.DropTable(
                name: "t_e_gammemoto_gmm");

            migrationBuilder.DropTable(
                name: "t_e_compteclient_ccl");
        }
    }
}
