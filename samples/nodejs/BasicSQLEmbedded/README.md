# Basic SQL Embedded

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

Embedded data model allows application to store related pieces of information in the same record, so that applications may need fewer queries to complete operations.
NosDB fully supports the embedded data model.

This sample program demonstrates how to perform CRUD operations on a NosDB collection through queries. It utilizes the NosDB embedded data model. By using SQL INSERT, UPDATE and DELETE queries it shows how to perform Write Operation on NosDB and SQL SELECT to fetch the reader from NosDB.

This sample uses EntityObjects project as a reference for model class "Product".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**.
- Connection string in configuration.js is correct. For example, you need to provide correct database name and password.
- NosDB Distributor Service (NosDistributorSvc)is running

### Build and run the sample

Before starting the sample follow these steps:
1.  Install node.
2.  Install npm.
3. 	Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**.
4.  Make sure that all NosDB services are running including Distributor Service (NosDistributorSvc).
5.  Use "npm install" command to install dependencies for Node.js sample. Make sure that path of dependencies is correct in 'package.json'.
	
You are ready to start the sample application.

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
- To request additional features in the future, or if you notice any discrepancy regarding this document, please drop an email to [support@alachisoft.com](mailto:support@alachisoft.com).

### Copyrights

© Copyright 2017 Alachisoft. 