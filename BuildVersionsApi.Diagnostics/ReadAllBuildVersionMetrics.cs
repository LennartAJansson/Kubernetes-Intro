namespace BuildVersionsApi.Diagnostics;

using System.Diagnostics.Metrics;

using Microsoft.Extensions.Logging;

public class ReadAllBuildVersionMetrics
{
  private readonly Counter<int> _valuesAllReadCounter;
  private readonly Counter<int> _valuesReadCounter;
  private readonly ILogger<ReadAllBuildVersionMetrics> logger;

  public ReadAllBuildVersionMetrics(ILogger<ReadAllBuildVersionMetrics> logger,IMeterFactory meterFactory)
  {
    var meter = meterFactory.Create("endpoints-read");
    _valuesAllReadCounter = meter.CreateCounter<int>("buildversions-allprojects-read", "requests_total", "Amount of requests");
    _valuesReadCounter = meter.CreateCounter<int>("buildversions-projects-read", "requests_total","Amount of requests");
    this.logger = logger;
  }

  public void CountReadAll(int quantity)
  {
    logger.LogInformation("ReadAllBuildVersionMetrics.CountReadAll: {quantity}", quantity);
    _valuesAllReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", "ReadAll"));
  }

  public void CountReadByName(string projectName, int quantity)
  {
    logger.LogInformation("ReadAllBuildVersionMetrics.CountReadByName: {projectName}, {quantity}", projectName, quantity);
    _valuesReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", projectName));
  }
  public void CountReadById(string projectName, int quantity)
  {
    logger.LogInformation("ReadAllBuildVersionMetrics.CountReadById: {projectName}, {quantity}", projectName, quantity);
    _valuesReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", projectName));
  }
}