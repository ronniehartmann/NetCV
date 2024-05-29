namespace Application.Authentication;

public class LockoutService
{
    private readonly object _lock = new();
    private int _accessFailedCount;
    private DateTimeOffset? _lockoutEnd;

    public int GetAccessFailedCount()
    {
        lock (_lock)
        {
            return _accessFailedCount;
        }
    }

    public void IncrementAccessFailedCount()
    {
        lock (_lock)
        {
            _accessFailedCount++;
        }
    }

    public void ResetAccessFailedCount()
    {
        lock (_lock)
        {
            _accessFailedCount = 0;
        }
    }

    public DateTimeOffset? GetLockoutEndDate()
    {
        lock (_lock)
        {
            return _lockoutEnd;
        }
    }

    public void SetLockoutEndDate(DateTimeOffset? lockoutEnd)
    {
        lock (_lock)
        {
            _lockoutEnd = lockoutEnd;
        }
    }
}