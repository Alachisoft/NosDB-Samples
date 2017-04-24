# EntityFramework Music Store

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

A sample application for using enityframework with music store database
This sample is a modification of the Microsoft's aspnet MusicStore application

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](../../archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Visual Studio 2015 Tools (Preview 2) is installed. (https://go.microsoft.com/fwlink/?LinkId=827546)

- The nuget package **NosDB.EntityFrameworkCore** -version **1.0.0.0** must be installed to make the solution compilable.	

- config.json have been changed according to the configurations.
	- Change database name in connection string.(optional)
	- Change configuration server port if required.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = ********`
	- The user provided in the connection string must have rights to create databases and collections

- In app.config Set `xmlPath` in `dbFirstSection` under `databaseFirstApproachGroup` to **`.\NosDBMusicStore.xml`**. This xml contains the schema configuration of the MusicStore database. It is created by using code first approach with Enity framework or Get-DatabaseSchema command in powershell, as NosDB is schema-less and Entity framework is not so this configuration bridges this gap between the two and is used by several different operations such as API, Queries etc.

- Note that the sample may take some time on the first run as it has to create databases, collection and indexes.

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
Copyright (c) .NET Foundation. All rights reserved. Licensed under the Apache License, Version 2.0
© Copyright 2017 Alachisoft. 