using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NasLandingPage.Auth;

/// <summary>
/// Provides a randomly generating PKCE code verifier and it's corresponding code challenge.
/// </summary>
public class Pkce
{
  public string CodeVerifier;
  public string CodeChallenge;

  public Pkce(uint size = 128)
  {
    CodeVerifier = GenerateCodeVerifier(size);
    CodeChallenge = GenerateCodeChallenge(CodeVerifier);
  }

  public static string GenerateCodeVerifier(uint size = 128)
  {
    if (size < 43 || size > 128)
      size = 128;

    const string unreservedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
    Random random = new Random();
    char[] highEntropyCryptograph = new char[size];

    for (int i = 0; i < highEntropyCryptograph.Length; i++)
    {
      highEntropyCryptograph[i] = unreservedCharacters[random.Next(unreservedCharacters.Length)];
    }

    return new string(highEntropyCryptograph);
  }

  public static string GenerateCodeChallenge(string codeVerifier)
  {
    using var sha256 = SHA256.Create();
    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
    return Base64UrlEncoder.Encode(challengeBytes);
  }
}
