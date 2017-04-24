# EntityFramework Northwind

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

Entity Framework is a set of technologies in ADO.NET that makes the development of data-oriented software applications easy. NosDB 2.0 have full support for Entity framework Core (version-1.0.0).

This sample application shows how to use EF Core with NosDB. This sample uses Northwind as the database to demonstrate the use of EF.

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](../../archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**
- Nuget package **NosDB.EntityFrameworkCore.Design** -version **1.0.0.0** should be installed on EFNorthwind project to make the solution compilable.

### Build and run the sample

- app.config have been changed according to the configurations. 
    - change the database name (optional)
    - change the configuration server port.(optional)
    - To use NosDB Security, set Integrated Security to false and specify User Id and Password(optional) `Integrated Security = false; User ID = admin; Password = ********`
- Set `xmlPath` in `dbFirstSection` under `databaseFirstApproachGroup` to  **`..\..\NosDBNorthwindSchemaConf.xml`**. This xml contains the schema configuration of the northwind database. It is created by using code first approach with Enity framework or Get-DatabaseSchema command in powershell, as NosDB is schema-less and Entity framework is not so this configuration bridges this gap between the two and is used by several different operations such as API, Queries etc.
- Build and run the sample.

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