using Dapper;
using LBAngularNet.Core.Domain.Entities;
using System.Data.SqlClient;

namespace LBAngularNet.Application.Queries
{
    public class DemoQueries
    {
        public IEnumerable<Demo> ObtenerDemo()
        {
            using (var conexion= new SqlConnection("conn"))
            {
                conexion.Open();
                var demos = conexion.Query<Demo>("Select Id, Nombre FROM Demo");
                return demos;

            }
        }
    }
}
