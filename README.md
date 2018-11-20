[SCENARIO]

XPTO company has a digital platform built under SOA that currently contains (insert random number "> 0" here) services/applications inside it. When a team needs to implement a new feature in their service, they implement it under a toggle so that the feature can be turned on or off as a fail-safe. To make a change in the toggle value on a production environment, the team needs to change it in a static file and restart the service/application in each server so that the changes take effect. This process can be painful and can lead to some unexpected behavior as the toggle value can be different in the multiple instances of the service/application.

[API REQUIREMENTS]
* Object oriented programming
* SOLID Principles
* Clean Code
* Design Patterns
* REST Web API that could manage in real-time and dynamically the platform toggles
* Authentication & Authorization
* When the services/applications request their toggles, they will only provide to the API their identifiers
* Only the services with the correct authorization can access the TogglerAPI
* This service must be able to register new toggles, by an user with Administrator privileges, that can be used by one or more services/applications with a hierarchy, as for example:
	- Toggle named isButtonBlue with value true can be used by all services
	- Toggle named isButtonBlue with value false can only be used by the service ABC overriding the value of the toggle mentioned above
	- Toggle named isButtonGreen with value true can only be used by the service ABC
	- Toggle named isButtonRed with value true can be used by all services except the service ABC
* Broadcast to every service/application that a toggle has changed its value without having to know who those same services/applications are
* SQL and NoSQL database
* Message Driven Development
* Service built with the possibility to scale and with high performance
* Unit/Automation/Integration Testing

[IMPLEMENTED FEATURES]
* Users with the Administrator role will be able to manage everything from creating, updating and deleting all Users, Roles, Services, Toggles and Toggle's permissions for a specific Service
* All other Users will have no privileges using the Web API
* Only the Services that are registered by their UniqueIdentifier in the Database will have access to their Toggles when invoking TogglerAPI. This UniqueIdentifier needs to be passed in the Endpoint. Check the [ENDPOINT] list below.
* Created TogglerAPITests project where Tests will be created. So far, only RoleService has defined tests though...

[MISSING FEATURES -> TODO LIST]
* Replace headers verifications with OAuth or any other kind of Token instead of passing Username and Password on every request
* Implement SignalR to notify that a Toggle's value has been modified to all Services
* Implement Swagger to document and consume REST APIs in a fancier way
* Find better "namings" for some classes / objects perhaps... some of the objects are too long
* Implement a Bulk system. The API is not prepared to insert/edit/delete in a "bulk way", you have to do it 1 by 1. That means if you want a Toggle to be available to every Service, you have to insert in the table ToggleServicePermissions saying that you want it available for each Service available
* Implement Automatic Tests
* Implement Units Tests


[BEST PRACTICES USED]
* REST API;
* Object oriented programming;
* SOLID Principles;
* Clean Code;
* Several Design Patterns being applied;
* Service built with the possibility to scale and with high performance
* Proper HTTP StatusCode being returned in all endpoints, or at least I think they are!
* Easy to maintain and modify the implementation

[SETUP CONFIGURATIONS]
* Clone this repository into your system
* I used SQL Express in my machine, thus the SQL Connection is:
	> "Server=localhost\\SQLEXPRESS;Database=Toggler;Trusted_Connection=True;"
* You may change this SQL ConnectionString in the appsettings.json for your SQL Server instance
* Assuming you cloned the Code with migrations included, open Package Manager Console and run the following command so your Database is configured properly:
	> update-database -context TogglerContext
* Incase you don't have any "migrations" in your system, do the following:
	- Open Package Manager Console and run the following command:
		> Add-Migration TogglerMigration -context TogglerContext
	- A partial class will be generated and you will need to replace everything inside with the next step
	- Open the File "TogglerMigration.txt" that is in the folder "Configurations" and copy all it's content and replace the generated class in Visual Studio
	- Go to Package Manager Console and run the following command:
		> update-database -context TogglerContext
* After a little bit it will create automatically the Database, all the tables and the relationships
* Open SQL Server Management Studio or whatever tool you may use and populate the database with dummy data. This dummy data can be found in the file "SQL Dummy Data.txt" inside the same "Configurations" folder
* Follow the steps in the "SQL Dummy Data.txt" file

[EXECUTION]
* Run TogglerAPI
* Open Postman or whatever tool you prefer to invoke endpoints from an API
* Base URL is: http://localhost:5000
* Base URL can be changed within the solution at launchSettings.json
* You can import my collection folder for Postman. It's in the file "Toggler.postman_collection.json"
* Test all endpoints against the Web API

[ENDPOINTS]
* Gets list of Toggles available for a service
> [GET] http://localhost:5000/api/toggleservicepermissions/{id}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 404 - Haven't found any Toggle for this Service

> 200 - Toggle list returned


########## ########## ##########


All other Endpoints requires Administrator privileges! Make sure to have the following content in your Request Header!
* Note: this Username was defined when you ran the SQL Dummy Data. If by instance you got a different Administrator user, use it.
	> Content-Type:application/json
	
	> Username:GrasherAdmin1
	
	> Password:Password

	
########## ########## ##########


* Creates new Role
> [POST] http://localhost:5000/api/roles

