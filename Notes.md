Setting up ng project in 4 steps:
1- start up container with `tail -f /dev/null` on docker-compose.yml (line 9)
2- run `ng new` to create an angular project `ng new net-ng`
    ✔ Which stylesheet format would you like to use? CSS             [ https://developer.mozilla.org/docs/Web/CSS                     ]
    ✔ Do you want to enable Server-Side Rendering (SSR) and Static Site Generation (SSG/Prerendering)? Yes
    ✔ Do you want to create a 'zoneless' application without zone.js? Yes
    ✔ Which AI tools do you want to configure with Angular best practices? https://angular.dev/ai/develop-with-ai None

    https://angular.dev/guide/ssr

3- rebuild container using line 8
4- uncomment line 24 dockerfile

Setting up webapi
- create dotnet webapi `dotnet new webapi --use-controllers -o expenses-api`
    `dotnet add package Microsoft.EntityFrameworkCore.InMemory`
    `dotnet add package NSwag.AspNetCore`
    `dotnet run --launch-profile https`
    `dotnet build`
    https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#install-entity-framework-core
    `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
    `dotnet tool install --global dotnet-ef`
     
```shell
Tools directory '/root/.dotnet/tools' is not currently on the PATH environment variable.
If you are using bash, you can add it to your profile by running the following command:

cat << \EOF >> ~/.bash_profile
# Add .NET Core SDK tools
export PATH="$PATH:/root/.dotnet/tools"
EOF

You can add it to the current session by running the following command:

export PATH="$PATH:/root/.dotnet/tools"

You can invoke the tool using the following command: dotnet-ef
Tool 'dotnet-ef' (version '9.0.10') was successfully installed.
```
-- initial db
`dotnet-ef migrations add InitialCreate`
`dotnet-ef database update`

-- auth toolkit
`dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`
## Browse JWToken:
https://www.jwt.io/


-- adding bootstrap
`npm install bootstrap`

## browse swagger
- https://localhost:7186/openapi/v1.json

- https://localhost:7186/swagger/index.html#/WeatherForecast/GetWeatherForecast
explore:/openapi/v1.json

- https://localhost:7186/weatherforecast/


### adding ng components
`ng generate component components/header`
`ng generate component components/footer`
`ng generate component components/login`
`ng generate component components/signup`
`ng generate component components/transaction-form`
`ng generate component components/transaction-list`

### adding models
`ng generate interface models/user`
`ng generate interface models/transaction`
`ng generate interface models/authresponse`

### adding services
`ng generate service services/auth-service`
`ng generate service services/transaction-service`