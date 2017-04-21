# CLR Functions

### Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Sample CLR Function creation](#sample-CLR-Function-creation)
* [Build and run the sample](#Build-and-run-the-sample)
* [Additional Resources](#additional-resources)
* [Technical Support](#technical-support)
* [Copyrights](#copyrights)

### Introduction

NosDB provides users with the flexibility to provide their own custom logic to perform calculations which may be beyond the scope of built-in functions. These user-defined functions (UDFs) are database-level routines which can be used in SELECT queries just like built-in functions for projection and comparison.

A sample program that demonstrates the usage of CLR Functions. The sample CLR Functions used are defined in **NorthwindFunctions** and **EmployeeInfoProvider** class in CLRFunctions project. 
	
### Prerequisites

Before you can use a CLR UDF in NosDB DQL (Document Query Language), make sure:

- Northwind sample data has been imported as explained in **[InstallDir]\samples\data\json\README.md**.
- App.config have been changed according to the configurations. 
	- Change database name in connection string (optional).
	- Change configuration server port if required(optional).
	- To use NosDB Security, set Integrated Security to `false` and specify User Id and Password (optional)
`Integrated Security = false; User ID = admin; Password = 9047b0f0`

### Sample CLR Function creation

- Using NosDB Management Studio, open Northwind database.
- Open "New Function" wizard by right-clicking "CLR Functions" tree item under the Northwind node.
- Follow the wizard and add the "CalculateAmount", "GetFullName" and "GetFullAddress" CLR Functions defind in CLRFunctions project.
- After the successful addition of the CLR function to Northwind database, it can be used in NosDB DQL (Document Query Language),
i.e.,
	>` SELECT CalculateAmount(OrderDetails[0].UnitPrice, OrderDetails[0].Quantity,OrderDetails[0].Discount) FROM orders `
- CLR Functions could also be added to a database by the means of NosDB DQL, for details, please see NosDB Documentation.

### Build and run the sample

- Select **CLRFunctionsUsage** as the main project.
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

© Copyright 2017 Alachisoft 