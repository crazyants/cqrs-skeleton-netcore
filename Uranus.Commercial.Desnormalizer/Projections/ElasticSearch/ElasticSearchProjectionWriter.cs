using Uranus.Commercial.Desnormalizer.Projections.Common;
using Uranus.Commercial.QueryStack.Common;
using Uranus.Commercial.QueryStack.Elastic;

namespace Uranus.Commercial.Desnormalizer.Projections.ElasticSearch
{
    public class ElasticSearchProjectionWriter : 
        ProjectionsBase, 
        IProjectionWriter
    {
        public ElasticSearchProjectionWriter(ElasticSearchContextProvider elasticContextProvider) 
            : base(elasticContextProvider)
        { }

        #region [ IProjectionWriter ]

        void IProjectionWriter.Add<T>(T view)
        {
            Context.Index<T>(view, i => i.Id(view.Id.ToString()));
        }

        void IProjectionWriter.Delete<T>(T view)
        {
            Context.Delete<T>(view.Id.ToString());
        }

        void IProjectionWriter.Update<T>(T view)
        {
            Context.Update<T, IProjectionView>(
                view.Id.ToString(),
                des => des.Doc(view)
            );
        }

        #endregion
    }
}
