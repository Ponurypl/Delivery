# Delivery

## Przydatne komendy
### Docker
Podnoszenie db
``` Powershell
docker-compose -f db-docker-compose.yml up -d
```

Ubicie db
``` Powershell
docker-compose -f db-docker-compose.yml rm -f -s
```

### EF Core
Dodanie migracji
``` Powershell
Add-Migration *NazwaMigracji* -Project Infrastructure -OutputDir Persistence\Migrations 
```

Upgrade bazy
```Powershell
Update-Database
```

Wycofanie ostaniej migracji
```Powershell
Remove-Migration
````