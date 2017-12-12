dotnet new sln 
dotnet new console -n $1
dotnet new classlib -n $1.implementation
dotnet new nunit -n $1.test
dotnet sln $1.sln add */*.csproj
dotnet add $1/$1.csproj reference $1.implementation/$1.implementation.csproj
dotnet add $1.test/$1.test.csproj reference $1.implementation/$1.implementation.csproj
