

namespace SAE_API.Models.EntityFramework
{
    public partial class ConfigurationMoto
    {
        public ConfigurationMoto(int idConfigurationMoto, int? idReservationOffre, int idMoto, int? idColoris, decimal? prixTotalConfiguration, DateTime? dateConfiguration, Moto motoConfigurationMoto, Coloris colorisConfigurationMoto, ICollection<Enregistrer> enregistrerConfigurationMoto, ICollection<AChoisi> aChoisiConfigurationMoto, ICollection<AChoisiOption> aChoisiOptionsConfigurationMoto, Commander commanderConfigurationMoto)
        {
            IdConfigurationMoto = idConfigurationMoto;
            IdReservationOffre = idReservationOffre;
            IdMoto = idMoto;
            IdColoris = idColoris;
            PrixTotalConfiguration = prixTotalConfiguration;
            DateConfiguration = dateConfiguration;
            MotoConfigurationMoto = motoConfigurationMoto;
            ColorisConfigurationMoto = colorisConfigurationMoto;
            EnregistrerConfigurationMoto = enregistrerConfigurationMoto;
            AChoisiConfigurationMoto = aChoisiConfigurationMoto;
            AChoisiOptionsConfigurationMoto = aChoisiOptionsConfigurationMoto;
            CommanderConfigurationMoto = commanderConfigurationMoto;
        }

        public override bool Equals(object? obj)
        {
            return obj is ConfigurationMoto moto &&
                   IdConfigurationMoto == moto.IdConfigurationMoto &&
                   IdReservationOffre == moto.IdReservationOffre &&
                   IdMoto == moto.IdMoto &&
                   IdColoris == moto.IdColoris &&
                   PrixTotalConfiguration == moto.PrixTotalConfiguration &&
                   DateConfiguration == moto.DateConfiguration &&
                   EqualityComparer<Moto>.Default.Equals(MotoConfigurationMoto, moto.MotoConfigurationMoto) &&
                   EqualityComparer<Coloris>.Default.Equals(ColorisConfigurationMoto, moto.ColorisConfigurationMoto) &&
                   EqualityComparer<ICollection<Enregistrer>>.Default.Equals(EnregistrerConfigurationMoto, moto.EnregistrerConfigurationMoto) &&
                   EqualityComparer<ICollection<AChoisi>>.Default.Equals(AChoisiConfigurationMoto, moto.AChoisiConfigurationMoto) &&
                   EqualityComparer<ICollection<AChoisiOption>>.Default.Equals(AChoisiOptionsConfigurationMoto, moto.AChoisiOptionsConfigurationMoto) &&
                   EqualityComparer<Commander>.Default.Equals(CommanderConfigurationMoto, moto.CommanderConfigurationMoto);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(IdConfigurationMoto);
            hash.Add(IdReservationOffre);
            hash.Add(IdMoto);
            hash.Add(IdColoris);
            hash.Add(PrixTotalConfiguration);
            hash.Add(DateConfiguration);
            hash.Add(MotoConfigurationMoto);
            hash.Add(ColorisConfigurationMoto);
            hash.Add(EnregistrerConfigurationMoto);
            hash.Add(AChoisiConfigurationMoto);
            hash.Add(AChoisiOptionsConfigurationMoto);
            hash.Add(CommanderConfigurationMoto);
            return hash.ToHashCode();
        }

        public static bool operator ==(ConfigurationMoto? left, ConfigurationMoto? right)
        {
            return EqualityComparer<ConfigurationMoto>.Default.Equals(left, right);
        }

        public static bool operator !=(ConfigurationMoto? left, ConfigurationMoto? right)
        {
            return !(left == right);
        }
    }
}
