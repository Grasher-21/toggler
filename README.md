[SUMMARY]
- This TogglerAPI has several endpoints. All of them require "Administrator" privileges except for one.
- The "public" endpoint is http://localhost:5000/api/toggleservicepermissions/{id}
- Although that ID needs to be registered in the Services table.

[CONFIGURATIONS]
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
- Replace headers verifications with OAuth or any other kind of Token instead of passing Username and Password on every request
- Implement SignalR to notify that a Toggle's value has been modified
- Implement Swagger to test the API which becomes easy and user friendly to work with
- Find better "class naming" for some classes / objects
- The API is not prepared to insert/edit/delete in a "bulk way", you have to do it 1 by 1. That means if you want a Toggle to be available to every Service, you have to insert in the table ToggleServicePermissions saying that you want it available for each Service available
- Implement Automatic Tests
- Implement Units Tests
