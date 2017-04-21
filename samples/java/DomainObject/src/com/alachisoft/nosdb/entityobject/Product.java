// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

package com.nosdb.entityobject;

public class Product
{
    public String ProductID;
    public String ProductName;
    public  String QuantityPerUnit;
    public double UnitPrice;
    public short UnitsInStock;
    public short UnitsOnOrder;
    public short ReorderLevel;
    public boolean Discontinued;
    public String SupplierID;
    public EmbeddedCategory Category;
}
