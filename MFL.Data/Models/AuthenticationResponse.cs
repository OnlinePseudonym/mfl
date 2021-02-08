using MFL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MFL.Data.Models
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(AuthenticationRequest user, string status)
        {
            Username = user.Username;
            Status = status;
            IsAuthenticated = !string.IsNullOrEmpty(status) && status == "OK";
        }

        public string Username { get; set; }
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Status { get; set; }
    }
}
