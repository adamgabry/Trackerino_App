namespace Trackerino.App.Options;

public record DALOptions
{
    public LocalDbOptions? LocalDb { get; init; }
}

public record LocalDbOptions
{
    public bool Enabled { get; init; }
    public string ConnectionString { get; init; } = null!;
}
