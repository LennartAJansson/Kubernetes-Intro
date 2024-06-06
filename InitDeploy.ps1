param (
   [string]$target = "local"
)

$name = "buildversionsapi"
$namespace = "buildversions"
$registryHost = "registry:5000"
$kubeseal = "c:\apps\kubeseal\kubeseal.exe"
if($target -eq "local")
{
	$kubeconfig = $env:KUBECONFIG
}
else
{
	$kubeconfig = $env:KUBECONFIGX
}

$semanticVersion = "0.0.0.dev-9"
"Current deploy: ${registryHost}/${name}:${semanticVersion}"

kubectl apply -f ./deploy/${target}/namespace.yaml --kubeconfig $kubeconfig

cd ./deploy/${target}/${name}

kustomize edit set image "${registryHost}/${name}:${semanticVersion}"

if(Test-Path -Path ./secrets/*)
{
	"Creating secrets"
	kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets --validate=false --kubeconfig $kubeconfig |
		&${kubeseal} -n $namespace --controller-namespace kube-system --format yaml --validate=false --kubeconfig $kubeconfig > "secret.yaml"
}

cd ../../..

kubectl apply -k ./deploy/${target}/${name} --kubeconfig $kubeconfig
