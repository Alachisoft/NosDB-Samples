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

import java.util.ArrayList;
import java.util.Date;

public class Order
{
    public String OrderID;
    public Date OrderDate;
    public Date RequiredDate;
    public Date ShippedDate;
    public double Freight;
    public String ShipName;
    public String ShipAddress;
    public String ShipCity;
    public String ShipRegion;
    public String ShipPostalCode;
    public String ShipCountry;
    public String CustomerID;
    public String EmployeeID;
    public String ShipperID;
    public ArrayList<OrderDetail> OrderDetails;

}
