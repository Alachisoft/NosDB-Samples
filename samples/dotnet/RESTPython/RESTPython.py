# ===============================================================================
# Alachisoft (R) NosDB Sample Code.
# ===============================================================================
# Copyright © Alachisoft.  All rights reserved.
# THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
# OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
# LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
# FITNESS FOR A PARTICULAR PURPOSE.
# ===============================================================================
import urllib.request
import requests
import json
import pprint

#A sample python script to demonstrate CRUD operations using REST API with NOSDB


requestUri = 'http://localhost/nosdbdata/clustered/northwind/customers'

#Insert Customer 'John Doe' into the 'Customers' Collection using HTTP POST Request.
jsonDocument = {}
#jsonDocument ['_key'] = 'Customer1'
jsonDocument ['CustomerID'] = 'Customer1'
jsonDocument ['Name'] = 'John Doe'
jsonDocument ['Address'] = 'Obere Street 57'
jsonDocument ['City'] = 'Berlin'

headers = {'Content-type': 'application/json'}

response = requests.post(requestUri, json.dumps(jsonDocument), headers=headers);

if response.status_code == 200:
    pprint.pprint("Inserted Successfully")
    pprint.pprint(response.json()['value'])
else:
    pprint.pprint("Error : " + response.reason)
print('')


#Get 'John Doe' from the Customers Collection using HTTP GET Request.
documentKey = {}
documentKey ['CustomerID'] = 'Customer1'
response = requests.get(requestUri, data=json.dumps(documentKey))

if response.status_code == 200:
    pprint.pprint("Retrieved Successfully")
else:
    pprint.pprint("Error : " + response.reason)
print('')


#Update 'John Doe' in Customers Collection using HTTP PUT Request.
updatedjsonDocument = {}
#updatedjsonDocument ['_key'] = 'Customer1'
updatedjsonDocument ['CustomerID'] = 'Customer1'
updatedjsonDocument ['Name'] = 'John Doe'
updatedjsonDocument ['Address'] = 'Keskuskatu 45'   #Update Address to Keskuskatu 45
updatedjsonDocument ['City'] = 'Helsinki'           #Update City from Berlin to Helsinki

updateUri = requestUri+ "('Customer1')"
response = requests.put(updateUri, json.dumps(updatedjsonDocument))

if response.status_code == 200:
    pprint.pprint("Updated Successfully")
else:
    pprint.pprint("Error : " + response.reason)
print('')

#Executing Query on Customers Collection using HTTP POST Request.                
queryDocument = {}
queryDocument ['Query'] = 'Select * FROM customers'
queryDocument ['Parameters'] = '[]'
queryHeaders = {'Content-type': 'application/nosquery'}

response = requests.post(requestUri, json.dumps(queryDocument), headers=queryHeaders);

if response.status_code == 200:
    pprint.pprint("Query Executed Successfully")
else:
    pprint.pprint("Error : " + response.reason)
print('')


#Delete 'John Doe' from the Customers Collection using HTTP DELETE Request.
deleteUri = requestUri+ "('Customer1')"
response = requests.delete(deleteUri, data=json.dumps(documentKey))

if response.status_code == 200:
    pprint.pprint("Deleted Successfully")
else:
    pprint.pprint("Error : " + response.reason)
print('')


