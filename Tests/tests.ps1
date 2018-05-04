<#
import-module "C:\Program Files\WindowsPowerShell\Modules\pester"
invoke-pester ..\..\..\tests

#>

Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest

<#
		g
		|
	a-b-c-f
	  | |
	  d h
	  |
	  e
#>


$a = [tsltest.node]::new("A")
$b = [tsltest.node]::new("B")
$c = [tsltest.node]::new("C")
$d = [tsltest.node]::new("D")
$e = [tsltest.node]::new("E")
$f = [tsltest.node]::new("F")
$g = [tsltest.node]::new("G")
$h = [tsltest.node]::new("H")

Add-GEVertex -Vertex $a
Add-GEVertex -Vertex $b
Add-GEVertex -Vertex $c
Add-GEVertex -Vertex $d
Add-GEVertex -Vertex $e
Add-GEVertex -Vertex $f
Add-GEVertex -Vertex $g
Add-GEVertex -Vertex $h

Get-GECellCount

Add-GEEdge -from $a -to $b
Add-GEEdge -from $b -to $c
Add-GEEdge -from $c -to $f

Add-GEEdge -from $b -to $d
Add-GEEdge -from $d -to $e

Add-GEEdge -from $c -to $h
Add-GEEdge -from $c -to $g