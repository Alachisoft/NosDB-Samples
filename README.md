# NosDB sample applications

### Table of contents

* [Introduction](#introduction)
* [About NosDB](#about-nosDB)
* [Using the samples](#using-the-samples)
* [Samples by category](#samples-by-category)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

This repository consists on the source code of sample applications that illusturate the NosDB 2.0 API usage in different langauges and runtimes. The code samples were created in Visual Studio and NetBeans.

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### About NosDB

Welcome to NosDB! NosDB is a schema-less and scalable NOSQL database solution to handle ad-hoc querying on huge amounts of real-time, unstructured data. As NosDB scales out to accommodate the rapidly increasing volume of your data, it applies robust data distribution strategies to ensure availability and fault tolerance at all times. Keeping in mind the suitability of NosDB for Big Data applications, MapReduce and Aggregation support has also been introduced to dramatically enhance performance due to parallel processing.

NosDB features and tools are designed to be tuned flexibly into applications of any size – from small to enterprise-wide global installations.


### Using the samples

The easiest way to use these samples without using Git is to download the zip file containing the current version (using the following link or by clicking the "Download ZIP" button on the repository page). You can then unzip the entire archive and use the samples in Visual Studio 2015/2017.

[Download the samples ZIP](../../archive/master.zip)
    
 >   **Note:**  The .NET samples are made using Visual Studio 2015. 
 
All the sample applications except [*EF Music Store*](samples/dotnet/EFMusicStore) use 'Northwind' database for various operations. A PowerShell script "Northwind.ps1" is included in the ['data/json'](data/json) directory which creates the database and imports data into its collections.

### Samples by category
##### CRUD (Create, Read, Update and Delete)

Basic Document API ([.NET](samples/dotnet/BasicDocumentAPI) \| [JAVA](samples/java/BasicDocumentAPI)  \| [Node.js](samples/nodejs/BasicDocumentAPI))|  Basic Document API Async ([.NET](samples/dotnet/BasicDocumentAPIAsync)) | Attachments ([.NET](samples/dotnet/Attachments) \| [Node.js](samples/nodejs/Attachments))| 
|---|---|---|


##### Queries

|Basic SQL Normalized ([.NET](samples/dotnet/BasicSQLNormalized) \| [Java](samples/java/BasicSQLNormalized) \| [Node.js](samples/nodejs/BasicSQLNormalized)) |Basic SQL Embedded ([.NET](samples/dotnet/BasicSQLEmbedded) \| [Java](samples/java/BasicSQLEmbedded) \| [Node.js](samples/nodejs/BasicSQLEmbedded)) |Joins Nested Queries ([.NET](samples/dotnet/JoinsNestedQueries)) |
|---|---|---|

|Basic LINQ ([.NET](samples/dotnet/BasicLINQ)) | 
|---|


##### Entity Framework

| EF Music Store ([.NET](samples/dotnet/EFMusicStore))| EF Northwind ([.NET](samples/dotnet/EFNorthwind)) |
|---|---|


##### REST

|REST ([.NET](samples/dotnet/RESTDotNet) \| [Python](samples/dotnet/RESTPython))|
|---|


##### Miscellaneous
|BasicADO ([.NET](samples/dotnet/BasicADONET))|Client Side Caching ([.NET](samples/dotnet/ClientSideCaching))|CLR Functions ([.NET](samples/dotnet/CLRFunctions))|
|---|---|---|

|CLR Triggers ([.NET](samples/dotnet/CLRTriggers))|Entity Objects ([.NET](samples/dotnet/EntityObjects))|MapReduce ([.NET](samples/dotnet/MapReduce))|
|---|---|---|

|SQL Dependency ([.NET](samples/dotnet/SQLDependency))|Domain Object ([Java](samples/java/DomainObject))|
|---|---|

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