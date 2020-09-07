using System.Data.Entity;
using TP3_NET.Models.Camion;
using TP3_NET.Models.Commande;
using TP3_NET.Models.Commun;
using TP3_NET.Models.Client;
using TP3_NET.Models.Employe;
using TP3_NET.Models.Livraison;
using TP3_NET.Models.Marchandise;
using System.Collections.Generic;

namespace TP3_NET.Dao
{
    /// <summary>
    ///     Classe singleton pour le contexte de la base de données avec Entity Framework
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private const string DatabaseName = "NET_ARCHITECTURE";
        private const string Server = ".\\SQLEXPRESS";
        private const int Port = 1433; // Unused for now
        private const string User = "utilisateur_net";
        private const string Password = "bonjour";

        private static DatabaseContext _databaseContext;

        private DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.Connection.ConnectionString = nameOrConnectionString;

            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
            
            Database.Initialize(true);
        }

        public DbSet<Models.Client.Client> Clients { get; set; }
        public DbSet<Models.Compte.Compte> Comptes { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Technicien> Techniciens { get; set; }
        public DbSet<Operateur> Operateurs { get; set; }
        public DbSet<Conducteur> Conducteurs { get; set; }
        public DbSet<Camion> Camions { get; set; }
        public DbSet<Remorque> Remorques { get; set; }
        public DbSet<Models.Marchandise.Marchandise> Marchandises { get; set; }
        public DbSet<Models.Livraison.Livraison> Livraisons { get; set; }
        public DbSet<Models.Commande.Commande> Commandes { get; set; }
        public DbSet<Models.Livraison.Itineraire> Itineraires { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Marchandise.Marchandise>()
                .HasRequired(marchandise => marchandise.Commande)
                .WithMany(commande => commande.Marchandises)
                .HasForeignKey(marchandise => marchandise.IdCommande)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Models.Marchandise.Marchandise>()
                .HasOptional(marchandise => marchandise.Livraison)
                .WithMany(commande => commande.Marchandises)
                .HasForeignKey(marchandise => marchandise.IdLivraison);

            modelBuilder.Entity<Models.Commun.Adresse>()
                .HasOptional(adresse => adresse.Itineraire)
                .WithMany(itineraire => itineraire.Adresses)
                .HasForeignKey(marchandise => marchandise.IdItineraire);
        }

        private static string GetConnectionString()
        {
            return "Data Source=" + Server + ";Initial Catalog=" + DatabaseName + ";User id=" + User + ";Password=" +
                   Password + ";";
        }

        public static DatabaseContext GetInstance()
        {
            return _databaseContext ?? (_databaseContext = new DatabaseContext(GetConnectionString()));
        }

        /// <summary>
        ///     Pour tester la création de la base de données et des tables sans avoir à lancer l'application
        /// </summary>
        public static void CreateDatabase()
        {
            GetInstance();

            Dao.Employe.TechnicienDao technicienDao = new Dao.Employe.TechnicienDao();
            Dao.Employe.OperateurDao operateurDao = new Dao.Employe.OperateurDao();
            Dao.Commun.AdresseDao adresseDao = new Dao.Commun.AdresseDao();
            Dao.Compte.CompteDao compteDao = new Dao.Compte.CompteDao();
            Dao.Client.ClientDao clientDao = new Dao.Client.ClientDao();
            Dao.Commande.CommandeDao commandeDao = new Dao.Commande.CommandeDao();
            Dao.Marchandise.MarchandiseDao marchandiseDao = new Dao.Marchandise.MarchandiseDao();
            Dao.Livraison.LivraisonDao livraisonDao = new Dao.Livraison.LivraisonDao();
            Dao.Vehicule.CamionDao camionDao = new Dao.Vehicule.CamionDao();
            Dao.Employe.ConducteurDao conducteurDao = new Dao.Employe.ConducteurDao();
            Dao.Vehicule.RemorqueDao remorqueDao = new Dao.Vehicule.RemorqueDao();

            Models.Commun.Adresse adresse_1 = new Models.Commun.Adresse();
            adresse_1.CodePostal = "CP_1";
            adresse_1.Complement = "Complement_1";
            adresse_1.Pays = "Pays_1";
            adresse_1.Rue = "Rue_1";
            adresse_1.Ville = "Ville_1";

            adresse_1 = adresseDao.Create(adresse_1);

            Models.Commun.Adresse adresse_2 = new Models.Commun.Adresse();
            adresse_2.CodePostal = "CP_2";
            adresse_2.Complement = "Complement_2";
            adresse_2.Pays = "Pays_2";
            adresse_2.Rue = "Rue_2";
            adresse_2.Ville = "Ville_2";

            adresse_2 = adresseDao.Create(adresse_2);

            Models.Commun.Adresse adresse_3 = new Models.Commun.Adresse();
            adresse_3.CodePostal = "CP_3";
            adresse_3.Complement = "Complement_3";
            adresse_3.Pays = "Pays_3";
            adresse_3.Rue = "Rue_3";
            adresse_3.Ville = "Ville_3";

            adresse_3 = adresseDao.Create(adresse_3);

            Models.Compte.Compte compte_1 = new Models.Compte.Compte();
            compte_1.Email = "client@foo.bar";
            compte_1.MotDePasse = Models.Service.EncryptionService.EncryptSha256("password");
            compte_1.TypeCompte = Models.Compte.TypeCompte.Client;

            compte_1 = compteDao.Create(compte_1);

            Models.Compte.Compte compte_2 = new Models.Compte.Compte();
            compte_2.Email = "operateur@foo.bar";
            compte_2.MotDePasse = Models.Service.EncryptionService.EncryptSha256("password");
            compte_2.TypeCompte = Models.Compte.TypeCompte.Operateur;

            compte_2 = compteDao.Create(compte_2);

            Models.Employe.Operateur operateur_1 = new Models.Employe.Operateur();
            operateur_1.Compte = compte_2;
            operateur_1.Prenom = "Prenom_2";
            operateur_1.Nom = "Nom_2";

            operateurDao.Create(operateur_1);

            Models.Employe.Technicien technicien_1 = new Models.Employe.Technicien();

            technicien_1.Prenom = "Prenom_4";
            technicien_1.Nom = "Nom_4";
            technicien_1.Competences = new List<Models.Employe.Competence>();

            technicienDao.Create(technicien_1);

            Models.Client.Client client_1 = new Models.Client.Client();
            client_1.Adresse = adresse_1;
            client_1.Compte = compte_1;
            client_1.Entreprise = "Entreprise_1";
            client_1.Fidelite = Models.Client.Fidelite.Platine;
            client_1.Prenom = "Prenom_1";
            client_1.Nom = "Nom_1";
            client_1.Telephone = "0102030405";

            client_1 = clientDao.Create(client_1);

            Models.Commande.Commande commande_1 = new Models.Commande.Commande();
            commande_1.Client = client_1;
            commande_1.DateCommande = System.DateTime.Now;
            commande_1.DepartSouhaitee = System.DateTime.Now.AddDays(1);
            commande_1.Frequence = Frequence.NonApplicable;
            commande_1.LieuArrivee = adresse_2;
            commande_1.LieuDepart = adresse_3;
            commande_1.Marchandises = new List<Models.Marchandise.Marchandise>();

            commande_1 = commandeDao.Create(commande_1);

            Models.Marchandise.Marchandise marchandise_1 = new Models.Marchandise.Marchandise();
            marchandise_1.Nom = "Marchandise_1";
            marchandise_1.TypeMarchandise = Models.Marchandise.TypeMarchandise.Frais;
            marchandise_1.Volume = 1f;
            marchandise_1.Commande = commande_1;

            marchandise_1 = marchandiseDao.Create(marchandise_1);

            Models.Marchandise.Marchandise marchandise_2 = new Models.Marchandise.Marchandise();
            marchandise_2.Nom = "Marchandise_2";
            marchandise_2.TypeMarchandise = Models.Marchandise.TypeMarchandise.Animal;
            marchandise_2.Volume = 2f;
            marchandise_2.Commande = commande_1;

            marchandiseDao.Create(marchandise_2);

            Models.Livraison.Livraison livraison_1 = new Models.Livraison.Livraison();

            livraison_1.Client = client_1;
            livraison_1.Commande = commande_1;
            livraison_1.EtatLivraison = Models.Livraison.EtatLivraison.EnPreparation;

            Models.Livraison.Itineraire itineraire_1 = new Models.Livraison.Itineraire();
            itineraire_1.Adresses = new List<Models.Commun.Adresse>();
            itineraire_1.Adresses.Add(commande_1.LieuDepart);
            itineraire_1.Adresses.Add(commande_1.LieuArrivee);

            livraison_1.Itineraire = itineraire_1;

            livraison_1.Marchandises = new List<Models.Marchandise.Marchandise>();
            livraison_1.Marchandises.Add(marchandise_1);
            livraison_1.Marchandises.Add(marchandise_2);

            livraisonDao.Create(livraison_1);

            Models.Camion.Camion camion_1 = new Models.Camion.Camion();
            camion_1.Immatriculation = "Imm_c1";
            camion_1.PrixJournalier = 120f;
            camion_1.Capacite = 16f;
            camion_1.EtatCamion = Models.Camion.EtatCamion.Disponible;

            camionDao.Create(camion_1);

            Models.Employe.Conducteur conducteur_1 = new Models.Employe.Conducteur();
            conducteur_1.Prenom = "Prenom_3";
            conducteur_1.Nom = "Nom_3";

            conducteurDao.Create(conducteur_1);

            Models.Camion.Remorque remorque_1 = new Models.Camion.Remorque();
            remorque_1.Immatriculation = "Fra_1";
            remorque_1.TypeMarchandise = Models.Marchandise.TypeMarchandise.Frais;
            remorque_1.PrixJournalier = 40f;
            remorque_1.Capacite = 30f;

            remorqueDao.Create(remorque_1);

            Models.Camion.Remorque remorque_2 = new Models.Camion.Remorque();
            remorque_2.Immatriculation = "Ess_1";
            remorque_2.TypeMarchandise = Models.Marchandise.TypeMarchandise.Essence;
            remorque_2.PrixJournalier = 20f;
            remorque_2.Capacite = 30f;

            remorqueDao.Create(remorque_2);
        }
    }
}