// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
var nosDClient = require('nosdb').nosDBClient;
var connStr = require('./configuration').connectionString;
// Get database using the connection string specified in configuration.js
nosDClient.getDatabase(connStr, function (dbErr, db) {
    if (dbErr) {
        console.log(dbErr);
    }
    else {
        // Get instance of products collection
        db.getCollection("Products", function (collErr, productsCollection) {
            if (collErr) {
                console.log(collErr);
            }
            else {
                // Insert product in products collection
                insertProduct(db, function (err, num) {
                    if (err) {
                        console.log(err);
                    }
                    else {
                        console.log("\n'" + num + "' record(s) inserted!");
                    }
                    // Fetch product from products collection
                    getProduct(db, function (err, reader) {
                        if (err) {
                            console.log(err);
                        }
                        else {
                            reader.toArray(function (readerErr, array) {
                                if (readerErr) {
                                    console.log(readerErr);
                                }
                                else if (array[0]) {
                                    var productId = array[0].ProductID;
                                    var companyName = array[0].CompanyName;
                                    var address = array[0].Address;
                                    var city = array[0].City;
                                    console.log("'" + array.length + "' record(s) retrieved");
                                }
                                else {
                                    console.log("No document is retrieved against 'Product1'.");
                                }
                            });
                        }
                        // Update product in products collection
                        updateProduct(db, function (err, num) {
                            if (err) {
                                console.log(err);
                            }
                            else {
                                console.log("'" + num + "' record(s) were updated!");
                            }
                            // Delete product from products collection
                            deleteProduct(db, function (err, num) {
                                if (err) {
                                    console.log(err);
                                }
                                else {
                                    console.log("'" + num + "' record(s) deleted!");
                                }
                                // Release resources
                                db.dispose();
                            });
                        });
                    });
                });
            }
        });
    }
});
/**
 * This method inserts product in the collection
 * @param {dbCollection} coll collection in which product will be inserted
 * @param {callback} callback callback that will be called after the operation
 */
var insertProduct = function (db, callback) {
    var command = {
        query: 'INSERT INTO Products (ProductID, ProductName, UnitsInStock, UnitPrice, Category) VALUES (@productId, @productName,@unitsInStock,@unitPrice, @category)',
        parameters: [
            { name: 'productId', value: 1001 },
            { name: 'productName', value: "Chai" },
            { name: 'unitsInStock', value: 39 },
            { name: 'unitPrice', value: 100.0 },
            // Category is an embedded document here
            { name: 'category', value: { ID: 13, Name: "Beverages", Description: "All kinds of Beverages" } }
        ]
    };
    db.executeNonQuery(command, callback);
};
/**
 * This method fetches product from the collection
 * @param {dbCollection} coll collection from which product will be fetched
 * @param {callback} callback callback that will be called after the operation
 */
var getProduct = function (db, callback) {
    var getQueryCmd = {
        query: 'Select * FROM Products WHERE ProductID = @productId',
        parameters: [
            { name: 'productId', value: 1001 }
        ]
    };
    db.executeReader(getQueryCmd, callback);
};
/**
 * This method updates product in the collection
 * @param {dbCollection} coll collection in which product will be updated
 * @param {callback} callback callback that will be called after the operation
 */
var updateProduct = function (db, callback) {
    var command = {
        query: 'UPDATE Products SET (UnitsInStock = @unitsInStock,ProductName = @productName, UnitPrice = @unitPrice, Category.Name = @categoryName) WHERE ProductID = @productId',
        parameters: [
            { name: 'unitsInStock', value: 45 },
            { name: 'unitPrice', value: Number(125.0) },
            { name: 'productName', value: "Tofu" },
            { name: 'categoryName', value: "Food" },
            { name: 'productId', value: 1001 }
        ]
    };
    db.executeNonQuery(command, callback);
};
/**
 * This method deletes product from the collection
 * @param {dbCollection} coll collection from which product will be deleted
 * @param {callback} callback callback that will be called after the operation
 */
var deleteProduct = function (db, callback) {
    var command = {
        query: 'DELETE FROM Products WHERE ProductID = @productId',
        parameters: [
            { name: 'productId', value: 1001 }
        ]
    };
    db.executeNonQuery(command, callback);
};
//# sourceMappingURL=app.js.map