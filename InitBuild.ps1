param (
   [string]$target = "local"
)

$name = "BuildVersionsApi"
$registryHost = "registry.${target}:5000"
$registryHost

$semanticVersion = "0.0.0.dev-1"
$version = "0.0.0.1"
$configuration = "Production"

$lowerName = $name.ToLower()
$branch = git rev-parse --abbrev-ref HEAD
$commit = git log -1 --pretty=format:"%H"
$description = "${branch}: ${commit}"

"Current build: ${registryHost}/${lowerName}:${semanticVersion}"

docker build -f ./${name}/Dockerfile --force-rm -t ${registryHost}/${lowerName}:${semanticVersion} --build-arg Version="${version}" --build-arg Configuration="${configuration}" --build-arg Description="${description}" .
docker push ${registryHost}/${lowerName}:${semanticVersion}
