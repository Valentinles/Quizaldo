# Quizaldo
**Quizaldo** is ASP.NET Core web application where *registered users* can paticipate in quizzes on different topics, suggest questions, unlock achievements, read jokes and vote for them. There is an *admin* account which allows you to *create/delete quizzes and add questions as well as approving or removing user's suggested questions* Admin account allows you to visit the notification page. *Guests* can see the *ranklist* and register to use more features.

**Quizzes**  </br>
<img src="https://i.postimg.cc/9QVT2RDL/quiz-Index.png" width="23%"></img> <img src="https://i.postimg.cc/NMy2DRnj/quiz-Start.png" width="23%"></img> <img src="https://i.postimg.cc/QNTWWk56/quiz-Result.png" width="23%"></img> <img src="https://i.postimg.cc/xd8XjKn3/quiz-Search.png" width="23%"></img> 

**Achievements, Ranking and Jokes**  </br>
<img src="https://i.postimg.cc/FKkhnGh8/achievements.png" width="23%"></img> <img src="https://i.postimg.cc/kGsCtpy6/MYachievements.png" width="23%"></img> <img src="https://i.postimg.cc/wMwKBs6D/ranking.png" width="23%"></img> <img src="https://i.postimg.cc/1tK2WNVb/jokes.png" width="23%"></img> 

**Admin features (notifications, forms and question suggestions)**  </br>
<img src="https://i.postimg.cc/pdJsxMVF/1845x800.png" width="23%"></img> <img src="https://i.postimg.cc/BZVMJg3M/admin-Question.png" width="23%"></img> <img src="https://i.postimg.cc/2S4wFNZK/admin-Add-Joke.png" width="23%"></img> <img src="https://i.postimg.cc/4y7vYDGw/admin-Suggestions.png" width="23%"></img> 


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
