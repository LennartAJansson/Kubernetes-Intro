namespace BuildVersionsApi.Diagnostics;

using System.Diagnostics.Metrics;

public class ReadAllBuildVersionMetrics
{
  private readonly Counter<int> _valuesAllReadCounter;
  private readonly Counter<int> _valuesReadCounter;

  public ReadAllBuildVersionMetrics(IMeterFactory meterFactory)
  {
    var meter = meterFactory.Create("endpoints-read");
    _valuesAllReadCounter = meter.CreateCounter<int>("buildversions-allprojects-read", "requests_total", "Amount of requests");
    _valuesReadCounter = meter.CreateCounter<int>("buildversions-projects-read", "requests_total","Amount of requests");
  }

  public void CountReadAll(int quantity)
  {
    _valuesAllReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", "ReadAll"));
  }

  public void CountReadByName(string projectName, int quantity)
  {
    _valuesReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", projectName));
  }
  public void CountReadById(string projectName, int quantity)
  {
    _valuesReadCounter.Add(quantity,
        new KeyValuePair<string, object?>("requests_total", projectName));
  }
}