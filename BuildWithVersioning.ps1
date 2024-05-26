$hostname=""
$url = "http://buildversionsapi.local:8080"
$curl = "curl.exe"
$configuration = "production"

$alive = &${curl} -s "${url}/api/Ping/v1" -H "accept: text/plain"
if($alive -ne """pong""")
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
	$json = "{""ProjectName"":""${name}"",""VersionElement"":""Revision""}"
#	"Sending json: " + $json
	$buildVersion = $null
	$buildVersion = &${curl} -s -X 'PUT' "${url}/api/BuildVersion/Increment/v1" -H 'accept: application/json' -H 'Content-Type: application/json' -d "$json" | ConvertFrom-Json
#	$buildVersion
	$semanticVersion = $buildVersion.semanticVersion
	$version = $buildVersion.version
	
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

	#docker build --progress=plain --no-cache -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} --build-arg Version="${version}" --build-arg Configuration="${configuration}" --build-arg Description="${description}" .
	docker build --progress=plain -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} --build-arg Version="${version}" --build-arg Configuration="${configuration}" --build-arg Description="${description}" .
	docker push ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}
}