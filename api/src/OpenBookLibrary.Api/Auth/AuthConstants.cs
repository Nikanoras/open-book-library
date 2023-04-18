namespace OpenBookLibrary.Api.Auth;

public static class AuthConstants
{
    public const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    public const string AdminUserPolicyName = "Admin";
    public const string AdminUserClaimValue = "admin";

    public const string TrustedMemberPolicyName = "Trusted";
    public const string TrustedMemberClaimValue = "trusted_member";
}