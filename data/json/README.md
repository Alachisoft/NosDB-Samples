# Importing sample Northwind data into NosDB
	
### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#database-name-options)
* [Running the Northwind.ps1 script](#running-the-northwind.ps1-script)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

All NosDB samples use 'Northwind' database for CRUD operations. This folder contains northwind database scripts for NosDB in flexible JSON format. A PowerShell script "Northwind.ps1" is also included which creates the database and imports data into its collections from the JSON files included in the folder (`northwind`).

However, the script file can be tweaked and executed according to your requirements as explained in this document:

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

#### Server name option

Provide the IPv4 address or *server-name* in the variable 'server', of the server on which the *database cluster* exists and *configuration server* is running.

Empty value will lead it to connect to the server on which the script is running.

#### StandAlone switch option

Set the 'standalone' variable to true if you intend to connect to a standalone instance of the database cluster.

Unspecified switch will lead it to connect to an instance of a sharded database cluster.

#### Security mode option

Specify 'username' and 'password' in their variables, if the database cluster is using *NosDB Authentication*

Upspecified values will lead it to connect to the database cluster relying on *Windows Authentication*

#### Port value option

Specify the port value on which the *configuration server* is entertaining requests to the 'port' variable. The default value is *9950*.

#### Database name option

Provide the value for the 'DatabaseName' variable for the database to be created.

The database can either be created by the filename of the data folder "northwind" or you can provide a custom unqiue name as well.

#### Using Default Folder Name

If you wish to create the database with the default folder name "northwind", make sure that there is no database existing by the same name in the cluster.

#### Providing Custom Name 

If you wish to provide a custom name, uncomment following line in the script to modify it according to your requirement:
	`#$DatabaseName ='SampleDatabase';`

> **NOTE:** Please refer to the *"Configuring Database Clusters and Shards"* section of *NosDB Administrator's Guide (Power Shell)* for details on *creating* and *connecting* to a database cluster.  

### What does the Northwind.ps1 script do? 

Creating collections and importing data into them does not require any user intervention after providing the database name. The script proceeds to perform the following operations once the changes have been made to NorthWind.ps1:

1. Create the database of the specified name in the cluster.
2. Get filenames from the respective folder in "northwind" which contains the input JSON files.
3. Create collections against each filename.
4. Import the data from the files into the corresponding collections.

### Running the Northwind.ps1 script

Once the script file has been edited according to your requirements:

1. Make sure you are connected to the cluster and within the context "NosDB:\$cluster\>".
2. Type the following command in the context:
   `& "[BaseDir]\data\json\Northwind.ps1"` (Absolute path)

The data is imported into the collections which can be verified by querying or API calls. Please refer to the Programmer's Guide or Administrator's Guide in online documentation for more detail.

### Additional Resources

##### Documentation
The complete online documentation for NosDB is available at:
http://www.alachisoft.com/resources/docs/#nosdb

##### Programmers' Guide
The complete programmers guide of NosDB is available at:
http://www.alachisoft.com/resources/docs/nosdb/nosdb-programmers-guide.pdf

##### Administrator's Guide (Power Shell)
The complete administrator guide of NosDB is available at:
http://www.alachisoft.com/resources/docs/nosdb/nosdb-admin-guide-powershell.pdf

### Technical Support

Alachisoft © provides various sources of technical support. 

- Please refer to http://www.alachisoft.com/support.html to select a support resource you find suitable for your issue.
- To request additional features in the future, or if you notice any discrepancy
   regarding this document, please drop an email to [support@alachisoft.com](mailto:support@alachisoft.com).

### Copyrights

© Copyright 2017 Alachisoft. 