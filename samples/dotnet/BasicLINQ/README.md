# Basic LINQ

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

NosDB allows using LINQ with the underlying database to improve the application’s performance without changing the LINQ object model. NosDB's LINQ provider converts LINQ related query into NosDB’s DQL format and returns the result accordingly after transforming it in LINQ format. 

This sample program demonstrates the usage of LINQ with NosDB. It inserts, updates, deletes and queries the documents from the NosDB using LINQ API with IQueryable instance.

This sample uses EntityObjects project as a reference for model class "Product".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported into the collections as explained in **[InstallDir]\samples\data\json\README&#46;md**.
- App.config have been changed according to the configurations. 
	- Change database name in connection string.(optional)
	- Change collection name.(optional)
	- Change configuration server port if required.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = 9047b0f0`

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

© Copyright 2017 Alachisoft. 