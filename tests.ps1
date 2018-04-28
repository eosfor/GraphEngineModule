<#
import-module "C:\Program Files\WindowsPowerShell\Modules\pester"
invoke-pester ..\..\..\tests

#>

Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest
Get-GETypeName

$cell = [tsltest.VNET]::new()
Add-GEVertex -Vertex $cell

$cell = [tsltest.VNET]::new()
Add-GEVertex -Vertex $cell
$r1 = Get-GEVertex -NodeID $cell.CellId


$res = Get-GEVertex -TypeName "VNET"


$cellFrom = [tsltest.VNET]::new()
$cellTo = [tsltest.VNET]::new()

$cellFrom | Should -Not -BeNullOrEmpty
$cellTo | Should -Not -BeNullOrEmpty

Add-GEVertex -Vertex $cellFrom
Add-GEVertex -Vertex $cellTo

Add-GEEdge -From $cellFrom -To $cellTo

$res2 = Get-GEVertex -NodeID $cellFrom.CellId
