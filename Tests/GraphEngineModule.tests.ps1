Describe "Tests" {
    It "Resets Storage" {
        Reset-GEStorage # does not remove assemblies which were previously loaded. this leads to issues loading different asseblis holding the same TSL
    }
    It "Sets the configuration" {
        Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
    }
    It "Compiles and loads the TSL file" {
        Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest
    }
    It "Receives names of the type" {
        Get-GETypeName
    }
    It "Generates and adds a generic cell" {
        $cell = [tsltest.VNET]::new()
        Add-GEVertex -Vertex $cell
    }
    It "Reads the cell" {
        $cell = [tsltest.VNET]::new()
        Add-GEVertex -Vertex $cell
        $r1 = Get-GEVertex -NodeID $cell.CellId
    }
    #this one stucks for some reason!!
    It "Query the Storage for Nodes by TypeName" {
        $res = Get-GEVertex -TypeName "VNET"
        $res | Should -Not -BeNullOrEmpty
    }
    It "Adds a link to a cell" {             
        $cellFrom = [tsltest.VNET]::new()
        $cellTo = [tsltest.VNET]::new()

        $cellFrom | Should -Not -BeNullOrEmpty
        $cellTo | Should -Not -BeNullOrEmpty

        Add-GEVertex -Vertex $cellFrom
        Add-GEVertex -Vertex $cellTo

        Add-GEEdge -From $cellFrom -To $cellTo

        $res = Get-GEVertex -NodeID $cellFrom.CellId

        $res.OutEdge | Should -Not -BeNullOrEmpty
    }

}