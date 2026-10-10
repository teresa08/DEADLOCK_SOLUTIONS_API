# Deadlock Solutions - Prueba Tecnica

API REST en .NET 8 (C#) para gestionar clientes, ordenes de compra y facturacion, con un frontend basico en React.

## Requisitos

- .NET SDK 8 o superior
- SQL Server (local o Express) escuchando en `localhost,1433`
- Node.js 20+ (solo si se quiere modificar el frontend)

## Como correr el proyecto

```bash
cd DEADLOCK_SOLUTIONS
dotnet run --launch-profile http
```

- Frontend: http://localhost:5078
- Swagger: http://localhost:5078/swagger

Al iniciar, la aplicacion aplica las migraciones de EF Core, crea la base de datos `DEADLOCK` y carga cuatro productos de ejemplo.

La cadena de conexion esta en `DEADLOCK_SOLUTIONS/appsettings.json` (`ConnectionStrings:Deadlock`) y usa autenticacion de Windows. Para usar otro servidor o credenciales basta con cambiarla alli.

## Endpoints

| Verbo | Ruta | Descripcion |
| --- | --- | --- |
| GET | `/api/clientes` | Lista los clientes |
| GET | `/api/clientes/{id}` | Obtiene un cliente |
| POST | `/api/clientes` | Registra un cliente |
| GET | `/api/productos` | Lista los productos con su stock |
| POST | `/api/productos` | Registra un producto |
| GET | `/api/ordenes` | Lista las ordenes |
| GET | `/api/ordenes/{id}` | Obtiene una orden |
| POST | `/api/ordenes` | Crea una orden (minimo dos productos) y descuenta stock |
| PUT | `/api/ordenes/{id}/items/{itemId}` | Actualiza la cantidad de un item y ajusta el stock |
| DELETE | `/api/ordenes/{id}/items/{itemId}` | Elimina un item y devuelve su cantidad al stock |
| POST | `/api/ordenes/{id}/factura` | Genera la factura de la orden |
| GET | `/api/ordenes/{id}/factura` | Obtiene la factura de la orden |

Todas las respuestas usan el mismo sobre: `{ data, status, message, isSuccess }`.

## Estructura

```
DEADLOCK_SOLUTIONS/
  Controllers/        Reciben la peticion y delegan en los servicios
  Aplicacion/         DTOs, interfaces de servicios, servicios y mapeos
  Dominio/            Entidades e interfaces de repositorios
  Infraestructura/    DbContext, migraciones y repositorios
  Middleware/         Manejo global de excepciones
  wwwroot/            Frontend compilado
frontend/             Codigo fuente del frontend (React + Vite)
```

## Decisiones tecnicas

- **Capas**: controlador -> servicio -> repositorio. Los controladores no tienen logica de negocio; solo traducen el `Response<T>` del servicio a la respuesta HTTP.
- **Unit of Work**: las operaciones que tocan orden y stock se confirman con un unico `SaveChanges`, por lo que son atomicas.
- **Items y factura como subrecursos de la orden**: no existen fuera de una orden, por eso sus rutas cuelgan de `/api/ordenes/{id}`.
- **Precio unitario en el item**: se copia el precio del producto al crear la orden para que un cambio de precio posterior no altere ordenes ni facturas.
- **Orden facturada**: una vez facturada no se puede modificar (409 Conflict) y solo admite una factura.
- **Errores**: 400 para validaciones, 404 para recursos inexistentes, 409 para conflictos de negocio (stock insuficiente, orden facturada) y 500 controlado por middleware.

## Frontend

Se uso **React con Vite** por ser liviano, rapido de configurar y suficiente para pantallas simples que consumen la API. El compilado se sirve desde `wwwroot`, asi que no hace falta levantar nada adicional.

Para modificarlo:

```bash
cd frontend
npm install
npm run dev
```

Para regenerar el compilado en `wwwroot`:

```bash
npm run build
```

## Que cambiaria en produccion

- Control de concurrencia en el stock (`rowversion`) para evitar sobreventa con peticiones simultaneas.
- Cadena de conexion en secretos o variables de entorno y migraciones aplicadas en el despliegue, no al iniciar la aplicacion.
- Paginacion en los listados, autenticacion y pruebas unitarias de los servicios.
- Impuestos y numeracion de facturas con una secuencia propia.
