<#
import-module "C:\Program Files\WindowsPowerShell\Modules\pester"
invoke-pester ..\..\..\tests
#>

<#
#function Get-IDHash {
#	[cmdletbinding()]
#	param(
#	$id
#	)
#	process {
#		$ret = [System.Collections.Generic.List[long]]::new()
#		foreach ($el in $id) {
#			if (! [string]::IsNullOrEmpty($el)) {
#				$ret.Add((Get-GEStringHash -String $el))
#			}
#		}

#		$ret
#	}
#}
#>

Set-GEConfigOption -LogDirectory "C:\Tests\psTrinityTests\storage\trinity-log" -StorageRoot "C:\Tests\psTrinityTests\storage" -LogEchoOnConsole $false
Add-GETslData -Path C:\Repo\Github\My\GraphEngineModule\TSL\ -Namespace tsltest

$allVnets =  Import-Clixml -Path "C:\tests\objects\vnets.xml"
$subs = Import-Clixml "C:\tests\objects\subs.xml"
$allGWs =  Import-Clixml -Path "C:\tests\objects\gws.xml"
$allConections =  Import-Clixml -Path "C:\tests\objects\connections.xml"
$allCircuits =  Import-Clixml -Path "C:\tests\objects\circuits.xml"
$classicVnets = Import-Clixml "C:\tests\objects\classicNets.xml"

#region Import Vertices
foreach ($vnet in $allVnets) {
	$vnetHash = @{
		SubscriptionId = Get-GEStringHash -String ($vnet.SubscriptionId)
		Name  =  $vnet.Name
		ResourceType = $vnet.ResourceType
		Location = $vnet.Location
		AddressSpace = $vnet.Properties.addressSpace.addressPrefixes
		DHCPOptions = [string]$vnet.Properties.dhcpOptions.dnsServers
		PeeredNetwork =  Get-GEStringHash -string ($vnet.Properties.virtualNetworkPeerings.properties.remoteVirtualNetwork.id)
		ResourceID = $vnet.ResourceId
		Gateway =  Get-GEStringHash -string (($vnet.Properties.Subnets | ? name -eq GatewaySubnet).Properties.IpConfigurations.id)
		CellId = Get-GEStringHash -string $vnet.ResourceId
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

#add gateways
foreach ($gw in $allGWs) {
	$gwHash = @{
		Name = $gw.name
		ResourceType = $gw.Resourcetype
		SubscriptionID = $gw.subscriptionid
		ResourceID = $gw.resourceid
		CellId = Get-GEStringHash -String ($gw.resourceid)
	}
    Add-GEVertex -Vertex ([tsltest.Gateway]$gwHash)
}

#add connections
foreach ($conn in $allConections) {
	$connHash = @{
		Name = $conn.Name
		ResourceName = $conn.Resourcename
		ResourceType = $conn.ResourceType
		Location = $conn.Location
		SubscriptionID = $conn.SubscriptionID
		ResourceID =  $conn.ResourceID
		CellId = Get-GEStringHash -String ($conn.resourceid)
	}
    $connObject = [tsltest.Connection]$connHash
    Add-GEVertex -Vertex $connObject
}

#add MPLS
foreach ($er in $allCircuits) {
	$erHash = @{
		Name = $er.Name
		ResourceName = $er.Resourcename
		ResourceType = $er.ResourceType
		Location = $conn.Location
		SubscriptionID = $conn.SubscriptionID
		ResourceID = $er.ResourceID
		CellId = Get-GEStringHash -String ($conn.resourceid)

	}
    $circuitObject = [tsltest.Circuit]$erHash
    Add-GEVertex -Vertex $circuitObject
}

#endregion Import vertices

#region Add Edges
#Subscriptions -> VNET
$subVertices = Get-GEVertex -TypeName "Subscription"
$subVertices | % {
	$subID = $_.SubscriptionID
	$sub = $_
	Get-GEVertex -TypeName "VNET" | where {$_.SubscriptionID -eq $subID} | % {
		Add-GEEdge -from $sub -to $_
	}
}

#VNET -> VNET GW
$gwVerices = Get-GEVertex -TypeName "GW"
$gwVerices | % {
	$g = $_
	$v =  Get-GEVertex -TypeName "VNET" | where {$_.GWId -eq $g.}
}

foreach ($v in ($g.Vertices  | ? {$_ -is [Gateway]})){
    $vnetToConnect = $vnetsByGW[$v.Properties.IpConfigurations.ID]
    if ($vnetToConnect){
        $vnetToConnect | % {Add-Edge -From $_ -To $v -Graph $g}
    }
}

#endregion Add Edges


$vertexes = Get-GEVertex -ParameterName SubscriptionID -ParameterValue ($subs[10].SubscriptionId)
$s = $vertexes | where {$_ -is [tsltest.Subscription]}
$v = $vertexes | where {$_ -is [tsltest.VNET]}

$v | % {Add-GEEdge -from $s -to $_}

Get-GEVertex -StartFromID $s.CellId