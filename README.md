# Quizaldo
**Quizaldo** is ASP.NET Core web application where *registered users* can paticipate in quizzes on different topics and suggest questions. There is an *admin* account which allows you to *create/delete quizzes and add questions as well as approving or removing user's suggested questions*. *Guests* can see the *ranklist* and register to use more features.

<img src="https://i.postimg.cc/d3W1q2yf/Screenshot-1.png" width="285"/> <img src="https://i.postimg.cc/sDXxTLFC/Screenshot-2.png" width="285"/> <img src="https://i.postimg.cc/Df5mdTMc/Screenshot-3.png" width="285"/> <img src="https://i.postimg.cc/28K6Mxmb/Screenshot-4.png" width="285"/> <img src="https://i.postimg.cc/tg0gCCs9/Screenshot-5.png" width="285"/>

**Admin features**  </br>
<img src="https://i.postimg.cc/28f5MSnm/Screenshot-5.png" width="285"/> <img src="https://i.postimg.cc/VkfksZ7V/Screenshot-6.png" width="285"/> <img src="https://i.postimg.cc/FKjHf5S3/Screenshot-8.png" width="285"/>


**Admin account**: </br>
 *username*: admin <br>  *password*: 123456
## Getting Started

###### To run the application you need:
- .NET Core 3.1 

- If you don't have *Sql server* on your machine you should replace the configuration in *Quizaldo.Web/appsettings.json* with this code:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=Quizaldo;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
- In your *package manger console* type: 

```
update-database
```

## Used technologies
- C#
- .NET Core 3.1
- Entity Framework Core
- Bootstrap
- HTML
- CSS
- JavaScript
