dotnet ef database drop --force --context IdentityContext
dotnet ef database drop --force --context DDDSample1DbContext
rmdir /s /q Migrations
dotnet ef migrations add InitialIdentity --context IdentityContext
dotnet ef migrations add Initial --context DDDSample1DbContext
dotnet ef database update --context IdentityContext
dotnet ef database update --context DDDSample1DbContext