param (
   [string]$target = "local"
)

#Assumes you have the project buildversionsapi running on your localhost on port 9000
#

$namespace = "buildversions"
$hostname=""
$url = "http://buildversionsapi.${target}"
$registryHost = "registry:5000"

if($target -eq "local")
{
	$url = $url + ":8080"
	$kubeconfig = $env:KUBECONFIG
}
else
{
	$kubeconfig = $env:KUBECONFIGX
}

$curl = "curl.exe"
$configuration = "production"
$deploy = "Deploy"
$kubeseal = "C:/Apps/kubeseal/kubeseal"


$alive = &${curl} -s "${url}/api/Ping/v1" -H "accept: text/plain"
if($alive -ne """pong""")
{
	"You need to do an initial deploy of BuildVersionsApi"
	"Please run InitBuildVersion.ps1"
	return
}

kubectl apply -f ./deploy/${target}/namespace.yaml --kubeconfig $kubeconfig

foreach($name in @(
	"buildversions", 
	"buildversionsapi"
))
{
	$buildVersion = $null
	$buildVersion = &${curl} -s "${url}/api/BuildVersion/ReadByName/${name}/v1" | ConvertFrom-Json
	$semanticVersion = $buildVersion.semanticVersion

	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		"Could not connect to buildversionsapi"
		"Please check that BuildVersionsApi is working correctly in your Kubernetes"
		return
	}

	"Current deploy: ${registryHost}/${name}:${semanticVersion}"

	cd ./${deploy}/${target}/${name}

	kustomize edit set image "${registryHost}/${name}:${semanticVersion}"

	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets --kubeconfig $kubeconfig|
			&${kubeseal} -n "${namespace}" --controller-namespace kube-system --format yaml --kubeconfig $kubeconfig > "secret.yaml"
	}

	cd ../../..

	kubectl apply -k ./${deploy}/${target}/${name} --kubeconfig $kubeconfig
	
	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
	#if([string]::IsNullOrEmpty($env:AGENT_NAME) -and [string]::IsNullOrEmpty($hostname))
	#if([string]::IsNullOrEmpty($env:AGENT_NAME))
	#{
#		if(Test-Path -Path ${deploy}/${name}/secret.yaml)
#		{
#			git checkout ${deploy}/${name}/secret.yaml
#		}
	#}
}

