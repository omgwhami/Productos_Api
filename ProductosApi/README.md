# ProductosApi – CRUD con ADO.NET (.NET 8 + MySQL)

API para administrar productos usando ADO.NET (MySqlConnector), patrón Repository y Swagger.

#### Cómo correrlo
1. Ejecutar `Database/script.sql` en MySQL.
2. Poner tu contraseña en `appsettings.json` (`ConnectionStrings:Default`).
3. `dotnet restore`
4. `dotnet run`
5. Abrir `http://localhost:<puerto>/swagger` (el puerto sale en la consola).

#### Endpoints
| Método | Ruta | Qué hace |
|---|---|---|
| GET | /api/productos | Lista todos |
| GET | /api/productos/{id} | Trae uno |
| GET | /api/productos/buscar?nombre=x | Busca por nombre |
| POST | /api/productos | Crea |
| PUT | /api/productos/{id} | Actualiza |
| DELETE | /api/productos/{id} | Elimina |
