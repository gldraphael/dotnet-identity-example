namespace Web
{

    // Add roles to this enum and perform GET:/dev/seed to add the roles to the database

    public enum Role
    {
        Admin,
        Role1,
        Role2
    }

    public static class RoleExtensions
    {
        public static string GetRoleName(this Role role)
        {
            return role.ToString();
        }
    }
}
