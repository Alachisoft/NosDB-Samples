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

import com.nosdb.client.DBCollection;
import com.nosdb.client.DBCollectionReader;
import com.nosdb.client.Database;
import com.nosdb.client.NosDBClient;
import com.nosdb.common.JSONDocument;
import com.nosdb.common.server.engine.ReadPreference;
import com.nosdb.common.server.engine.WriteConcern;
import com.nosdb.common.server.engine.impl.ParameterImpl;
import com.nosdb.entityobject.Customer;
import com.nosdb.entityobject.EmbeddedCategory;
import com.nosdb.entityobject.Product;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import java.util.Enumeration;
import java.util.Properties;

public class Program {

    private static Database _database;
    private static DBCollection<Product> _productsCollection;

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
                _productsCollection = _database.getDBCollection("Customers", Product.class);
                if (_productsCollection != null) {

                    // Insert documents in the collection using query
                    insertDocuments();

                    // Fetch documents from the collection using query
                    getDocuments();

                    // Update documents in the collection using query
                    updateDocuments();

                    // Delete documents from the collection using query
                    deleteDocuments();
                }
            }
        } catch (Exception exception) {
            System.out.println(exception.getMessage());
            System.exit(0);
        }
    }

    /**
     * This method inserts documents in the collection using query
     */
    private static void insertDocuments() {
        //Insert Product 'Chai' into the 'Products' Collection using Insert Query.
        final String insertQuery = "INSERT INTO Products (ProductID, ProductName, UnitsInStock, UnitPrice, Category) VALUES(@productId, @productName,@unitsInStock,@unitPrice, @category)";
        java.util.List<ParameterImpl> insertParameters = new java.util.ArrayList<ParameterImpl>();
        insertParameters.add(new ParameterImpl("productId", "Product1"));
        insertParameters.add(new ParameterImpl("productName", "Chai"));
        insertParameters.add(new ParameterImpl("unitsInStock", 39));
        insertParameters.add(new ParameterImpl("unitPrice", 100.0));
        insertParameters.add(new ParameterImpl("category", new EmbeddedCategory(13, "Beverages")));

        //Executing Insert Query
        long affectedrow = _database.executeNonQuery(insertQuery, insertParameters);
        if (affectedrow > 0) {
            System.out.println("All items inserted into the " + _productsCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be inserted to the " + _productsCollection.getName() + " collection.\n");
        }
    }

    /**
     * This method fetches documents from the collection using query
     */
    private static void getDocuments() {//Get the Product 'Chai' from the 'Products' Collection using the Select Query
        final String selectQuery = "SELECT ProductName, UnitsInStock, UnitPrice, Category FROM Products WHERE ProductName = @productName";
        java.util.List<ParameterImpl> selectParameters = new java.util.ArrayList<ParameterImpl>();
        selectParameters.add(new ParameterImpl("productName", "Chai"));

        //Executing Select Query
        DBCollectionReader reader = _database.executeReader(selectQuery, selectParameters, ReadPreference.PrimaryOnly);
        System.out.println("The following items were retrieved from the Products collection:\n");
        while (reader.readNext()) {

			try
			{
            JSONDocument product = (JSONDocument) reader.getObject();
            printDetails(product.parse(Product.class));
            System.out.println("\n");
			}
			catch(Exception ex)
			{
				ex.printStackTrace();
			}
        }
    }

    /**
     * This method updates documents in the collection using query
     */
    private static void updateDocuments() { //Update 'Chai' in the 'Products' Collection using Update Query.
        final String updateQuery = "UPDATE Products SET (UnitsInStock = @unitsInStock, UnitPrice = @unitPrice, Category.CategoryName = @categoryName) WHERE ProductName = @productName";
        java.util.List<ParameterImpl> updateParametes = new java.util.ArrayList<ParameterImpl>();
        updateParametes.add(new ParameterImpl("unitsInStock", 45));
        updateParametes.add(new ParameterImpl("unitPrice", 125.0));
        updateParametes.add(new ParameterImpl("productName", "Chai"));
        updateParametes.add(new ParameterImpl("categoryName", "Food"));

        //Executing Update Query
        long affectedrow = _database.executeNonQuery(updateQuery, updateParametes);
        if (affectedrow > 0) {
            System.out.println("All items updated into the " + _productsCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be updated to the " + _productsCollection.getName() + " collection.\n");
        }
    }

    /**
     * This method deletes documents from the collection using query
     */
    private static void deleteDocuments() {//Delete 'Chai' from the 'Product' Collection using Delete Query.
        final String deleteQuery = "DELETE FROM Products WHERE ProductName = @productName";
        java.util.List<ParameterImpl> deleteParametes = new java.util.ArrayList<ParameterImpl>();
        deleteParametes.add(new ParameterImpl("productName", "Chai"));

        //Executing Delete Query
        long affectedrow = _database.executeNonQuery(deleteQuery, deleteParametes);
        if (affectedrow > 0) {
            System.out.println("All items deleted into the " + _productsCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be deleted to the " + _productsCollection.getName() + " collection.\n");
        }
    }

    /**
     * A method for printing the details of the products type
     *
     * @param product
     */
    private static void printDetails(Product product) {
        if (product == null) {
            return;
        }
        System.out.println("");
        System.out.println("Product-Name : " + product.ProductName);
        System.out.println("Units-In-Stock : " + product.UnitsInStock);
        System.out.println("Unit-Price : " + product.UnitPrice);
        System.out.println("Category :");
        System.out.println("    ID : " + product.Category.CategoryID);
        System.out.println("    Name : " + product.Category.CategoryName);

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

}
