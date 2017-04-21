// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
package com.nosdb.samples;

import com.nosdb.client.*;
import com.nosdb.common.JSONDocument;
import com.nosdb.common.server.engine.*;
import com.nosdb.common.server.engine.impl.ParameterImpl;
import com.nosdb.entityobject.Customer;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Properties;

public class Program {

    private static Database _database;
    private static DBCollection<Customer> _customersCollection;

    public static void main(String[] args) {
        try {
            loadProperties();
            // change connectionstring 
            String connectionString = System.getProperty("connectionstring");
            if (connectionString == null || connectionString.isEmpty()) {
                System.out.println("The connection string cannot be null or empty.");
                return;
            }
            //Initialize an instance of the database to begin performing operations:
            _database = NosDBClient.getDatabase(connectionString);
            if (_database != null) {
                System.out.println("Database: " + _database.getDatabaseName() + " initialize successfully.");
                //Get an instance of the 'Customers' collection from the database:
                _customersCollection = _database.getDBCollection("Customers", Customer.class);
                if (_customersCollection != null) {

                    // Insert documents in the collection using query
                    insertDocuments();

                    // Get documents from the collection using query
                    getDocuments();

                    // Update documents in the collection using query
                    updateDocuments();

                    // Delete documents from the collection using queryF
                    deleteDocuments();
                }
            }
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
            System.exit(0);
        }
    }

    /*
    * This method inserts documents in the collection using query
     */
    private static void insertDocuments() {
        //Insert Customer 'John Doe' into the 'Customers' Collection using Insert Query.
        final String insertQuery = "INSERT INTO Customers (CustomerID, ContactName, Address, City) VALUES(@customerId, @customerName,@customerAddress,@customerCity)";
        java.util.List<ParameterImpl> insertParameters = new java.util.ArrayList<ParameterImpl>();
        insertParameters.add(new ParameterImpl("customerId", "Customer1"));
        insertParameters.add(new ParameterImpl("customerName", "John Doe"));
        insertParameters.add(new ParameterImpl("customerAddress", "Obere Street 57"));
        insertParameters.add(new ParameterImpl("customerCity", "Berlin"));

        //Executing Insert Query
        long affectedrow = _database.executeNonQuery(insertQuery, insertParameters);
        if (affectedrow > 0) {
            System.out.println("All items inserted into the " + _customersCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be inserted to the " + _customersCollection.getName() + " collection.\n");
        }
    }

    /*
    * This method fetches documents from the collection using query
     */
    private static void getDocuments() {
        //Get the Customer 'John Doe' from the 'Customers' Collection using the Select Query
        final String selectQuery = "SELECT CustomerID, ContactName, Address FROM Customers WHERE ContactName = @customerName";
        java.util.List<ParameterImpl> selectParameters = new java.util.ArrayList<ParameterImpl>();
        selectParameters.add(new ParameterImpl("customerName", "John Doe"));

        //Executing Select Query
        DBCollectionReader reader = _database.executeReader(selectQuery, selectParameters, ReadPreference.PrimaryOnly);
        System.out.println("The following items were retrieved from the Customers collection:\n");
        while (reader.readNext()) {

			try
			{
            JSONDocument customer = ((JSONDocument) reader.getObject());
            printDetails(customer.parse(Customer.class));
            System.out.println("\n");
			}
			catch(Exception ex)
			{
				ex.printStackTrace();	
			}
        }

    }

    /*
    * This method updates documents in the collection using query
     */
    private static void updateDocuments() {
        //Update 'John Doe' in the 'Customers' Collection using Update Query.
        final String updateQuery = "UPDATE Customers SET (Address = @customerAddress, City = @customerCity) WHERE ContactName = @customerName";
        java.util.List<ParameterImpl> updateParametes = new java.util.ArrayList<ParameterImpl>();
        updateParametes.add(new ParameterImpl("customerAddress", "120 Hanover Sq."));
        updateParametes.add(new ParameterImpl("customerCity", "London"));
        updateParametes.add(new ParameterImpl("customerName", "John Doe"));

        //Executing Update Query
        long affectedrow = _database.executeNonQuery(updateQuery, updateParametes);
        if (affectedrow > 0) {
            System.out.println("All items updated into the " + _customersCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be updated to the " + _customersCollection.getName() + " collection.\n");
        }
    }

    /*
    * This method deletes documents from the collection using query
     */
    private static void deleteDocuments() {
        //Delete 'John Doe' from the Customers Collection using Delete Query.
        final String deleteQuery = "DELETE FROM Customers WHERE ContactName = @customerName";
        java.util.List<ParameterImpl> deleteParametes = new java.util.ArrayList<ParameterImpl>();
        deleteParametes.add(new ParameterImpl("customerName", "John Doe"));

        //Executing Delete Query
        long affectedrow = _database.executeNonQuery(deleteQuery, deleteParametes);
        if (affectedrow > 0) {
            System.out.println("All items deleted into the " + _customersCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be deleted to the " + _customersCollection.getName() + " collection.\n");
        }
    }

    /**
     * A method for fetching sample properties
     */
    private static void loadProperties() {
        Properties props = new Properties();
        String fileName = new File(System.getProperty("user.dir") + File.separator + "sample.properties").getPath();

        try {
            props.load(new FileInputStream(fileName));
        } catch (IOException iOException) {
            System.out.println(iOException.toString());
        }

        Enumeration enu = props.keys();
        while (enu.hasMoreElements()) {
            String key = (String) enu.nextElement();
            System.setProperty(key, props.getProperty(key).trim());

        }

    }

    /**
     * A method for printing the details of the products type.
     *
     * @param product
     */
    private static void printDetails(Customer customer) {
        if (customer == null) {
            return;
        }
        System.out.println("");
        System.out.println("Customer-Id : " + customer.CustomerID);
        System.out.println("Customer-Name : " + customer.ContactName);
        System.out.println("Address : " + customer.Address);
    }

}
