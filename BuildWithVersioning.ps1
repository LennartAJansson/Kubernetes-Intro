$hostname=""
$url = "http://buildversionsapi.local:8080"
$curl = "curl.exe"
$configuration = "Production"

$alive = &${curl} -s "${url}/Ping" -H "accept: text/plain"
if($alive -ne "pong")
{
	"You need to do an initial deploy of BuildVersionsApi"
	"Please run InitBuildVersion.ps1"
	"Not responding to request ${url}/Ping"
	return
}

foreach($name in @(
	"BuildVersions", 
	"BuildVersionsApi"
))
{
	$lowerName = $name.ToLower()
	$branch = git rev-parse --abbrev-ref HEAD
	$commit = git log -1 --pretty=format:"%H"
	$description = "${branch}: ${commit}"
	$buildVersion = $null
	#https://localhost:7087/api/BuildVersion/ReadByName/BuildVersionsApi/v1
	$buildVersion = &${curl} -s "${url}/api/BuildVersion/ReadByName/$name/v1" | ConvertFrom-Json
	$semanticVersion = $buildVersion.semanticVersion
	
	if([string]::IsNullOrEmpty($semanticVersion) -or [string]::IsNullOrEmpty($description)) 
	{
		"Could not connect to git repo or buildversionsapi"
		"Please check that you are in the correct folder and that"
		"BuildVersionsApi is working correctly in your Kubernetes"
		return
	}
	
	"Current build: ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}"
	"Version: ${semanticVersion}"
	"Description: ${description}"

	docker build -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" --build-arg configuration="${configuration}" --build-arg Description="${description}" .
	docker push ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}
}