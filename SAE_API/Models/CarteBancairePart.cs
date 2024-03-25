using SAE_API.Models.EntityFramework;

namespace SAE_API.Models.EntityFramework
{
    public partial class CarteBancaire
    {
        public CarteBancaire(Int32 idCb, String nomCarte, String numeroCb, Int32? moisExpiration, Int32? anneeExpiration, String cryptoCb, ICollection<Acquerir> acquisCB)
        {
            this.IdCb = idCb;
            this.NomCarte = nomCarte;
            this.NumeroCb = numeroCb;
            this.MoisExpiration = moisExpiration;
            this.AnneeExpiration = anneeExpiration;
            this.CryptoCb = cryptoCb;
            this.AcquisCB = acquisCB;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj is CarteBancaire bancaire &&
                   this.IdCb == bancaire.IdCb &&
                   this.NomCarte == bancaire.NomCarte &&
                   this.NumeroCb == bancaire.NumeroCb &&
                   this.MoisExpiration == bancaire.MoisExpiration &&
                   this.AnneeExpiration == bancaire.AnneeExpiration &&
                   this.CryptoCb == bancaire.CryptoCb &&
                   EqualityComparer<ICollection<Acquerir>>.Default.Equals(this.AcquisCB, bancaire.AcquisCB);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(this.IdCb, this.NomCarte, this.NumeroCb, this.MoisExpiration, this.AnneeExpiration, this.CryptoCb, this.AcquisCB);
        }

        public static Boolean operator ==(CarteBancaire? left, CarteBancaire? right)
        {
            return EqualityComparer<CarteBancaire>.Default.Equals(left, right);
        }

        public static Boolean operator !=(CarteBancaire? left, CarteBancaire? right)
        {
            return !(left == right);
        }
    }
}
