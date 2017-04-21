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
var fs = require('fs');
var attachmentStore = require('nosdb').attachmentStore;
var attachment = require('nosdb').attachment;
var attachmentWriteStream = require('nosdb').attachmentWriteStream;
var attachmentReadStream = require('nosdb').AttachmentReadStream;
var fs = require('fs');
var employeeId = 59374347;
var attachmentId = "Employee:" + 59374347;
// Get database using the connection string specified in configuration.js
nosDBClient.getDatabase(connStr, function (dbErr, db) {
    if (dbErr) {
        console.log(dbErr);
    }
    else {
        // Get instance of employee collection
        db.getCollection("employees", function (collErr, employeesCollection) {
            if (collErr) {
                console.log(collErr);
            }
            else {
                // Insert employee in employee collection
                insertEmployee(employeesCollection, function (err, failedDoc) {
                    if (err) {
                        console.log(err);
                    }
                    else if (failedDoc) {
                        console.log("failed to insert: " + failedDoc.errorMessage);
                    }
                    else {
                        var metadata = { fileType: "BMP", info: "New Employee picture" };
                        // Insert employee picture in attachment collection
                        insertEmployeePhotoAsAttachment(db, metadata, function (errInsAttach, msgInsAttach) {
                            if (errInsAttach) {
                                console.log("failed to insert attachment: " + errInsAttach);
                            }
                            else {
                                console.log("Attachment inserted successfully. \n" + msgInsAttach);
                            }
                            // Fetch employee from employee collection
                            getEmployee(employeesCollection, function (err, document) {
                                if (err) {
                                    console.log(err);
                                }
                                else if (document) {
                                    // Fetch employee picture from attachment collection
                                    getEmployeePhotoFromAttachment(db, function (errGetAttach, attachmentReadableStream) {
                                        if (errGetAttach) {
                                            console.log("failed: " + errGetAttach);
                                        }
                                        else {
                                            if (attachmentReadableStream) {
                                                var writableStream = fs.createWriteStream("RetrievedImage.BMP");
                                                writableStream.once('error', function () {
                                                    console.log("Error While opening writeable stream.");
                                                    return;
                                                });
                                                attachmentReadableStream.pipe(writableStream);
                                                attachmentReadableStream.on('end', function () {
                                                    console.log("Stream writing is finished!!!!");
                                                    //Stream writing finished!. 
                                                    // Update employee in employee collection
                                                    updateEmployee(employeesCollection, function (err, failedDoc) {
                                                        if (err) {
                                                            console.log(err);
                                                        }
                                                        else if (failedDoc) {
                                                            console.log("failed top update employee: " + failedDoc.errorMessage);
                                                        }
                                                        else {
                                                            var metadata = { fileType: "BMP", info: "New Employee picture" };
                                                            // Update (replace) employee picture in attachment collection
                                                            updateEmployeePhotoFromAttachment(db, metadata, function (errInsAttach, msgInsAttach) {
                                                                if (errInsAttach) {
                                                                    console.log("failed to update attachment: " + errInsAttach);
                                                                }
                                                                else {
                                                                    console.log("Attachment updated successfully.\n " + msgInsAttach);
                                                                }
                                                                // Delete employee from employee collection
                                                                deleteEmployee(employeesCollection, function (err, failedDoc) {
                                                                    if (err) {
                                                                        console.log(err);
                                                                    }
                                                                    else if (failedDoc) {
                                                                        console.log("failed to delete employee: " + failedDoc.errorMessage);
                                                                    }
                                                                    else {
                                                                        // Delete employee picture from attachment collection
                                                                        deleteEmployeePhotoFromAttachment(db, function (errDltAttach) {
                                                                            if (errDltAttach) {
                                                                                console.log("Delete attachment failed: " + errDltAttach);
                                                                            }
                                                                            else {
                                                                                console.log("Attachment deleted successfully");
                                                                            }
                                                                            // Release resources
                                                                            db.dispose();
                                                                        });
                                                                    }
                                                                });
                                                            });
                                                        }
                                                    });
                                                });
                                                attachmentReadableStream.once('error', function (errorWriting) {
                                                    console.log("Failed. Exiting due to error. Error: " + errorWriting);
                                                    return;
                                                });
                                                var empId = document.EmployeeID;
                                                var firstName = document.FirstName;
                                                var lastName = document.LastName;
                                                var title = document.Title;
                                                var titleOfCourtesy = document.TitleOfCourtesy.Description;
                                                console.log("Attachment received successfully");
                                            }
                                            else {
                                                console.log("Invalid attachment readable stream!");
                                            }
                                        }
                                    });
                                }
                            });
                        });
                    }
                });
            }
        });
    }
});
/**
 * This method Inserts employee in the collection
 * @param {dbCollection} coll collection in which employee will be inserted
 * @param {callback} callback callback that will be called after the operation
 */
