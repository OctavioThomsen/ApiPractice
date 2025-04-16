# Funcionalidades del Proyecto
Este proyecto está desarrollado en C# e incluye endpoints HTTP simulados, así como consultas SQL que utilizan procedimientos almacenados, vistas, funciones y CTEs para resolver distintas necesidades de negocio.

## Endpoints HTTP (C#)
GET
1. Obtener todos los clientes activos.
2. Obtener los clientes activados en los últimos 90 días.
3. Obtener todas las facturas de un cliente específico (por ID).
4. Obtener todos los clientes con facturas impagas.
5. Calcular el monto total facturado para un cliente específico.
6. Obtener clientes sin facturas.

POST
1. Crear un nuevo cliente.
2. Crear una nueva factura.

PUT
1. Actualizar un cliente existente.
2. Marcar una factura como pagada.

DELETE
1. Eliminar un cliente por ID.
2. Eliminar todas las facturas de un cliente específico.

## Consultas SQL
1. Clientes y facturación total – Stored Procedure
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

3. Última factura por cliente – Inline TVF
Mostrar para cada cliente:
- ID
- Nombre
- Fecha de su última factura (si tiene)

4. Clientes sin facturas – View
Listar todos los clientes que no tienen ninguna factura asociada.

5. Clientes con promedio de facturación alto – Scalar Function + Stored Procedure
Obtener los nombres de los clientes cuyo promedio de facturación supera los $8000.

6. Factura más cara – Stored Procedure con TOP 1
Mostrar la información de la factura con el monto más alto, incluyendo:
- Nombre del cliente
- Fecha
- Monto
- Provincia

7. Top 3 facturas por cliente – CTE + Ranking
Mostrar las 3 facturas más altas por cliente, utilizando CTE + ROW_NUMBER(), e incluyendo:
- ID del cliente
- Monto
- Fecha
- Ranking por cliente
