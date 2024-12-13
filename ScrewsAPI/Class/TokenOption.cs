using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ScrewsAPI.Class
{
    public class TokenOption
    {
        public const string ISSUER = "MyTokenServer"; // издатель токена
        public const string AUDIENCE = "MyTokenClient"; // потребитель токена
        const string KEY = "mysuper_secretsecretsecretkey123hagivagi12";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
