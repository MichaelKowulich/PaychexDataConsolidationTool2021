# Paychex ReFlex Data Consolidation Tool 2021
An ASP.NET Core Blazor webpage for the purpose of analyzing Paychex Flex data and consolidating it into 
a user-friendly webapp for viewing current and historical records. Developed by SUNY Brockport students in
collaboration with Paychex.

## Technologies
* .NET Core - version 3.1
* bunit - version 1.0.19
* XUnit - version 2.4.1
* MOQ - version 4.16.1
* Dapper - version 2.0.30
* ChartJs.Blazor.Fork - version 2.0.2

## Description of Flex Data
The consolidation tool looks at 4 different kinds of Flex Data:
* Total of Clients Per Status
* Total of Clients Per Type
* Total of Users Per Type (Flex Users)
* Total of Clients Per Brand

The tool is flexible and can handle new kinds of Statuses, Types, and Brands to be added to the respective database tables without causing issues in the code.

## Webpage Layout
The home page shows the data from the most recent Flex data report. On the left the totals of each data type are represented in a donut chart and on the right they can be
seen represented in a table. At the top of the chart are the color codings for each of the data types. Each data type entry can be clicked to show or hide that data type from the graph.

![alt text](https://github.com/MichaelKowulich/PaychexDataConsolidationTool2021/blob/master/Demo%20Images/HomePage.png)

From the navigation menu on the left you can visit the respective pages for each data model. From each of theses pages you can select a start date and end date from the 
date pickers and view data from all reports in the selected range. Up top you'll see a ChartJs linear graph showing the changing data counts over the range selected.
This graph has the same functionality of showing and hiding certain data types as the main page has.Below the graph all the data entries from the range of reports will
be shown in a table format. The page has sorting features for the different columns including the date,
count, and other data classifiers which are unique to each data model. Just select the column name and it will sort the entries by that parameter.

![alt text](https://github.com/MichaelKowulich/PaychexDataConsolidationTool2021/blob/master/Demo%20Images/ClientsPerStatusPage.png)

## Building 
After cloning (or downloading and unzipping), you'll need to add an appsettings.json file under PaychexDataConsolidationTool2021/PaychexDataConsolidationTool/PaychexDataConsolidationTool/.
You'll need to add the following code to the file and insert your connection string where shown below.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<Your Connection String Here>"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

## Setting Up The Database
Run the DatabaseTablesSetup.sql script on your azure database to setup the tables needed for the application. Once the tables are setup
you will just need to add your own data.

## Project Status
Project is: _still in progress_

## Contact
#### Created by:
* [@Michael Kowulich](https://github.com/MichaelKowulich)
* [@Austin Ellsworth](https://github.com/Austin-Ellsworth)
* [@Ian Farrell](https://github.com/ianfarre11)
* [@Dave Duncan](https://github.com/djduncan585)
