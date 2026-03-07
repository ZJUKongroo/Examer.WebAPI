// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace Examer.Helpers;

public class JwtConfig
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expired { get; set; }
    public DateTime NotBefore => DateTime.Now;
    public DateTime Expiration => DateTime.Now.AddMinutes(Expired);
    public SymmetricSecurityKey SecurityKey => new(Encoding.UTF8.GetBytes(SecretKey));
    public SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha256);
}
