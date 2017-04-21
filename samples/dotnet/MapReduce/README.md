# MapReduce

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Build and run the sample](#Build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

MapReduce is a task which splits the input data-set into independent chunks which are processed by the map tasks in a completely parallel manner. The framework sorts the outputs of the maps, which are then input to the reduce tasks.

This sample program demonstrates how to use MapReduce with NosDB.
This sample consists of two projects, 
- MapReduceImplementation which provides the implementation for the phases of a MapReduce task.
- MapReduce sample application which demonstrates the MapReduce API to execute a MapReduce task.

### Prerequisites

Before the sample application is executed make sure that:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README.md**.
- App.config have been changed according to the configurations. 
	- Change database name in connection string.(optional)
	- Change collection name.(optional)
	- Change configuration server port if required.(optional)
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = 9047b0f0`

### Build and run the sample

- Build the MapReduceImplementation project.
- Deploy the built "NosDB.MapReduceImplementation" assembly to the NosDB using NosDB Management studio.
	- Right click on the database name and find "Deploy Provider" option.
	- Specify the same deployment name which was specified while deploying the assembly from NosDB Management studio. (Sample name specified is "mapreduceimpl")
	- Browse for the folder and specify the assembly and click OK to make deployment.
- Run the MapReduce sample application.

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

© Copyright 2017 Alachisoft 