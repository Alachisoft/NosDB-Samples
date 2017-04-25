# Attachments

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

Attachments are used for storing binary data (BLOBs) such as images, audio, video etc.

NosDB allows you to insert, fetch, replace and delete attachments, along with querying over the collection to retrieve specific attachments. NosDB uses streams for the purpose of attachments.
This sample program demonstrates how to use NosDB attachment API to Insert, Get, Update and Delete attachments in NosDB. This sample inserts an employee in NosDB, and inserts a picture regarding the employee as attachment in NosDB. Then fetches the employee from NosDB and sets the corresponding picture by fetching the attachment from NosDB attachment store.

It also demonstrates how to search through the attachments in NosDB, additional meta-data is used for this purpose.

This sample uses EntityObjects project as a reference for model class "Employee".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:
	
- Northwind sample data has been imported as explained in **[BaseDir]\data\json\README&#46;md**.
- app.config have been changed according to the configurations. 
    - change the database name (optional)
    - change the configuration server port(optional).
    - To use NosDB Security, set Integrated Security to false and specify User Id and Password(optional) `Integrated Security = false; User ID = admin; Password = ********`
- Enable attachments on the database from the NosDB Management Studio.
	- Browse database properties by right clicking the database name.
	- In Attachments section, check "Enable Attachments" and provide a path to which attachments will be stored.
	- Click "OK" to apply the settings.
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