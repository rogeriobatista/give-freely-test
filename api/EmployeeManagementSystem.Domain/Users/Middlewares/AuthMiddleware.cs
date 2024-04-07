using EmployeeManagementSystem.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeManagementSystem.Domain.Users.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public AuthMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.Length > 7)
            {
                var token = DecodeTokenFromHeader(authHeader);

                _authService.SetCurrentUserFromAuthToken(token);
            }

            await _next.Invoke(context);
        }

        private JwtSecurityToken DecodeTokenFromHeader(string header)
        {
            var handler = new JwtSecurityTokenHandler();

            header = header.Replace("Bearer ", "");

            return handler.ReadToken(header) as JwtSecurityToken;
        }
    }
}