var insertEmployee = function (coll, callback) {
    var insertCommand = {
        document: {
            EmployeeID: employeeId,
            FirstName: "John",
            LastName: "Doe",
            Title: "Sales Representative",
            TitleOfCourtesy: "Mr."
        }
    };
    coll.insert(insertCommand, function (insertErr, insertFailedDoc) {
        return callback(insertErr, insertFailedDoc);
    });
};
/**
 * This method fetches employee from the collection
 * @param {dbCollection} coll collection from which employee will be fetched
 * @param {callback} callback callback that will be called after the operation
 */
var getEmployee = function (coll, callback) {
    var command = {
        key: { EmployeeID: employeeId }
    };
    coll.get(command, callback);
};
/**
 * This method updates employee in the collection
 * @param {dbCollection} coll collection in which employee will be updated (replaced)
 * @param {callback} callback callback that will be called after the operation
 */
var updateEmployee = function (coll, callback) {
    var command = {
        document: {
            EmployeeID: employeeId,
            FirstName: "John",
            LastName: "Doe",
            Title: "Sales Manager",
            TitleOfCourtesy: "Mr."
        }
    };
    coll.update(command, callback);
};
/**
 * This method deletes employee from the collection
 * @param {dbCollection} coll collection from which employee will be deleted
 * @param {callback} callback callback that will be called after the operation
 */
var deleteEmployee = function (coll, callback) {
    var command = {
        key: { EmployeeID: employeeId }
    };
    coll.delete(command, callback);
};
/**
 * This method Inserts employee pitcure in the attachment collection
 * @param {database} db database whose attachment collection will be used to store employee picture
 * @param {callback} callback callback that will be called after the operation
 */
var insertEmployeePhotoAsAttachment = function (db, metadata, callback) {
    db.getAtachmentStore(function (errAtachmnetStore, attachmentStore) {
        if (errAtachmnetStore)
            return callback(errAtachmnetStore);
        else {
            // ------- insertAttachment Use Case
            attachmentStore.insertAttachment(attachmentId, metadata, function (errInsertAttachment, writeableStream) {
                if (errInsertAttachment) {
                    return callback(errInsertAttachment);
                }
                else {
                    var readStream = fs.createReadStream("Pictures/2.BMP");
                    readStream.pipe(writeableStream);
                    writeableStream.on('finish', function () {
                        callback(null, 'Stream successfully written in Store');
                    });
                    writeableStream.once('error', function (errorWriting) {
                        callback(errorWriting);
                    });
                }
            });
        }
    });
};
/**
 * This method Fetches employee pitcure from the attachment collection
 * @param {database} db database whose attachment collection will be used to fetch employee picture
 * @param {callback} callback callback that will be called after the operation
 */
var getEmployeePhotoFromAttachment = function (db, callback) {
    db.getAtachmentStore(function (errAtachmnetStore, attachmentStore) {
        if (errAtachmnetStore)
            return callback(errAtachmnetStore);
        else {
            // ------- getAttachment Use Case
            attachmentStore.getAttachment(attachmentId, function (errGetAttach, attachment) {
                if (errGetAttach)
                    return callback(errGetAttach);
                else {
                    attachment.getAttachmentStream(callback);
                }
            });
        }
    });
};
/**
 * This method updates (replaces) employee pitcure in the attachment collection
 * @param {database} db database whose attachment collection will be used to update employee picture
 * @param {callback} callback callback that will be called after the operation
 */
var updateEmployeePhotoFromAttachment = function (db, metadata, callback) {
    db.getAtachmentStore(function (errAtachmnetStore, attachmentStore) {
        if (errAtachmnetStore)
            return callback(errAtachmnetStore);
        else {
            // ------- updateAttachment Use Case
            attachmentStore.updateAttachment(attachmentId, metadata, function (errUpdateAttachment, writeableStream) {
                if (errUpdateAttachment) {
                    return callback(errUpdateAttachment);
                }
                else {
                    var readStream = fs.createReadStream("Pictures/1.BMP");
                    readStream.pipe(writeableStream);
                    writeableStream.once('finish', function () {
                        callback(null, 'Stream successfully written in Store');
                    });
                    writeableStream.once('error', function (errorWriting) {
                        callback(errorWriting);
                    });
                }
            });
        }
    });
};
/**
 * This method deletes employee pitcure from the attachment collection
 * @param {database} db database whose attachment collection will be used to delete employee picture
 * @param {callback} callback callback that will be called after the operation
 */
var deleteEmployeePhotoFromAttachment = function (db, callback) {
    db.getAtachmentStore(function (errAtachmnetStore, attachmentStore) {
        if (errAtachmnetStore)
            return callback(errAtachmnetStore);
        else {
            // ------- deleteAttachment Use Case
            attachmentStore.deleteAttachment(attachmentId, callback);
        }
    });
};
//# sourceMappingURL=app.js.map