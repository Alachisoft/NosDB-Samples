// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
var nosDBClient = require('nosdb').nosDBClient;
var connStr = require('./configuration').connectionString;
// Get database using the connection string specified in configuration.js
nosDBClient.getDatabase(connStr, function (dbErr, db) {
    if (dbErr) {
        console.log(dbErr);
    }
    else {
        // Get instance of customers collection
        db.getCollection("Customers", function (collErr, customersCollection) {
            if (collErr) {
                console.log(collErr);
            }
            else {
                // Insert customer in customers collection
                insertCustomer(db, function (err, num) {
                    if (err) {
                        console.log(err);
                    }
                    else {
                        console.log("\n'" + num + "' record(s) inserted!");
                    }
                    // Fetch customer from customers collection
                    getCustomer(db, function (err, reader) {
                        if (err) {
                            console.log(err);
                        }
                        else {
                            reader.toArray(function (readerErr, array) {
                                if (readerErr) {
                                    console.log(readerErr);
                                }
                                else if (array[0]) {
                                    var customerId = array[0].CustomerID;
                                    var companyName = array[0].CompanyName;
                                    var address = array[0].Address;
                                    var city = array[0].City;
                                    console.log("'" + array.length + "' record(s) retrieved");
                                }
                                else {
                                    console.log("No document is retrieved against 'Customer1'.");
                                }
                            });
                        }
                        // Update customer in customers collection
                        updateCustomer(db, function (err, num) {
                            if (err) {
                                console.log(err);
                            }
                            else {
                                console.log("'" + num + "' record(s) were updated!");
                            }
                            // Delete customer from customers collection
                            deleteCustomer(db, function (err, num) {
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
 * This method Inserts customer in the collection
 * @param {dbCollection} coll collection in which customer will be inserted
 * @param {callback} callback callback that will be called after the operation
 */
var insertCustomer = function (db, callback) {
    var command = {
        query: 'INSERT INTO Customers (CustomerID, CompanyName, Address, City) VALUES (@customerId, @customerName,@customerAddress,@customerCity)',
        parameters: [
            { name: 'customerId', value: "Customer1" },
            { name: 'customerName', value: "John Doe" },
            { name: 'customerAddress', value: "Obere Street 57" },
            { name: 'customerCity', value: "Berlin" }
        ]
    };
    db.executeNonQuery(command, callback);
};
/**
 * This method fetches customer from the collection
 * @param {dbCollection} coll collection from which customer will be fetched
 * @param {callback} callback callback that will be called after the operation
 */
var getCustomer = function (db, callback) {
    var getQueryCmd = {
        query: 'SELECT * FROM Customers WHERE CustomerID = @customerId',
        parameters: [{ name: 'customerId', value: "Customer1" }]
    };
    db.executeReader(getQueryCmd, callback);
};
/**
 * This method updates customer in the collection
 * @param {dbCollection} coll collection in which customer will be updated (replaced)
 * @param {callback} callback callback that will be called after the operation
 */
var updateCustomer = function (db, callback) {
    var command = {
        query: 'UPDATE Customers SET (Address = @customerAddress, City = @customerCity,CompanyName = @customerName) WHERE CustomerID = @customerId',
        parameters: [
            { name: 'customerAddress', value: "120 Hanover Sq." },
            { name: 'customerCity', value: "London" },
            { name: 'customerName', value: "John Doe" },
            { name: 'customerId', value: "Customer1" }
        ]
    };
    db.executeNonQuery(command, callback);
};
/**
 * This method deletes customer from the collection
 * @param {dbCollection} coll collection from which customer will be deleted
 * @param {callback} callback callback that will be called after the operation
 */
var deleteCustomer = function (db, callback) {
    var command = {
        query: 'DELETE FROM Customers WHERE CustomerID = @customerId',
        parameters: [
            { name: 'customerId', value: "Customer1" }
        ]
    };
    db.executeNonQuery(command, callback);
};
//# sourceMappingURL=app.js.map