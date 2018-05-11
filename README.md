# GraphEngineModule

Some tests to figure out how to use [GraphEngine](https://www.graphengine.io) via PowerShell


Here is something that works so far

```powershell
Set-GEConfigOption -LogDirectory "C:\trinity-log"`
				   -StorageRoot "C:\storage"`
				   -LogEchoOnConsole $false `
				   -LogLevel ([Trinity.Diagnostics.LogLevel]::new()) `
				   -Verbose

Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest


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

Add-GEEdge -from $a -to $b
Add-GEEdge -from $b -to $c
Add-GEEdge -from $c -to $f

Add-GEEdge -from $b -to $d
Add-GEEdge -from $d -to $e

Add-GEEdge -from $c -to $h
Add-GEEdge -from $c -to $g
Add-GEEdge -from $g -to $c

Get-GECellCount

$query = "MAG
            .StartFrom($($g.CellId))
            .FollowEdge(`"OutEdge`")
            .VisitNode(_ => Action.Continue & Action.Return)
            .FollowEdge(`"OutEdge`")
            .VisitNode(_ => Action.Continue & Action.Return)"

Get-GEVertex -Query $query
```