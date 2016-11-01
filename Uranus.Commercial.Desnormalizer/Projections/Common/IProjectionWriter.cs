using Uranus.Commercial.QueryStack.Common;

namespace Uranus.Commercial.Desnormalizer.Projections.Common
{
    public interface IProjectionWriter
    {
        void Add<T>(T view) where T : class, IProjectionView;

        void Delete<T>(T view) where T : class, IProjectionView;

        void Update<T>(T view) where T : class, IProjectionView;
    }
}