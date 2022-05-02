param (
  $output         = $PSScriptRoot,
  $project        = "NasLandingPage",
  $configuration  = "Release"
)

$outputRoot       = Join-Path $output "\";
$workingRoot      = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot "\..\"));
$sourceDir        = Join-Path $workingRoot "src";
$projectRootDir   = Join-Path $sourceDir ($project + "\");
$artifactDir      = Join-Path $outputRoot "artifacts";
$publisRoot       = Join-Path $artifactDir "publish";
$publishDir       = Join-Path $publisRoot ($project + "\");

# =============================================================================
# Build project
# =============================================================================
#
$buildCmd         = "dotnet build $projectRootDir --configuration $configuration --output `"$publishDir`""
Write-Host        "Running Build: $buildCmd"
Invoke-Expression $buildCmd;