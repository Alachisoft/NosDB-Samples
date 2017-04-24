# REST .NET

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

NosDB supports REST for network-based applications which are platform and language independent. This allows client-server communication based on the HTTP protocol, where the standard GET, POST, PUT and DELETE requests perform CRUD operations on collection level and queries.

This sample application demonstrates the usage of REST API capabilities of NosDB and performs CRUD operations on NosDB through it. 

This sample uses EntityObjects project as a reference for model class "Customer".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](../../archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- The NosDB REST API Package is deployed in IIS Server.
- The HTTP Request Uri in the app.config of the sample have been changed according to the connection string in web.config of REST API Package.

### Build and run the sample

- Add a reference of **Newtonsoft.Json 7.0.0** to the sample project.
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