[CmdletBinding(SupportsShouldProcess=$true)]
Param()
#----------------------------------------------------------------------------------------------------
$pkHashtable = @{}   # empty hashtable
$pkHashtable = @{
categories = '"PrimaryKey" : [{"Field":"CategoryID"}]'; 
customers='"PrimaryKey" : [{"Field":"CustomerID"}]';
employees=' "PrimaryKey" : [{"Field":"EmployeeID"}]';
orders= '"PrimaryKey" : [{"Field":"OrderID"}]';
'order-details'= '"PrimaryKey":[{"Field":"OrderID"}, {"Field":"ProductID"}]';
regions= '"PrimaryKey":[{"Field":"RegionID"}]';
territories= '"PrimaryKey":[{"Field":"TerritoryID"}, {"Field":"RegionID"}]';
employeeterritories= '"PrimaryKey":[{"Field":"EmployeeID"}, {"Field":"TerritoryID"}]';
products='"PrimaryKey" : [{"Field":"ProductID"}]';
shippers='"PrimaryKey" : [{"Field":"ShipperID"}]';
suppliers='"PrimaryKey" : [{"Field":"SupplierID"}]';
}

$standalone = $TRUE
$server = ''
$port = 9950
$username = ''
$password = ''

if($standalone)
{
    if(($username.Length -gt 0) -and ($password.Length -gt 0))
	{
        Connect-DatabaseCluster -Server $server -Port $port -Username $username -Password $password -StandAlone 
    }
    else 
	{
        Connect-DatabaseCluster -Server $server -Port $port -StandAlone 
    }
}
else
{
    if(($username.Length -gt 0) -and ($password.Length -gt 0))
	{
        Connect-DatabaseCluster -Server $server -Port $port -Username $username -Password $password
    }
    else 
	{
        Connect-DatabaseCluster -Server $server -Port $port 
    }
}

$paths =  $(Get-Location).Path.Split('\')
$condition  = ($paths[1] -ne '');
$conditionConnected = $paths[0] -eq 'NosDB:'

Write-Host "";
if($condition -and $conditionConnected)
{
	$clusterName = $paths[1] ;
	cd \;
	cd $clusterName;
	cd 'databases';
}
else
{
	Write-Host "Please connect with a database cluster to execute this script."
}

if($(Get-Location).Path.EndsWith('databases'))
{
    #----------------------------- Create Northwind Database Script ---------------------------------
    $scriptPath = split-path -parent $MyInvocation.MyCommand.Definition;
    $FolderName = Get-ChildItem -Path $scriptPath |?{ $_.PSIsContainer };
    $databseFolderName = $FolderName.Name;
    $Format = 'JSON';
    #------------------------------------------------------------------------------------------------
    $DatabaseContext = $(get-location).Path;
    $DatabaseName = $databseFolderName;#use folder name as database name or custom name
	#$DatabaseName ='SampleDatabase';
	
    cd -Path $DatabaseContext
    #---------------- Script for creating database, must bhe connected to database cluster ----------
    Write-Host "Creating database '$DatabaseName' ..."; 
    $query = ('CREATE DATABASE "' + $DatabaseName + '" ');
	Write-Verbose "Query >> $query";
	
    Invoke-SQL -Query $query
    Start-Sleep -Seconds 3;
    $junk = dir;

	Write-Host "";
    #------------------------------------------------------------------------------------------------
    cd ./$DatabaseName ; 
    $FilesName = @(Get-ChildItem -Path $scriptPath/$databseFolderName -Filter *.$Format |% {$_.BaseName});

    #---------------- Loop for creating collections, Database must exist before this loop -----------
    foreach($collection in $FilesName)
    {
        $query2 = ('CREATE COLLECTION "' + $collection + '" {"Database":"' + $DatabaseName +'",'+ $pkHashtable[$collection]+'}');
        Write-Output "CREATING COLLECTION: $collection ...";
        Write-Verbose "Query >> $query2";
		
        Invoke-SQL -Query $query2		
    }

	Write-Host "";
    #------------------------------------------------------------------------------------------------
    cd $DatabaseContext\$DatabaseName'\collections\';
    $junk = dir;

    #---------- Loop for importing data in collections, collections must exist before this loop -----
    foreach($collection in $FilesName)
    {
        cd $DatabaseContext\$DatabaseName'\collections\'$collection;
        $fullPath = "$scriptPath\$databseFolderName\$collection.$Format";
	    Write-Host "Importing data into collection '$collection' ...";        
		Write-Verbose $(get-location).Path;
        Import-Data -Format $Format -CollectionName $collection -Path $fullPath; 
    }
    foreach($chunk in $paths)
    {
        if($chunk -eq 'NosDB:')
        {
            cd \;
        }
        else
        {
            cd $chunk;
        }
    }
#----------------------------------------------------------------------------------------------------
}
else
{
    Write-Host "Change context to NosDb:\Cluster\Database> to execute the script"
}
Write-Host "";