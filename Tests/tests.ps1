<#
import-module "C:\Program Files\WindowsPowerShell\Modules\pester"
invoke-pester ..\..\..\tests

#>

Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest

$cell = [tsltest.VNET]::new()
Add-GEVertex -Vertex $cell
Get-GECellCount
