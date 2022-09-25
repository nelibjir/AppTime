namespace AppTime
{
  public class Security
  {
    public const string _BasicAuthentication = "BasicAuthentication";

    public const string _AuthorizationHeaderName = "Authorization";
    public const string _HeaderMissing = "Authorization header missing.";
    public const string _InvalidAuthorizationHeader = "Invalid authorization header.";
    public const string _InvalidEmailOrPassword = "Invalid email or password.";

    public class RolePolicy
    {
      public const string _PolicyName = "RolePolicy";

      /// <summary>
      /// Pouze admin
      /// </summary>
      public const string _Admin = "Admin";


      /// <summary>
      /// Bez autorizace - povoleny všechny role
      /// </summary>
      public const string _Public = "Public";
    }
  }
}
