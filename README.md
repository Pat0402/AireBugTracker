# AireBugTracker
Aire logic tech test bug tracker

The below cann all be found here with more details on design/stories etc.. > https://github.com/Pat0402/AireBugTracker/wiki

# AireBugTracker project documentation

This wiki is concerned with documenting the Aire Logic "Bug tracker" design and user stories.

This project aims to implement a simple bug tracker with user interface and api.

[User Stories](https://github.com/Pat0402/AireBugTracker/wiki/User-Stories) based on the project brief can be found here.

## Running the solution
It is recommended you install Docker Desktop
https://www.docker.com/?utm_source=google&utm_medium=cpc&utm_campaign=search_emea_brand&utm_term=docker_exact&gclid=Cj0KCQjwkOqZBhDNARIsAACsbfKZMlGn5TkfEN0CefPbYjspER2TWQhKFYevGr26R1x_FL1PeJjX8bQaApFAEALw_wcB

Then <br>
 ~~~ 
> CD "path/to/gitrepo"
> docker-compose build
> docker-compose up
~~~

This will spin up 3 containers, the api, web app and sql server.

**api address    :** http://localhost:8000 <br>
**swagger address:** http://localhost:8000/swagger/index.html <br>
**web app address:** http://localhost/8080 <br>
**sql server     :** 127.0.0.1, 8433 <br>
**sql user       :** sa <br>
**sql password   :** DevPassword123

There is config to run on a local machine also but will require sql server, dotnet 6 and ef 6.4. I've only tested this config running through visual studio ide but I'm sure CLI commands will work if you set the env to Development.

## Folder Structure
```
+---AireBugTrackerApi
|   +---Controllers
|   +---Helpers
|   \---Properties
+---AireBugTrackerWeb
|   +---Controllers
|   +---Models
|   +---obj
|   |   +---Container
|   +---Properties
|   +---Views
|   |   +---Bug
|   |   +---Shared
|   |   \---User
|   \---wwwroot
|       +---css
|       +---js
|       \---lib
+---DatabaseContext
|   +---Migrations
|   +---Models
+---Repositories
+---RespositoryTests
|   +---Helpers
+---Services
|   +---Interfaces
|   +---Models
+---ServiceTests
```
