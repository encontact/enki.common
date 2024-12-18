# To learn makefiles: https://makefiletutorial.com/
# On windows, use NMake: https://docs.microsoft.com/pt-br/cpp/build/reference/nmake-reference?view=msvc-160
dotnetFramework = net5.0
packFramework = netstandard2.0
solution = ./enki.common.core.sln
libProject = ./src/enki.common.core/enki.common.core.csproj
nuspec = ./enki.common.core.nuspec
distPath = ./dist
artifactDir = ./artifacts
nupkgFile = $(shell find ./artifacts -type f -name '*.nupkg')

show-pack:
	echo "${nupkgFile}"

run-clean: clean restore build

all : clean restore build

clean:
	dotnet clean ${solution}

restore:
	dotnet restore ${solution}

build:
	dotnet build -c Release ${solution}

run-test:
	dotnet test ${solution}

publish:
	dotnet publish ${libProject} -c Release -o out/Release/

pack:
	dotnet pack -c Release -o ${artifactDir} ${libProject}

push-pack:
	dotnet nuget push ${nupkgFile} --api-key ${NUGET_API} --source https://api.nuget.org/v3/index.json

# Mais em: https://renatogroffe.medium.com/net-nuget-atualizando-packages-via-linha-de-comando-b0c6b596ed2
# Para instalar dependência: dotnet tool install --local dotnet-outdated-tool
update-dependencies:
	dotnet tool restore
	dotnet dotnet-outdated -u:Prompt ${solution}

# Mais em: https://devblogs.microsoft.com/nuget/how-to-scan-nuget-packages-for-security-vulnerabilities/
check-vulnerabilities:
	dotnet tool restore
	dotnet list package --vulnerable
	dotnet security-scan ${solution} --excl-proj=tests/encontact.application.test/**

# Inicializa a estrutura de versionamento no sistema
init-versioning:
	dotnet nbgv install

# Detalhes de versionamento em:
# https://github.com/dotnet/Nerdbank.GitVersioning/blob/master/doc/nbgv-cli.md
prepare-release:
	dotnet tool restore
	export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 && dotnet nbgv prepare-release --format json

# Altera o sistema preparando uma nova estrutura de versão base.
# https://github.com/dotnet/Nerdbank.GitVersioning/blob/main/doc/nbgv-cli.md#customizing-the-next-version
# Outra forma: nbgv prepare-release --versionIncrement Major
prepare-next-version:
	dotnet tool restore
	export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 && dotnet nbgv prepare-release --nextVersion ${NextReleaseNumber} --format json

# Mesmo após gerar a Tag é necessário enviar a tag para o servidor
tag-release:
#   Pegando o resultado e separando apenas o comando de PUSH: cat dist/tagged-version.txt | cut -d ' ' -f 7-
	dotnet tool restore
	export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 && dotnet nbgv tag