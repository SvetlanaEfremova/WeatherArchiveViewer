An ASP.NET MVC application designed for adding and viewing weather data.

To launch the application, you need to install .NET SDK version 6.0 or higher and MSSQL Server 2019.

Launching the application via Visual Studio:
Open Visual Studio.
In the "File" menu, select "Open" -> "Project/Solution" or press Ctrl+Shift+O.
Choose the solution file "Weather_data_archive_viewer.sln".
In the "Build" menu, select the option "Rebuild Solution".
Check the correctness of the connection string in the "appsettings.json" file.
To create the database, open the Package Manager Console and execute the command:
Update-Database -Project Infrastructure
To launch the project, press the "Start Without Debugging" button or Ctrl+F5.

Launching the application using the command line:
Open the command line.
Execute the commands:
cd path/to/solution/folder, where the solution file is "Weather_data_archive_viewer.sln".
dotnet restore
dotnet build
dotnet ef database update --project Infrastructure
dotnet run