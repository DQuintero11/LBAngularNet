using System.Text;

namespace LBAngularNet.Core.Infraestructure.Security
{
    public class Encrypt
    {
        public string Codificar(string txt)
        {
            string base64Encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(txt));
            return base64Encoded;
        }

        public string Decodificar (string txt)
        {
            string base64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(txt));
            return base64Decoded;
        }
    }
}
