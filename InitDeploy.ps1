$name = "buildversionsapi"
$namespace = "buildversions"
$registryHost = "registry:5000"
$kubeseal = "c:\apps\kubeseal\kubeseal.exe"
$semanticVersion = "0.0.0-dev.1"
"Current deploy: ${registryHost}/${name}:${semanticVersion}"

cd ./deploy/${name}

kustomize edit set image "${registryHost}/${name}:${semanticVersion}"

if(Test-Path -Path ./secrets/*)
{
	"Creating secrets"
	kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
		&${kubeseal} -n $namespace --controller-namespace kube-system --format yaml > "secret.yaml"
}

cd ../..

kubectl apply -k ./deploy/${name}
