using Nest;
using System;

namespace Uranus.Commercial.QueryStack.Elastic
{
    public class ElasticSearchContextProvider
    {
        protected readonly ElasticClient _elasticClient;

        protected virtual Uri ConnectionString
        {
            get
            {
                return new Uri("http://localhost:9200");
            }
        }

        protected virtual string DefaultIndex
        {
            get
            {
                return "uranus-reader";
            }
        }

        public ElasticSearchContextProvider()
        {
            var settings = new ConnectionSettings(ConnectionString).PluralizeTypeNames();
            settings.DefaultIndex(DefaultIndex);

            _elasticClient = new ElasticClient(settings);
        }

        public ElasticClient GetContext()
        {
            return _elasticClient;
        }
    }
}
