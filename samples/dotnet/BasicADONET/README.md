# Basic ADO.NET

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and Run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

ADO.NET is a framework to separate the data access layer from the logical layer and making an application tiered. NosDB’s provider for ADO.NET can be used to establish connections with NosDB and perform querying over the data.

This sample program that demonstrates how to access NosDB through ADO.NET Provider. 
This program shows how to plug in the NosDB's ADO.NET provider in your application. It shows how to use INSERT, UPDATE, and DELETE statements using ADO.NET ExecuteNonQuery API which in turn communicates with NosDB.

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**.
- app.config have been changed according to the configurations. 
	- change the database name(optional)
	- change the configuration server port.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = 9047b0f0`
- Verify that app.config contains the configuration for plugging in the NosDB's ADO.NET provider.

    
    <system.data>
        <DbProviderFactories>
            <add invariant="NosDB.ADO.NETProvider" name="NosDB ADO.NET Provider" description="ADO.NET Provider for NosDB NosQL Database" type="NosDB.ADO.NETProvider.NosProviderFactory, NosDB.ADO.NETProvider, Version=2.0.0.0, Culture=neutral, PublicKeyToken=8a1e00327893b9ef"/>
        </DbProviderFactories>
    </system.data>


### Build and Run the Sample
    
- Build the sample application so that **NosDB.ADO.NETProvider.dll** is copied to the (Debug)directory of sample application. If it is not copied automatically then copy it manually from "[InstallDir]\integrations\ADO.NET Data Provider\".
- Run the sample application.

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