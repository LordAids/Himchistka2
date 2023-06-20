using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Himchistka.Api.DTO
{
    public class LoginViewModel
    {
        /// <summary>
        ///
        /// </summary>
        [DisplayName("userName")]
        public string UserName { get; set; }
        /// <summary>
        ///
        /// </summary>        
        [DataType(DataType.Password)]
        [DisplayName("password")]
        public string Password { get; set; }
    }
}
