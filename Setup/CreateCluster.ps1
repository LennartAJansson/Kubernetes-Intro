k3d cluster create --config ./config.yaml

$port = (docker port registry).Split(':')[1]
$registryhost = "registry:5000"
$env:REGISTRYHOST=$registryhost

"Add following to C:\Windows\System32\drivers\etc\hosts (In linux /etc/hosts):"
"127.0.0.1 registry"
"127.0.0.1 mysql"
"127.0.0.1 mysql.local"
"127.0.0.1 nats"
"127.0.0.1 nats.local"
"127.0.0.1 prometheus"
"127.0.0.1 prometheus.local"
"127.0.0.1 grafana"
"127.0.0.1 grafana.local"
"127.0.0.1 redis"
"127.0.0.1 redis.local"
""

# Verify that you have connection to your registry
"Trying to connect to registry:"
SETX /M REGISTRYHOST $registryhost
$env:REGISTRYHOST=$registryhost
curl.exe http://$registryhost/v2/_catalog

"To remove everything regarding cluster, loadbalancer and registry:"
"k3d cluster delete local"

kubectl apply -k ./mysql
kubectl apply -k ./sealedsecrets
kubectl apply -k ./nats
kubectl apply -k ./redis
kubectl apply -k ./prometheus
kubectl apply -k ./grafana

