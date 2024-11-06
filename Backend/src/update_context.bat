dotnet ef database drop --force 
dotnet ef migrations remove
dotnet ef migrations add Initial
dotnet ef database update