> [BODY]
> {
> 	"Name": "RoleTesteabc",
> 	"Description": "RoleTesteabc"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 500 - Internal Server Error

> 201 - Created - RoleID returned


########## ########## ##########


* Deletes Role by ID
> [DELETE] http://localhost:5000/api/roles/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 500 - Internal Server Error

> 204 - No Content


########## ########## ##########


* Gets Role by ID
> [GET] http://localhost:5000/api/roles/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Role not found

> 200 - Role returned


########## ########## ##########


* Gets list of Roles
> [GET] http://localhost:5000/api/roles

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Role not found

> 200 - Role list returned


########## ########## ##########


* Updates a Role by ID
> [PUT] http://localhost:5000/api/roles

> [BODY]
> {
> 	"RoleId": 17,
> 	"Name": "TemporaryRole",
> 	"Description": "TemporaryRole"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Role not found

> 204 - No Content


########## ########## ##########


* Creates an User
> [POST] http://localhost:5000/api/users

> [BODY]
> {
> 	"roleid": 1,
> 	"username": "CreatedUser",
> 	"password": "Password"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 500 - Internal Server Error

> 201 - Created - UserID returned


########## ########## ##########


* Deletes an User by ID
> [DELETE] http://localhost:5000/api/users/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User not found

> 204 - No Content


########## ########## ##########


* Deletes an User by Username
> [DELETE] http://localhost:5000/api/users/username/{username}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User not found

> 204 - No Content


########## ########## ##########


* Gets list of all Users' details
> [GET] http://localhost:5000/api/users

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User list not found

> 200 - User list returned


########## ########## ##########


* Gets User's details by ID
> [GET] http://localhost:5000/api/users/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User not found

> 200 - User returned


########## ########## ##########


* Gets User's details by Username
> [GET] http://localhost:5000/api/users/username/{username}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User not found

> 200 - User returned


########## ########## ##########


* Updates User's details
> [PUT] http://localhost:5000/api/users

> [BODY]
> {
> 	"userid": 11,
> 	"roleid": 1,
> 	"password": "Password12121"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - User not found

> 204 - No Content


########## ########## ##########


* Creates a new Service
> [POST] http://localhost:5000/api/services

> [BODY]
> {
> 	"name": "Grasher Service XPTO",
> 	"version": "1.1.1.0.1"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 500 - Internal Server Error

> 201 - Created - ServiceID returned


########## ########## ##########


* Deletes a Service by ID
> [DELETE] http://localhost:5000/api/services/{id}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Service not found

> 204 - No Content


########## ########## ##########


* Gets details for a Service
> [GET] http://localhost:5000/api/services/{id}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Service not found

> 200 - Service returned


########## ########## ##########


* Gets list of Services
> [GET] http://localhost:5000/api/services/

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Service list not found

> 200 - Service list returned


########## ########## ##########


* Updates a Service
> [PUT] http://localhost:5000/api/services/

> [BODY]
> {
> 	"serviceid":"B5EA917C-0D42-4F13-4E48-08D64DF69662",
> 	"name":"Grasher QWERTY",
> 	"version": "1.21.2"
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Service not found

> 204 - No Content


########## ########## ##########


* Creates a Toggle
> [POST] http://localhost:5000/api/toggles/

> [BODY]
> {
> 	"name":"NewToggleTest",
> 	"value": false
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 500 - Internal Server Error

> 201 - Created - ToggleID returned


########## ########## ##########


* Deletes a Toggler by ID
> [DELETE] http://localhost:5000/api/toggles/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle not found

> 204 - No Content


########## ########## ##########


* Gets Toggle by ID
> [GET] http://localhost:5000/api/toggles/{id}

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle not found

> 200 - Toggle returned


########## ########## ##########


* Gets Toggle list
> [GET] http://localhost:5000/api/toggles

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle not found

> 200 - Toggle list returned


########## ########## ##########


* Updates a Toggle
> [PUT] http://localhost:5000/api/toggles/

> [BODY]
> {
> 	"toggleid": 5,
> 	"name":"NewToggleTest",
> 	"value": true
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle not found

> 204 - No Content


########## ########## ##########


* Creates Toggle-Service Permissions
> [POST] http://localhost:5000/api/toggleservicepermissions/

> [BODY]
> {
> 	"toggleid":1,
> 	"serviceid":"BC088724-5EEB-E811-BC4F-704D7B3F4D32",
> 	"state":0,
> 	"overridenvalue":0
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle-Service not found

> 201 - Created


########## ########## ##########


* Deletes Toggle-Service Permission
> [DELETE] http://localhost:5000/api/toggleservicepermissions/toggleid/{id}/serviceid/{id}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle-Service not found

> 204 - No Content


########## ########## ##########


* Gets Permission details for a Toggle-Service
> [GET] http://localhost:5000/api/toggleservicepermissions/toggleid/{id}/serviceid/{id}

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle-Service not found

> 200 - Permission details for a Toggle-Service returned


########## ########## ##########


* Gets Permissions' list for Toggles-Services
> [GET] http://localhost:5000/api/toggleservicepermissions/

* Response StatusCodes
> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle-Service not found

> 200 - List with Permissions' details for all Toggles-Services returned


########## ########## ##########


* Updates Permissions for Toggle-Service
> [PUT] http://localhost:5000/api/toggleservicepermissions/

> [BODY]
> {
> 	"toggleid":1,
> 	"serviceid":"BC088724-5EEB-E811-BC4F-704D7B3F4D32",
> 	"state":0,
> 	"overridenvalue":0
> }

* Response StatusCodes
> 400 - Bad Request

> 401 - Invalid Credentials

> 403 - No Permission

> 404 - Toggle-Service not found

> 204 - No Content
