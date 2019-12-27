using System;

namespace SampleGet.DTOs
{
    public class AuthorizationDTO
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}