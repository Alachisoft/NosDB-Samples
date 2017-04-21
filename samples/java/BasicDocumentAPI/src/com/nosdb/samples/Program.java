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
import com.nosdb.common.DocumentKey;
import com.nosdb.common.server.engine.FailedDocument;
import com.nosdb.common.server.engine.ReadPreference;
import com.nosdb.common.server.engine.WriteConcern;
import com.nosdb.entityobject.Category;
import com.nosdb.entityobject.EmbeddedCategory;
import com.nosdb.entityobject.Product;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Program {

    private static java.util.Collection<DocumentKey> _productKeys;
    private static DBCollection<Product> _productsCollection;
    private static java.util.Collection<Product> _products;

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
            Database database = NosDBClient.getDatabase(connectionString);
            if (database != null) {
                //Get an instance of the 'Products' collection from the database:
                try {
                    _productsCollection = database.getDBCollection("products", Product.class);
                } catch (Exception ex) {
                    Logger.getLogger(Program.class.getName()).log(Level.SEVERE, null, ex);
                }
                if (_productsCollection != null) {
                    // Insert documents into the Products collection:
                    insertDocuments();
                    // Get documents from the Products collection:
                    getDocuments();
                    // Update the UnitsInStock for the documents which need to be replaced.
                    updateDocuments();
                    // Delete documents from the Products collection:
                    deleteDocuments();
                }
            }

        } catch (Exception e) {
            System.out.println(e.toString());
        }
        System.exit(0);

    }

    /**
     * This method inserts documents in the collection
     */
    private static void insertDocuments() throws Exception {
        // Insert documents into the Products collection:
        // This API returns the documents which could not be inserted.
        java.util.ArrayList<com.nosdb.common.server.engine.FailedDocument> failedDocsOnInsert = _productsCollection.insertDocuments(getProduct(), WriteConcern.InMemory);

        if ((failedDocsOnInsert == null || failedDocsOnInsert.isEmpty())) {
            System.out.println("All items inserted into the " + _productsCollection.getName() + " collection successfully. ");
        } else {
            System.out.println("One or more items could not be inserted to the " + _productsCollection.getName() + " collection.\n");
            for (FailedDocument failedDocument : failedDocsOnInsert) {
                printFailedDocumentDetails(failedDocument);
            }
        }
    }

    /**
     * This method fetches documents from the collection
     */
    private static void getDocuments() throws Exception {
        // Get documents from the Products collection:
        // It will return a reader populated with the retrieved items. You may iterate the reader as demonstrated below.
        DBCollectionReader reader = _productsCollection.getDocuments(getProductkeys(), ReadPreference.PrimaryOnly);
        System.out.println("The following items were retrieved from the Products collection:\n");
        while (reader.readNext()) {
            Product product = (Product) reader.getObject();
            printProductDetails(product);
            System.out.println("\n");
        }
    }

    /**
     * This method updates documents in the collection
     */
    private static void updateDocuments() throws Exception {
        // Update the UnitsInStock for the documents which need to be replaced.
        java.util.Iterator<Product> ie = _products.iterator();
        while (ie.hasNext()) {
            Product product = ie.next();
            product.UnitsInStock = (short) (product.UnitsInStock + 100);
        }
        // Replace documents in the Products collection:
        // This API returns the documents which could not be replaced.
        java.util.ArrayList<FailedDocument> failedDocsOnUpdate = _productsCollection.updateDocuments(getProduct(), WriteConcern.InMemory);

        if (failedDocsOnUpdate == null || failedDocsOnUpdate.isEmpty()) {
            System.out.println("All items replaced into the " + _productsCollection.getName() + " collection successfully.");
        } else {
            System.out.println("One or more items could not be replaced to the " + _productsCollection.getName() + "  collection.");
            for (FailedDocument failedDocument : failedDocsOnUpdate) {
                printFailedDocumentDetails(failedDocument);
            }
        }
    }

    /**
     * This method deletes documents from the collection
     */
    private static void deleteDocuments() {
        // Delete documents from the Products collection:
        // This API returns the documents which could not be deleted.
        java.util.ArrayList<FailedDocument> failedDocsOnDelete = _productsCollection.deleteDocuments(getProductkeys(), WriteConcern.InMemory);

        if (failedDocsOnDelete == null || failedDocsOnDelete.isEmpty()) {
            System.out.println("All items delete into the " + _productsCollection.getName() + " collection successfully.");
        } else {
            System.out.println("One or more items could not be delete to the " + _productsCollection.getName() + "  collection.");
            for (FailedDocument failedDocument : failedDocsOnDelete) {
                printFailedDocumentDetails(failedDocument);
            }
        }
    }

    /**
     * This method generates productkeys for this sample
     *
     * @return collection of product keys
     */
    private static java.util.Collection<DocumentKey> getProductkeys() {
        if (_productKeys != null) {
            return _productKeys;
        } else {
            _productKeys = new java.util.ArrayList<>();
            _productKeys.add(new DocumentKey("1001"));
            _productKeys.add(new DocumentKey("1002"));
            _productKeys.add(new DocumentKey("1003"));
            _productKeys.add(new DocumentKey("1004"));
            _productKeys.add(new DocumentKey("1005"));
            return _productKeys;
        }
    }

    /**
     * This method generates products for this sample
     *
     * @return collection of products
     */
    private static java.util.Collection<Product> getProduct() {
        if (_products != null) {
            return _products;
        } else {

            _products = new java.util.ArrayList<>();

            Product product1 = new Product();
            product1.ProductID = "1001";
            product1.ProductName = "Blue Marble Jack Cheese";
            product1.UnitPrice = 400;

            EmbeddedCategory category1 = new EmbeddedCategory();
            category1.CategoryID = 11;
            category1.CategoryName = "Milk Products";
            product1.Category = category1;

            _products.add(product1);

            Product product2 = new Product();
            product2.ProductID = "1002";
            product2.ProductName = "Peanut Biscuits";
            product2.UnitPrice = 500;

            EmbeddedCategory category2 = new EmbeddedCategory();
            category2.CategoryID = 12;
            category2.CategoryName = "Snacks";
            product2.Category = category2;

            _products.add(product2);

            Product product3 = new Product();
            product3.ProductID = "1003";
            product3.ProductName = "Walmart Delicious Cream";
            product3.UnitPrice = 300;
            product3.Category = category1;

            _products.add(product3);

            Product product4 = new Product();
            product4.ProductID = "4";
            product4.ProductName = "Nestle Yogurt";
            product4.UnitPrice = 400;
            product4.Category = category1;

            _products.add(product4);

            Product product5 = new Product();
            product5.ProductID = "1005";
            product5.ProductName = "American Butter";
            product5.UnitPrice = 600;
            product5.Category = category1;
            _products.add(product5);
            return _products;
        }
    }

    /**
     * A method for printing the details of the products type.
     *
     * @param product
     */
    private static void printProductDetails(Product product) {
        if (product == null) {
            return;
        }
        System.out.println("ProductId : " + product.ProductID);
        System.out.println("ProductName : " + product.ProductName);
        System.out.println("UnitPrice : " + product.UnitPrice);
    }

    /**
     * A method for printing all the failed documents and the reason for their
     * failure.
     *
     * @param failedDocument
     */
    private static void printFailedDocumentDetails(FailedDocument failedDocument) {
        if (failedDocument == null) {
            return;
        }
        System.out.println("Document Key : " + failedDocument.getDocumentKey());
        System.out.println("Error Code : " + failedDocument.getErrorCode());
        System.out.println("Error Message : " + failedDocument.getErrorMessage());
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
