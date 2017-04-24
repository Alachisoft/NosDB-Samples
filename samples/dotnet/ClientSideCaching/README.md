# Client Side Caching

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

**NCache** is a flexible and feature rich .NET caching solution that provides high performance and scalability to handle any transaction load. NosDB supports integration of NCache into client applications where frequently used data is cached along with being persisted to the file storage. 

This sample program demonstrates how to cache the documents and the result of a NosDB query in an external cache using a cache provider. It performs insert and fetch operation on NosDB, fetch will be entertained from the cache because it was cached on insert. Further it runs a SELECT query with caching options, which is cached and ready to be served when same query runs again.
The default and recommended cache provider is **NCache**. It can be plugged into the application with changes to app.config/web.config.

This sample uses EntityObjects project as a reference for model class "Product".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**.
- App.config have been changed according to the configurations. 
	- Change database name in connection string.(optional)
	- Change collection name.(optional)
	- Change configuration server port if required.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
	- Modify the cache configuration and provide the cache name in '<cacheconfig>' tag
`Integrated Security = false; User ID = admin; Password = 9047b0f0`
- **NCache client for NosDB** must be installed on the machine. Free download [Here](http://www.alachisoft.com/download-nosdb.html).
- Cache specified in App.config should be running on the machine. 

### Build and run the sample

- If using windows authentication then the NosDB service and the NCache service must run under same credentials. Or you can use NosDB authentication.

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