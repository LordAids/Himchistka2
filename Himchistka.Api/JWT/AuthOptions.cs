using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Himchistka.Api.JWT
{
    public class AuthOptions
    {
        /// <summary>
        ///
        /// </summary>
        public const string ISSUER = "kkokin"; // issuer
        /// <summary>
        ///
        /// </summary>
        public const string AUDIENCE = "http://localhost:5000/"; // audience

        // TODO: Generate a secret key
        const string KEY = "supersecretkey";
        /// <summary>
        ///
        /// </summary>
        public const int LIFETIME = 17280; // TTL 12 days, consider about less time
        /// <summary>
        ///
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
