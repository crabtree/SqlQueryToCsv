# SqlQueryToCsv

Simple program that runs SQL query and outputs results into CSV file. Theoretically should work with every SQL engine that is supported by [Simple.Data](https://github.com/markrendle/Simple.Data).

## Usage

### Connection string
Database connection string should be specified in ```App.config``` file with ```name``` attribute ```Default```

### Program arguments
```--query``` - specify SQL query string

```--queryFile``` - specify path to a file with a SQL query

```--outFilePath``` - specify output file path 

### SQL query parameters

You can bind parameters with your SQL query, just specify them when calling executable like below

```
SQCApplication.exe --query="SELECT * FROM Order WHERE Id = @Id" --outFilePath="D:\Temp\out.csv" @Id="47A3EEF1-4FC6-469D-A2CC-000860203D70"
```
