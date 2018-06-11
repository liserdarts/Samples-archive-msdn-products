param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath common.ps1) -Force

try {

    # Indicates if current project is a VB project
    $IsVbProject = ($project.CodeModel.Language -eq [EnvDTE.CodeModelLanguageConstants]::vsCMLanguageVB)
    
    if ($IsVbProject) {

        # Remove added Imports for VB project
        $VbImports | ForEach-Object {
            if ($project.Object.Imports -contains $_) {
                $project.Object.Imports.Remove($_)
            }
        }
    }
    
    # Remove references
    Foreach ($spRef in $ReferencesToRemoveWhenUninstall) {
        $project.Object.References | Where-Object { $_.Name -eq $spRef } | ForEach-Object { $_.Remove() }
    }
    
} catch {

    Write-Host "Error uninstalling package: " + $_.Exception -ForegroundColor Red
    exit
}