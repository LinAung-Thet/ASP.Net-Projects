namespace Common.Domain.Services
{
    public interface IDateTimeProvider
    {
        DateTimeOffset UtcNow();
    }
}
