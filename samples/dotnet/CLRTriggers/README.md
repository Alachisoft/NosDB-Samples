# CLR Triggers

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Sample CLR Trigger creation](#sample-clr-trigger-creation)
* [Build and run the sample](#build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

CLR Triggers are functions registered against a database operation and are executed in response as soon as the operation is performed, hence “triggering” the function. NosDB supports collection-level triggers to perform client-defined actions prior to or after an operation has been performed on the collection.

This sample shows how to create and use a trigger. This sample implements NOT NULL constraint for Product's UnitPrice Attribute.

> **Note:** If you are unfamiliar with Git and GitHub, you can download the entire collection as a 
> [ZIP file](https://github.com/Alachisoft/NosDB-Samples/archive/master.zip).

## Prerequisites

Before the sample CLR Trigger is affective, make sure:

- Northwind sample data has been imported as explained in **[BaseDir]\data\json\README&#46;md**.

### Sample CLR Trigger creation

- Using NosDB Management Studio, open Northwind database.
- Open "New Trigger" wizard by right-clicking "CLR Triggers" tree item under the "Products" collection.
- Follow the wizard and add the "PreInsert" and "PreUpdate" CLR Trigger defined in the CLRTriggers assembly.
- After the successful addition of the CLR trigger to Products collection, it will be executed on pre-insert and pre-update events of Products collection.
- CLR Triggers can also be added to a database by the means of NosDB DQL, for details, please see NosDB Documentation.

### Build and run the sample

- Select **CLRTriggersUsage** as main project.
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