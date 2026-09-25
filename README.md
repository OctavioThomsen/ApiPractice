# ApiPractice

Plantilla de practica para desarrollar una API REST en C# con .NET 8.

El proyecto esta intencionalmente vaci­o: la persona que lo utilice debe crear su propia implementacion. Se conserva la configuracion base de ASP.NET Core, Swagger, CORS, la cadena de conexion y la minima implementacion de Entity Framework Core.

### Ejecución de scripts de base de datos

Ejecutar **únicamente los siguientes scripts** en una instancia de **Microsoft SQL Server**, respetando estrictamente el orden indicado.

Se recomienda utilizar **SQL Server Management Studio (SSMS)** para su ejecución.

1. `Script_1_CreateDatabase.sql`
2. `Script_2_CreateTables.sql`
3. `Script_3_CreateData.sql`

## Estructura sugerida

Podes crear las siguientes carpetas y capas segun la solucion que elijas:

- `Controllers`: endpoints HTTP.
- `Data`: `DbContext` y configuracion de persistencia.
- `DTOs`: objetos de entrada y salida.
- `Extensions`: extensiones para configurar la aplicacion y la inyeccion de dependencias.
- `Interfaces`: contratos de repositorios y servicios.
- `Models`: entidades del dominio.
- `Repositories`: acceso a datos.
- `Services`: logica de negocio.

## Consigna

Implementar las operaciones siguientes utilizando clientes y facturas.

GET
1. Obtener todos los clientes activos.
2. Obtener los clientes activados en los ultimos 90 di­as.
3. Obtener todas las facturas de un cliente especi­fico (por ID).
4. Obtener todos los clientes con facturas impagas.
5. Calcular el monto total facturado para un cliente especi­fico.
6. Obtener clientes sin facturas.

POST
1. Crear un nuevo cliente.
2. Crear una nueva factura.

PUT
1. Actualizar un cliente existente.
2. Marcar una factura como pagada.

DELETE
1. Eliminar un cliente por ID.
2. Eliminar todas las facturas de un cliente especi­fico.

## Consultas SQL
1. Clientes y facturacion total – Stored Procedure
Obtener el listado de clientes activos que tienen al menos una factura, mostrando:
- Nombre
- CUIT
- Provincia
- Total facturado
- Cantidad de facturas

2. Facturas impagas por provincia – View
Mostrar, para cada provincia:
- Total acumulado de facturas impagas
- Cantidad de facturas impagas
- Ordenado de mayor a menor por total impago.

3. ultima factura por cliente – Inline TVF
Mostrar para cada cliente:
- ID
- Nombre
- Fecha de su ultima factura (si tiene)

4. Clientes sin facturas – View
Listar todos los clientes que no tienen ninguna factura asociada.

5. Clientes con promedio de facturacion alto – Scalar Function + Stored Procedure
Obtener los nombres de los clientes cuyo promedio de facturacion supera los $8000.

6. Factura mas cara – Stored Procedure con TOP 1
Mostrar la informacion de la factura con el monto mas alto, incluyendo:
- Nombre del cliente
- Fecha
- Monto
- Provincia

7. Top 3 facturas por cliente – CTE + Ranking
Mostrar las 3 facturas mas altas por cliente, utilizando CTE + ROW_NUMBER(), e incluyendo:
- ID del cliente
- Monto
- Fecha
- Ranking por cliente

