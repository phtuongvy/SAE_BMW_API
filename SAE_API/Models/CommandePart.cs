

namespace SAE_API.Models.EntityFramework
{
    public partial class Commande
    {
        public Commande(int idCommande, decimal? prixFraisLivraison, DateTime dateCommande, decimal? prixTotal, ICollection<Effectuer> effectuerCommande, ICollection<Provenance> provenanceCommande)
        {
            IdCommande = idCommande;
            PrixFraisLivraison = prixFraisLivraison;
            DateCommande = dateCommande;
            PrixTotal = prixTotal;
            EffectuerCommande = effectuerCommande;
            ProvenanceCommande = provenanceCommande;
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   IdCommande == commande.IdCommande &&
                   PrixFraisLivraison == commande.PrixFraisLivraison &&
                   DateCommande == commande.DateCommande &&
                   PrixTotal == commande.PrixTotal &&
                   EqualityComparer<ICollection<Effectuer>>.Default.Equals(EffectuerCommande, commande.EffectuerCommande) &&
                   EqualityComparer<ICollection<Provenance>>.Default.Equals(ProvenanceCommande, commande.ProvenanceCommande);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdCommande, PrixFraisLivraison, DateCommande, PrixTotal, EffectuerCommande, ProvenanceCommande);
        }

        public static bool operator ==(Commande? left, Commande? right)
        {
            return EqualityComparer<Commande>.Default.Equals(left, right);
        }

        public static bool operator !=(Commande? left, Commande? right)
        {
            return !(left == right);
        }
    }
}
