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
* Administrator User that has permissions to manage everything from creating, updating and deleting all Users, Roles, Services, Toggles and Toggle's permissions for a specific Service
* Only the Services that are registered in the Database will have access to their Toggles when invoking TogglerAPI

[MISSING FEATURES -> TODO LIST]
* Replace headers verifications with OAuth or any other kind of Token instead of passing Username and Password on every request
* Implement SignalR to notify that a Toggle's value has been modified to all Services
* Implement Swagger to document and consume REST APIs in a fancier way
* Find better "class naming" for some classes / objects perhaps... some of the objects are too long when it comes to names
* The API is not prepared to insert/edit/delete in a "bulk way", you have to do it 1 by 1. That means if you want a Toggle to be available to every Service, you have to insert in the table ToggleServicePermissions saying that you want it available for each Service available
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
- Clone this repository into your system
- I used SQL Express in my machine, thus the SQL Connection is "Server=localhost\\SQLEXPRESS;Database=Toggler;Trusted_Connection=True;"
- You may change this SQL ConnectionString in the appsettings.json for your SQL Server instance
- Assuming you cloned the Code with migrations included, open Package Manager Console and run the following command so your Database is configured properly:
	> update-database -context TogglerContext
- Incase you don't have any "migrations" in your system, do the following:
	- Open Package Manager Console and run the following command:
		> Add-Migration TogglerMigration -context TogglerContext
	- A partial class will be generated and you will need to replace everything inside with the next step
	- Open the File "TogglerMigration.txt" that is in the folder "Configurations" and copy all it's content and replace the generated class in Visual Studio
	- Go to Package Manager Console and run the following command:
		> update-database -context TogglerContext
- After a little bit it will create automatically the Database, all the tables and the relationships
- Open SQL Server Management Studio or whatever tool you may use and populate the database with dummy data. This dummy data can be found in the file "SQL Dummy Data.txt" inside the same "Configurations" folder
- Follow the steps in the "SQL Dummy Data.txt" file

[EXECUTION]
- Run TogglerAPI
- Open Postman or whatever tool you prefer to invoke endpoints from an API
- Base URL is: http://localhost:5000
- It can be changed within the solution at launchSettings.json
- You can import my collection folder for Postman. It's in the file "Toggler.postman_collection.json"

[NOTES]
- All endpoints require Username and Password with Administrator privileges in the Header except for one endpoint

[ENDPOINTS]
[POST] http://localhost:5000/api/roles


[TODO LIST]






Although some 
Unit/Automation/Integration Testing;

- 
- This TogglerAPI has several endpoints. All of them require "Administrator" privileges except for one.
- The "public" endpoint is http://localhost:5000/api/toggleservicepermissions/{id}
- Although that ID needs to be registered in the Services table.
