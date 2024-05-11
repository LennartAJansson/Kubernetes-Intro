namespace BuildVersionsApi.Persistance.Interceptor;

using System;
using System.Data.Common;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore.Diagnostics;

public class PerformanceInterceptor : DbCommandInterceptor
{
  private const long QuerySlowThreshold = 100; // milliseconds

  public override InterceptionResult<DbDataReader> ReaderExecuting(
      DbCommand command,
      CommandEventData eventData,
      InterceptionResult<DbDataReader> result)
  {
    Stopwatch stopwatch = Stopwatch.StartNew();

    InterceptionResult<DbDataReader> originalResult = base.ReaderExecuting(command, eventData, result);

    stopwatch.Stop();
    if (stopwatch.ElapsedMilliseconds > QuerySlowThreshold)
    {
      Console.WriteLine($"Slow Query Detected: {command.CommandText}");
    }

    return originalResult;
  }
}