namespace ScheduledMeets.Persistence;
public class DbConnectionOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public void Deconstruct(out string connectionString, out string password) =>
        (connectionString, password) = (ConnectionString, Password);
}