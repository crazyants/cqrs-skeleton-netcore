using Nest;
using Uranus.Commercial.QueryStack.Elastic;

namespace Uranus.Commercial.Desnormalizer.Projections.Common
{
    public abstract class ProjectionsBase
    {
        protected readonly ElasticSearchContextProvider _elasticSearchContextProvider;

        protected ElasticClient Context
        {
            get
            {
                return _elasticSearchContextProvider.GetContext();
            }
        }

        public ProjectionsBase(ElasticSearchContextProvider elasticContextProvider)
        {
            _elasticSearchContextProvider = elasticContextProvider;
        }
    }
}
