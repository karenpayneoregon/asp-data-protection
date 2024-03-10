namespace Project1.Classes;
public class Utilities
{
    /// <summary>
    /// Get current environment from ASPNETCORE_ENVIRONMENT
    /// </summary>
    /// <returns>
    /// Current environment
    /// <para/><see cref="AspNetCoreEnvironment.Development"/>
    /// <para/><see cref="AspNetCoreEnvironment.Staging"/>
    /// <para/><see cref="AspNetCoreEnvironment.Production"/>
    /// </returns>
    public static AspNetCoreEnvironment CurrentEnvironment() => 
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
        {
            "Development" => AspNetCoreEnvironment.Development,
            "Staging" => AspNetCoreEnvironment.Staging,
            "Production" => AspNetCoreEnvironment.Production,
            _ => AspNetCoreEnvironment.Development
        };

}
public enum AspNetCoreEnvironment
{
    Development,
    Staging,
    Production
}
