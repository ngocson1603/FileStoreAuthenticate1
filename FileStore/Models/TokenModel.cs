using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStore.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
