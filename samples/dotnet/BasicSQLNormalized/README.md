# Basic SQL Normalized

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

A sample program that demonstrates how to perform CRUD operations on a NosDB collection through queries. This sample also utilizes the NosDB normalized data model.

This sample program demonstrates how to perform CRUD operations on a NosDB collection through queries. It utilizes the NosDB embedded data model. By using SQL INSERT, UPDATE and DELETE queries it shows how to perform Write Operation on NosDB and SQL SELECT to fetch the reader from NosDB.

This sample uses EntityObjects project as a reference for model class "Product".

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](../../archive/master.zip).

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README&#46;md**.
- app.config have been changed according to the configurations. 
    - change the database name (optional)
    - change the configuration server port.(optional)
    - To use NosDB Security, set Integrated Security to false and specify User Id and Password (optional)
    `Integrated Security = false; User ID = admin; Password = 9047b0f0`
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