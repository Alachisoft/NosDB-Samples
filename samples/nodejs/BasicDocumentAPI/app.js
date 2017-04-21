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
        // Get instance of products collection
        db.getCollection("Products", function (collErr, productsCollection) {
            if (collErr) {
                console.log(collErr);
            }
            else {
                // Insert products in products collection
                insertProductApi(productsCollection, function (err, failedDoc) {
                    if (err) {
                        console.log(err);
                    }
                    else if (failedDoc) {
                        console.log("Insert failed: " + failedDoc.errorMessage);
                    }
                    else {
                        console.log("\nDocument inserted successfully");
                    }
                    // Fetch product from product collection
                    getProductApi(productsCollection, function (err, document) {
                        if (err) {
                            console.log(err);
                        }
                        else if (document) {
                            var productId = document.ProductID;
                            var productName = document.productName;
                            var unitPrice = document.unitPrice;
                            var categoryId = document.Category.CategoryId;
                            var categoryName = document.Category.CategoryName;
                            var categoryDesc = document.Category.Description;
                            console.log("Document received successfully");
                        }
                        else {
                            console.log("Failed to retrieve document. Key does not exist in database.");
                        }
                        // Update product in product collection
                        updateProductApi(productsCollection, function (err, failedDoc) {
                            if (err) {
                                console.log(err);
                            }
                            else if (failedDoc) {
                                console.log("failed to update: " + failedDoc.errorMessage);
                            }
                            else {
                                console.log("Document updated successfully");
                            }
                            // Delete product from product collection
                            deleteProductApi(productsCollection, function (err, failedDoc) {
                                if (err) {
                                    console.log(err);
                                }
                                else if (failedDoc) {
                                    console.log("failed to delete: " + failedDoc.errorMessage);
                                }
                                else {
                                    console.log("Document deleted successfully");
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
var insertProductApi = function (coll, callback) {
    var command = {
        document: {
            ProductID: 1001,
            productName: "Chai",
            unitPrice: 100.0,
            Category: {
                CategoryId: 13,
                CategoryName: "Beverages",
                Description: "All kinds of Beverages"
            }
        }
    };
    coll.insert(command, callback);
};
/**
 * This method fetches product from the collection
 * @param {dbCollection} coll collection from which product will be fetched
 * @param {callback} callback callback that will be called after the operation
 */
var getProductApi = function (coll, callback) {
    var command = {
        key: { ProductID: 1001 }
    };
    coll.get(command, callback);
};
/**
 * This method updates product in the collection
 * @param {dbCollection} coll collection in which product will be updated (replaced)
 * @param {callback} callback callback that will be called after the operation
 */
var updateProductApi = function (coll, callback) {
    var command = {
        document: {
            ProductID: 1001,
            productName: "Tofu",
            unitPrice: 125.0,
            Category: {
                CategoryId: 13,
                CategoryName: "Beverages",
                Description: "All kinds of Beverages"
            }
        }
    };
    coll.update(command, callback);
};
/**
 * This method deletes product from the collection
 * @param {dbCollection} coll collection from which product will be deleted
 * @param {callback} callback callback that will be called after the operation
 */
var deleteProductApi = function (coll, callback) {
    var command = {
        key: { ProductID: 1001 }
    };
    coll.delete(command, callback);
};
//# sourceMappingURL=app.js.map