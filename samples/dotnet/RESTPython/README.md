# REST Python

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

NosDB doesn't have a Python client yet, but it will be out soon. But it provides an indirect way to use NosDB using REST API.
This sample communicates with NosDB's REST API to perform CRUD operations on NosDB. It demonstrates the CRUD operation by Inserting, Getting, Updating and Deleting a document from NosDB using standard HTTP methods POST, GET, PUT and DELETE.

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](../../archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- The NosDB REST API Package is deployed in IIS Server.
- The `requests` module should be installed for python before executing the script. Run the following command.
    >`[Python_Path]\Scripts\easy_install.exe requests`
- The `requestUri` in sample application have been changed according to the connection string in web.config of REST API Package.

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