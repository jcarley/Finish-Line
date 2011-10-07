using NHibernate.Mapping.ByCode;

namespace FinishLine.Library.Repository.NHibernate.ClassMaps
{
    public interface IClassMap
    {
        void Map(ModelMapper mapper);
    }
}
