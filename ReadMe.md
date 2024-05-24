EXAMPLE OF PROMETHEUS QUERIES:
container_last_seen{pod="nats-0"}
http_request_duration_seconds_sum{method="GET",controller="Query"}
workloadsapi_controllers_executiontime{path="/api/Query/GetAssignments"}
workloadsapi_controllers_executiontime{path="/api/Query/GetPeople"}
workloadsapi_controllers_executiontime{path="/api/Query/GetWorkloads"}
http_request_duration_seconds_sum{method=~"GET|POST|PUT|DELETE",controller=~"Query|Command"}
workloadsapi_controllers_executiontime{path=~"/api/Query/GetAssignments|/api/Query/GetPeople|/api/Query/GetWorkloads|/api/Command/CreatePerson|/api/Command/CreateAssignment|/api/Command/CreateWorkload|/api/Command/UpdateWorkload"}
container_memory_usage_bytes{container=~"cronjob|buildversion|workloadsapi|workloadsprojector|countriesapi"}