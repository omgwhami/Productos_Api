# ProductosApi – CRUD con ADO.NET (.NET 10 + MySQL)

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

#### Flujo

Swagger → Controller → Repository (ADO.NET) → MySQL → Repository → Controller → Swagger

1. En Swagger se elige un endpoint y se manda la petición.
2. El Controller la recibe y revisa los datos.
3. El Controller le pide el trabajo al Repository.
4. El Repository abre la conexión a MySQL y ejecuta la consulta (insertar, consultar, actualizar, eliminar o buscar).
5. El resultado regresa por el Repository y el Controller.
6. Swagger muestra la respuesta (200, 201, 204 o 404).
