using SAE_API.Models.EntityFramework;

namespace SAE_API.Models.EntityFramework
{
    public partial class CompteClient
    {
        public CompteClient(int idCompteClient, string? nomClient, string? prenomClient, string? civiliteClient, string? numeroClient, string? email, DateTime? datenaissanceClient, string? password, string? clientRole, ICollection<Enregistrer> enregistrerCompteClient, ICollection<Effectuer> effectuerCompteClient, ICollection<Acquerir> acquisComptes, ICollection<Favoris> favorisCompteClient, ICollection<Transation> transationCompteClient, Adresse adresseCompteClient, CompteAdmin compteAdminCompteClient, CompteClientProfessionnel compteClientProfessionnelCompteClient, ICollection<Estimer> estimerCompteClient, ICollection<RepriseMoto> repriseMotoCompteClient)
        {
            IdCompteClient = idCompteClient;
            NomClient = nomClient;
            PrenomClient = prenomClient;
            CiviliteClient = civiliteClient;
            NumeroClient = numeroClient;
            Email = email;
            DatenaissanceClient = datenaissanceClient;
            Password = password;
            ClientRole = clientRole;
            EnregistrerCompteClient = enregistrerCompteClient;
            EffectuerCompteClient = effectuerCompteClient;
            AcquisComptes = acquisComptes;
            FavorisCompteClient = favorisCompteClient;
            TransationCompteClient = transationCompteClient;
            AdresseCompteClient = adresseCompteClient;
            CompteAdminCompteClient = compteAdminCompteClient;
            CompteClientProfessionnelCompteClient = compteClientProfessionnelCompteClient;
            EstimerCompteClient = estimerCompteClient;
            RepriseMotoCompteClient = repriseMotoCompteClient;
        }

        public override bool Equals(object? obj)
        {
            return obj is CompteClient client &&
                   IdCompteClient == client.IdCompteClient &&
                   NomClient == client.NomClient &&
                   PrenomClient == client.PrenomClient &&
                   CiviliteClient == client.CiviliteClient &&
                   NumeroClient == client.NumeroClient &&
                   Email == client.Email &&
                   DatenaissanceClient == client.DatenaissanceClient &&
                   EqualityComparer<string?>.Default.Equals(Password, client.Password) &&
                   ClientRole == client.ClientRole &&
                   EqualityComparer<ICollection<Enregistrer>>.Default.Equals(EnregistrerCompteClient, client.EnregistrerCompteClient) &&
                   EqualityComparer<ICollection<Effectuer>>.Default.Equals(EffectuerCompteClient, client.EffectuerCompteClient) &&
                   EqualityComparer<ICollection<Acquerir>>.Default.Equals(AcquisComptes, client.AcquisComptes) &&
                   EqualityComparer<ICollection<Favoris>>.Default.Equals(FavorisCompteClient, client.FavorisCompteClient) &&
                   EqualityComparer<ICollection<Transation>>.Default.Equals(TransationCompteClient, client.TransationCompteClient) &&
                   EqualityComparer<Adresse>.Default.Equals(AdresseCompteClient, client.AdresseCompteClient) &&
                   EqualityComparer<CompteAdmin>.Default.Equals(CompteAdminCompteClient, client.CompteAdminCompteClient) &&
                   EqualityComparer<CompteClientProfessionnel>.Default.Equals(CompteClientProfessionnelCompteClient, client.CompteClientProfessionnelCompteClient) &&
                   EqualityComparer<ICollection<Estimer>>.Default.Equals(EstimerCompteClient, client.EstimerCompteClient) &&
                   EqualityComparer<ICollection<RepriseMoto>>.Default.Equals(RepriseMotoCompteClient, client.RepriseMotoCompteClient);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdCompteClient);
            hash.Add(NomClient);
            hash.Add(PrenomClient);
            hash.Add(CiviliteClient);
            hash.Add(NumeroClient);
            hash.Add(Email);
            hash.Add(DatenaissanceClient);
            hash.Add(Password);
            hash.Add(ClientRole);
            hash.Add(EnregistrerCompteClient);
            hash.Add(EffectuerCompteClient);
            hash.Add(AcquisComptes);
            hash.Add(FavorisCompteClient);
            hash.Add(TransationCompteClient);
            hash.Add(AdresseCompteClient);
            hash.Add(CompteAdminCompteClient);
            hash.Add(CompteClientProfessionnelCompteClient);
            hash.Add(EstimerCompteClient);
            hash.Add(RepriseMotoCompteClient);
            return hash.ToHashCode();
        }

        public static bool operator ==(CompteClient? left, CompteClient? right)
        {
            return EqualityComparer<CompteClient>.Default.Equals(left, right);
        }

        public static bool operator !=(CompteClient? left, CompteClient? right)
        {
            return !(left == right);
        }
    }
}
