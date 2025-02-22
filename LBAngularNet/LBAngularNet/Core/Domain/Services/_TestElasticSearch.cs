using LBAngularNet.Core.Domain.Entities;
using Nest;

namespace LBAngularNet.Core.Domain.Services
{
    public class _TestElasticSearch
    {
        private readonly ElasticClient _client;

        public _TestElasticSearch (ElasticClient client) 
        {
            _client = client;
        }

        public async Task IndexarDemo(Demo demo)
        {
            await _client.IndexDocumentAsync(demo);
        }

        public async Task<List<Demo>> BuscarDemo (string Nombre) 
        {
            var response = await _client.SearchAsync<Demo>(s => s.Query(q => q.Match(m => m.Field(f => f.Name).Query(Nombre))));

            return response.Documents.ToList();
        }

        public async Task<Demo?> ObtenerDemo(int id)
        {
            var response = await _client.GetAsync<Demo>(id);
            return response.Source;
        }


        public async Task<bool> EliminarDemo(int id)
        {
            var response = await _client.DeleteAsync<Demo>(id);
            return response.IsValid;
        }
    }
}
