$name = "BuildVersionsApi"
$registryHost = "registry:5000"
$semanticVersion = "0.0.0-dev.2"
$configuration = "Production"
$url = "https://localhost:7087"
$curl = "curl.exe"

$lowerName = $name.ToLower()
$branch = git rev-parse --abbrev-ref HEAD
$commit = git log -1 --pretty=format:"%H"
$description = "${branch}: ${commit}"

"Current build: ${registryHost}/${lowerName}:${semanticVersion}"

docker build -f ./${name}/Dockerfile --force-rm -t ${registryHost}/${lowerName}:${semanticVersion} --build-arg Version="${semanticVersion}" --build-arg Configuration="${configuration}" --build-arg Description="${description}" .
docker push ${registryHost}/${lowerName}:${semanticVersion}

while ($alive -ne "pong") 
{
	Start-Sleep -s 5
	"Trying to detect api"
	$alive = &${curl} -s "${url}/Ping" -H "accept: text/plain"
}

&${curl} -X 'POST' '${url}/api/BuildVersion/Create/v1' -H 'accept: application/json' -H 'Content-Type: application/json' \
  -d '{"projectName":"${name}","major":0,"minor":0,"build":0,"revision":1,"semanticVersionText":"{Major}.{Minor}.{Build}.dev.{Revision}"}'
