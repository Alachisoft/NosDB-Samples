# SQL Dependency

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

This sample program demonstrates the usage of SQL dependency in NosDB. The feature allows the user to be notified if any change occurs in any of the document that falls in the criteria of a specified SQL query.
The features requires a callback that is invoked if a change is detected. 

This sample inserts a document, and registers SQL dependency for that specific document. Then it updates the document by UPDATE query, that should trigger the SQL Notification and calls the specified callback.

This sample uses EntityObjects project as a reference for model class "Product".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[BaseDir]\data\json\README&#46;md**.	
- App.config have been changed according to the configurations. 
	- Change database name in connection string.(optional)
	- Change collection name.(optional)
	- Change configuration server port if required.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = 9047b0f0`
- Build and run the smaple

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