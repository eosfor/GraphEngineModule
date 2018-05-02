<#
import-module "C:\Program Files\WindowsPowerShell\Modules\pester"
invoke-pester ..\..\..\tests

#>

Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest

$allVnets =  Import-Clixml -Path "C:\tests\objects\vnets.xml"
$subs = Import-Clixml "C:\tests\objects\subs.xml"
$allGWs =  Import-Clixml -Path "C:\tests\objects\gws.xml"
$allConections =  Import-Clixml -Path "C:\tests\objects\connections.xml"
$allCircuits =  Import-Clixml -Path "C:\tests\objects\circuits.xml"
$classicVnets = Import-Clixml "C:\tests\objects\classicNets.xml"


foreach ($vnet in $allVnets) {
	$vnetHash = @{
		SubscriptionId = $vnet.SubscriptionId
		Name  =  $vnet.Name
		ResourceType = $vnet.ResourceType
		Location = $vnet.Location
		AddressSpace = $vnet.Properties.addressSpace.addressPrefixes
		DHCPOptions = [string]$vnet.Properties.dhcpOptions.dnsServers
		PeeredNetwork = $vnet.Properties.virtualNetworkPeerings.properties.remoteVirtualNetwork.id
		ResourceID = $vnet.ResourceId
		CellId = Get-GEStringHash -String ($vnet.ResourceId)
	}
	#[tsltest.VNET]$vnetHash
	$cell = [tsltest.VNET]$vnetHash
    Add-GEVertex -Vertex $cell
}

foreach ($s  in $subs){
	$subHash =  @{
		SubscriptionID = $s.SubscriptionId
		SubscriptionName = $s.SubscriptionName
		CellId = Get-GEStringHash -String ($s.SubscriptionId)
	}

    Add-Vertex -Vertex ([tsltest.Subscription]$subHash)
}

$vertexes = Get-GEVertex -ParameterName SubscriptionID -ParameterValue ($subs[10].SubscriptionId)
$s = $vertexes | where {$_ -is [tsltest.Subscription]}
$v = $vertexes | where {$_ -is [tsltest.VNET]}

$v | % {Add-GEEdge -from $s -to $_}

Get-GEVertex -StartFromID $s.CellId