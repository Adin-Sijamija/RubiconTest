# RubiconTest

A test application for Rubicon employment offer.


General Information:

Framework: Net Core 3.1
Database: SQL server 2017
Development principle: Clean architecture
EndPoint access: swaggerUI
Testing: xunit integration testing for the controllers, MsUnit tests for shared code paths

Logging: serilog
Note that the application uses a serilog sink to offer logging data of request to a seq endpoint found on 
Note to use seq you need to download it from their website. Not having seq won't impede the work of the app.

Data generation:
The database is created and populated at the start of the application lifecycle.

Issues: 
If you have any issues clear the bin/debug folders if there are any and 
redownload your NuGet packages.
