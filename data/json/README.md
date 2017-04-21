# Importing sample Northwind data into NosDB
	
### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#database-name-options)
* [Running the Northwind.ps1 script](#Running-the-Northwind.ps1-script)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

This module contains sample data in JSON format which is supported by NosDB. 
This readme explains how the provided sample data ([InstallDir]\NoSDB\samples\data\json\northwind) can be imported into your configured database to get you started with NosDB features.

The provided "NorthWind.ps1" script contains all steps - creating a database, collections and importing the sample northwind data into them. However, the script file should be tweaked and executed according to your requirements as explained in this document:

#### Database name options

Provide the value for the 'DatabasesName' variable for the database to be created.

The database can either be created by the filename of the data folder (northwind) or you can provide a custom unqiue name as well.

#### Using Default Folder Name

If you wish to create the database with the default folder name (northwind), make sure that there is no database existing by the same name in the cluster.

#### Providing Custom Name 

If you wish to provide a custom name, uncomment following line in the script to modify it according to your requirement:
	`#$DatabasesName ='CustomName';`

NOTE: Make sure you are connected to the database cluster at this stage. 

### What does the Northwind.ps1 script do? 

Creating collections and importing data into them does not require any user intervention after providing the database name. The script proceeds to perform the following operations once the changes have been made to NorthWind.ps1:

1. Create the database of the specified name in the cluster.
2. Get filenames from the respective folder in "[InstlalDir]\NoSDB\samples\data\json\Northwind\" which contains the input JSON files.
3. Create collections against each filename.
4. Import the data from the files into the corresponding collections.

### Running the Northwind.ps1 script

Once the script file has been edited according to your requirements:

1. Make sure you are connected to the cluster and within the context "NosDB:\$cluster\>".
2. Type the following command in the context:
   `& "[InstallDir]\NoSDB\samples\data\json\Northwind.ps1"`  (INCLUDING the & sign)

The data is imported into the collections which can be verified by querying or API calls. Please refer to the Programmers' Guide or Administrators' Guide in online documentation for more detail.

### Additional Resources

##### Documentation
The complete online documentation for NosDB is available at:
http://www.alachisoft.com/resources/docs/#nosdb

##### Programmers' Guide
The complete programmers guide of NosDB is available at:
http://www.alachisoft.com/resources/docs/nosdb/nosdb-programmers-guide.pdf

### Technical Support

Alachisoft © provides various sources of technical support. 

- Please refer to http://www.alachisoft.com/support.html to select a support resource you find suitable for your issue.
- To request additional features in the future, or if you notice any discrepancy
   regarding this document, please drop an email to [support@alachisoft.com](mailto:support@alachisoft.com).

### Copyrights

© Copyright 2017 Alachisoft 