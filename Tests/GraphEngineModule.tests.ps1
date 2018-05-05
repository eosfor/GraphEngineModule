Describe "Tests" {
    It "Initialize a server" {
        Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
    }
    It "Compiles and loads the TSL file" {
        Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest
    }
    It "Receives names of the type" {
        Get-GETypeName
    }
    It "Generates and adds a generic cell" {
        $cell = [tsltest.node]::new()
        Add-GEVertex -Vertex $cell
    }
    It "Reads the cell" {
        $cell = [tsltest.node]::new()
        Add-GEVertex -Vertex $cell
        $r1 = Get-GEVertex -NodeID $cell.CellId
    }
    #this one stucks for some reason!!
    It "Query the Storage for Nodes by TypeName" {
        $res = Get-GEVertex -TypeName "Node"
        $res | Should -Not -BeNullOrEmpty
    }
    It "Adds a link to a cell" {             
        $cellFrom = [tsltest.node]::new()
        $cellTo = [tsltest.node]::new()

        $cellFrom | Should -Not -BeNullOrEmpty
        $cellTo | Should -Not -BeNullOrEmpty

        Add-GEVertex -Vertex $cellFrom
        Add-GEVertex -Vertex $cellTo

        Add-GEEdge -From $cellFrom -To $cellTo

        $res = Get-GEVertex -NodeID $cellFrom.CellId

        $res.OutEdge | Should -Not -BeNullOrEmpty
    }

	it "Generate a graph and run LIKQ query" {
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

		Get-GECellCount

		$query = "MAG
					.StartFrom($($d.CellId))
					.FollowEdge(`"OutEdge`")
					.VisitNode(_ => Action.Continue & Action.Return)
					.FollowEdge(`"OutEdge`")
					.VisitNode(_ => Action.Continue & Action.Return)"

		Get-GEVertex -Query $query
	}

}