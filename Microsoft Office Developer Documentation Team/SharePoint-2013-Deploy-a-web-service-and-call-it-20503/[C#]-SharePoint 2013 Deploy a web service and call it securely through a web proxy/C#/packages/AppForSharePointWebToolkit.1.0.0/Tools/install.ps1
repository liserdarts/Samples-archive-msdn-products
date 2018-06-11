param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath common.ps1) -Force

try {

    # Indicates if current project is a VB project
    $IsVbProject = ($project.CodeModel.Language -eq [EnvDTE.CodeModelLanguageConstants]::vsCMLanguageVB)

    if ($IsVbProject) {

        # For VB project, delete tokenhelper.cs
        $project.ProjectItems | Where-Object { $_.Name -eq "TokenHelper.cs" } | ForEach-Object { $_.Delete() }

        # Add Imports for VB project
        $VbImports | ForEach-Object {
            if (!($project.Object.Imports -contains $_)) {
                $project.Object.Imports.Add($_)
            }
        }
    }
    else {
        # For CSharp project, delete tokenhelper.vb
        $project.ProjectItems | Where-Object { $_.Name -eq "TokenHelper.vb" } | ForEach-Object { $_.Delete() }
    }

    # Set CopyLocal = True as needed
    Foreach ($spRef in $CopyLocalReferences) {
        $project.Object.References | Where-Object { $_.Name -eq $spRef } | ForEach-Object { $_.CopyLocal = $True }
    }

} catch {

    Write-Host "Error while installing package: " + $_.Exception -ForegroundColor Red
    exit
}