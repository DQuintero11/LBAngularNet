using Newtonsoft.Json;
using StackExchange.Redis;

namespace LBAngularNet.Core.Infraestructure.Cache.Redis
{
    public class RedisConn
    {
        private static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
        private static readonly IDatabase db = redis.GetDatabase();

        public void  _TestRedis()
        {
         // Guardar un valor en Redis
        db.StringSet("clave", "Hola, Redis!");
        // Recuperar el valor almacenado
        string valor = db.StringGet("clave");
        Console.WriteLine($"Valor desde Redis: {valor}");
        }

        public void _JsonTestRedis()
        {
            var usuario = new { Id = 123, Nombre = "John Doe", Email = "john@example.com" };
            string json = JsonConvert.SerializeObject(usuario);
            db.StringSet("usuario:123", json);
            // Recuperar y deserializar
            string jsonFromRedis = db.StringGet("usuario:123");
            var usuarioRecuperado = JsonConvert.DeserializeObject(jsonFromRedis);
            Console.WriteLine(jsonFromRedis);
        }

        public void _QueueTestRedis() 
        {
            db.ListRightPush("tareas", "Tarea 1");
            db.ListRightPush("tareas", "Tarea 2");
            // Obtener y eliminar el primer elemento
            string tarea = db.ListLeftPop("tareas");
            Console.WriteLine($"Tarea procesada: {tarea}");
        }


    }
}
