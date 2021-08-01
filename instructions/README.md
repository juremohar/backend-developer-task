# Setup

Provide a file with instructions on how to set up and use the API:
* dependencies
* variables to be set
* run configs
* file system permissions
* ...

### Optional
You can provide Docker and Docker Compose files instead.


I was using .net 5.0 (https://dotnet.microsoft.com/download/dotnet/5.0)
and MySQL 8.0 (https://dev.mysql.com/downloads/mysql/)

## dependencies
* EntityFrameworkCore
* EntityFrameworkCore.Tools
* Pomelo.EntityFrameworkCore.MySql
* Swashbuckle.AspNetCore

All dependencies are installed at the start of the program

## variables to be set

in appsettings.Development.json you need to set "ConnectionStrings", to your instance of database

It should look something like that:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost; port=3306; database=notes; user=root; password=root;"
  }
}
```

I was running the API in visual studio via IIS.


Some notes:

- I put plain password in db (they should be hashed)
- For authorization I would normally use JWT token, which would be passed to the api in the header or in though cookie
- I didn't implement proper error handling since there is no frontend to test it, but where I throw an exception I would catch it in middleware and return nice response to the frontend with details about the error
- You can use swagger to preview the api (https://localhost:5001/swagger/index.html)
- For testing the api I was using "Insomnia" (https://insomnia.rest/)
- Database is not as clean as it could be (DateTime is a little weird, no indexes, some field should be mandatory required, some can be null)

I tried to do it as scalable as I could think of. You could have used a bool for "private/public", but I made a new table for it.
You could also have IdFolder in notes table, but I made m:n table for it.

A few example api calls:
```
curl --request GET \
  --url 'https://localhost:5001/note?OrderByDirection=desc&OrderByField=heading' \
  --header 'Authorization: Basic ZG9wcGxlcjpkb3BwbGVy'

curl --request GET \
  --url 'https://localhost:5001/note?Page=1&Limit=2' \
  --header 'Authorization: Basic ZG9wcGxlcjpkb3BwbGVy'

curl --request GET \
  --url 'https://localhost:5001/note?IdFolder=9' \
  --header 'Authorization: Basic ZG9wcGxlcjpkb3BwbGVy'

curl --request POST \
  --url 'https://localhost:5001/note?IdFolder=9' \
  --header 'Authorization: Basic ZG9wcGxlcjpkb3BwbGVy' \
  --header 'Content-Type: application/json' \
  --data '{
  "idFolder": 9,
  "visibilityCode": "public",
  "title": "test note",
  "noteBodyTypeCode": "single",
  "body": [
    "test"
  ]
}'

```

I hope I did everything in the task that was requested. If I missed something please let me know.

If you have any questions or you have trouble with the setup please contact me at jure.mohar5@gmail.com

Thank you and have a great day :).
