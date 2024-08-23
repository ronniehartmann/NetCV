# CV
Welcome to the CV-Wiki! This is a Blazor Web App with SSR (server side rendering). Its purpose is to visualize my CV in a modern website. It uses the [Radzen](https://blazor.radzen.com/) component library for a clean and simple design.

## Setup
Use the following instructions to start developing on your local environment. 

### Requirements
To start developing you need the following tools:

```
- Visual Studio 2022
- .NET 8 & C# 12
- Docker Desktop
```

### Clone Project
Use the following URL to clone the project:

```
https://github.com/ronniehartmann/NetCV.git
```

### Database
The easiest way to setup your database is to use Docker Desktop. The following command will create a container running a MariaDB database:

```
docker run --name netcv_mariadb -e MYSQL_ROOT_PASSWORD=netcv$ -e MYSQL_DATABASE=netcv -p 3306:3306 -d mariadb:latest
```

Now you have to apply the migrations, so the database contains the required tables. To do this, type the following command in the Package Manager Console in Visual Studio:

```
Update-Database
```