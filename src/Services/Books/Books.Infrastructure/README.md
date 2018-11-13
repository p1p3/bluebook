Add migrations:

dotnet ef  migrations add InitialApplicationDbContext --context BooksManagmentContext --startup-project Books.API --project Books.Infrastructure -o ./Database/Migrations