foreach($name in @(
	"buildversionsapi"
))
{
	$namespace = "buildversions"
	$registryHost = "registry:5000"
	$kubeseal = "c:\apps\kubeseal\kubeseal.exe"
	$curl = "curl.exe"
	$url = "http://buildversionsapi.local:8080"
	$semanticVersion = "0.0.0-dev.2"
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

	#Restore secret.yaml and kustomization.yaml since this script alters them temporary
#	if(Test-Path -Path ./deploy/${name}/secret.yaml)
#	{
#		git checkout ./deploy/${name}/secret.yaml
#	}
#	git checkout ./deploy/${name}/kustomization.yaml

#	$alive = ""
#	while($alive -ne "pong")
#	{
#		"Trying to connect. Could take approx 30 seconds..."
#		Start-Sleep -Seconds 10
#		$alive = &${curl} -s "${url}/Ping"
#		"Result from &${curl} -s ${url}/Ping is: " + $alive
#	}

#	&${curl} -X 'POST' 'http://buildversionsapi.local:8080/buildversions/CreateProject' -H 'Content-Type: application/json' -d '{"projectName": "buildversionsapi", "major": 0, "minor": 0, "build": 0, "revision": 1, "semanticVersionText": "dev"}' 
#	&${curl} -X 'POST' 'http://buildversionsapi.local:8080/buildversions/CreateProject' -H 'Content-Type: application/json' -d '{"projectName": "buildversions", "major": 0, "minor": 0, "build": 0, "revision": 1, "semanticVersionText": "dev"}' 
#	""
#	"Done!"
}
