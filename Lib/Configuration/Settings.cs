namespace Lib.Configuration;

public sealed record Settings
{
    public Features Features { get; init; }
    public ServiceType ServiceType { get; init; }
}