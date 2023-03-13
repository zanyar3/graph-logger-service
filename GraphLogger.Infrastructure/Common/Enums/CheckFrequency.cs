namespace GraphLogger.Infrastructure.Common.Enums;

public enum CheckFrequency : int
{
    /// <summary>
    /// 15sec
    /// </summary>
    SEC_15 = 15000,

    /// <summary>
    /// 30sec
    /// </summary>
    SEC_30 = 30000,

    /// <summary>
    /// 1min
    /// </summary>
    MIN_1 = 60000,

    /// <summary>
    /// 2min
    /// </summary>
    MIN_2 = 120000,

    /// <summary>
    /// 5min
    /// </summary>
    MIN_5 = 300000,
}
