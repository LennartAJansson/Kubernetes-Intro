param (
   [string]$target = "local"
)

$deploy = "deploy"
$name = "buildversionsapi"

if($target -eq "local")
{
	$kubeconfig = $env:KUBECONFIG
}
else
{
	$kubeconfig = $env:KUBECONFIGX
}

kubectl delete -k ./${deploy}/${target}/${name} --kubeconfig $kubeconfig