namespace GraphLogger.Infrastructure.Common.Models;

public class Timer
{
    private int _hour;
    /// <summary>
    /// Gets or sets the hour value of the time.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the hour value is outside the valid range of 0-23.
    /// </exception>
    public int Hour
    {
        get => _hour;
        set
        {
            if (value < 0 || value > 24)
            {
                throw new ArgumentOutOfRangeException("Minute value must be between 0-23.");
            }

            _hour = value;
        }
    }

    private int _minute;
    /// <summary>
    /// Gets or sets the minute value of the time.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the minute value is outside the valid range of 0-59.
    /// </exception>
    public int Minute
    {
        get => _minute;
        set
        {
            if (value < 0 || value > 59)
            {
                throw new ArgumentOutOfRangeException("Minute value must be between 0-59.");
            }

            _minute = value;
        }
    }
}
