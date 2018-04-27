Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage"
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest
Get-GETypeName

$cell = [tsltest.VNET]::new()
Add-GEVertex -Vertex $cell

Save-GeStorage