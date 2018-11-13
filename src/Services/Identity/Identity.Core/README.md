Add migrations:

 dotnet ef  migrations add InitialApplicationDbContext --context ApplicationDbContext --startup-project Identity.API --project Identity.Core -o ./Infrastructure/Database/Migrations

 dotnet ef  migrations add InitialPersistedGrantDbContext --context PersistedGrantDbContext --startup-project Identity.API --project Identity.Core -o ./Infrastructure/Database/Migrations

dotnet ef  migrations add InitialConfigurationDbContext  --context ConfigurationDbContext --project Identity.Core  --startup-project Identity.API -o ./Infrastructure/Database/Migrations



List migrations:

dotnet ef migrations list --context ApplicationDbContext --project Identity.Core  --startup-project Identity.API


Scripts:

dotnet ef migrations script --context ApplicationDbContext --project Identity.Core  --startup-project Identity.API