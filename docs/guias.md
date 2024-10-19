### Dar reset à base de dados
Precisam de fazer isto sempre que derem update do modelo de dominio e quiserem ver essas alterações na base de dados, para já ainda não encontrei uma maneira automatizada e segura de fazer isto:

- `dotnet ef database drop --froce`
- apagar a pasta Migrations
- `dotnet ef migrations add Initial`
- `dotnet ef database update`