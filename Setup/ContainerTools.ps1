winget install --id Microsoft.Powershell --source winget

if($env:Path -notmatch "chocolatey")
{
	iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
	Set-Alias choco "C:\ProgramData\chocolatey\bin\choco.exe"
	choco feature enable -n allowGlobalConfirmation

	choco install -force nvm
	choco install -force conemu
	choco install -force curl
	choco install -force kubernetes-cli
	choco install -force kustomize
	choco install -force k9s
	choco install -force k3d
	choco install -force gh
	choco install -force openssl
      	choco install -force rancher-desktop
}
else
{
	choco upgrade chocolatey
	choco upgrade -force nvm
	choco upgrade -force conemu
	choco upgrade -force k3d
	choco upgrade -force curl
	choco upgrade -force kubernetes-cli
	choco upgrade -force kubernetes-helm
	choco upgrade -force kustomize
	choco upgrade -force k9s
	choco upgrade -force gh
	choco upgrade -force openssl
      	choco upgrade -force rancher-desktop
}
