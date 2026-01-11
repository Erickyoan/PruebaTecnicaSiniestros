# Prueba Tecnica - API Siniestros

Hola, esta es la solución que desarrolle para la prueba de backend. Es una API hecha en .NET 8.

## Como correr el proyecto

Para que funcione tienen que tener instalado el .NET 8 y tambien el SQL Server en la maquina.

1. Clonar el proyecto con git.
2. Abrir la carpeta en la terminal.
3. Ejecutar este comando para que se cree la base de datos:
   `dotnet ef database update -p Infrastructure -s WebApi`
   (Asegurate de estar en la carpeta raiz)
4. Para correr la api usa:
   `dotnet run --project WebApi`
   
O tambien le puedes dar Play desde el Visual Studio si lo tienes instalado.
Cuando arranque entra a `https://localhost:7152/swagger` (o el puerto que te salga) para probar.

---

## Explicación del Codigo (Dominio)

Use una arquitectura por capas para separar todo.
La clase principal es `Siniestro`, que es donde guardo la fecha del evento, fecha reportada, ciudad, direccion, departamento, numero de victimas...
Adentro del siniestro hay una lista de `Vehiculos`.

- **Siniestro:** Es la entidad principal. Valida que no metas fechas del futuro ni numero de victimas negativo. Y que la fecha del evento no sea menor a la reportada
- **Vehiculo:** Tiene la placa y el tipo. Le agregue el campo `Modelo` que puede ser nulo por si no hay un valor que se sepa del modelo.

No use diagramas pero basicamente Siniestro tiene muchos Vehiculos.

---

## Decisiones que tome (Arquitectura)

**1. Arquitectura Limpia**
Decidi separar en carpetas (Domain, Application, Infrastructure) para que no este todo mezclado. Asi es mas facil de mantener despues aunque al principio cuesta mas configurarlo.

**2. CQRS con MediatR**
Lei que es buena practica separar las lecturas de las escrituras. Use la libreria `MediatR` para mandar los comandos (Creates) y los queries (Gets). Asi el controlador queda mas limpio.

**3. Base de datos**
Use Entity Framework Core con Code First. Me parece mas rapido crear las clases primero y que el codigo haga la tabla.

---

## Lo que me falto

No alcance a terminar todo por el tiempo, pero esto es lo que falta:

- **Unit Tests:** Cree el proyecto de test pero me falto hacer los casos de prueba, solo esta la estructura.
- **Validaciones:** Me hubiera gustado usar FluentValidation para validar mejor los inputs, ahorita valido con `if` en el constructor.
- **Docker:** No alcance a crear el Dockerfile para contenerizarlo.
- **Seguridad:** Faltaria ponerle JWT para que no cualquiera pueda llamar al API.

Me tarde como unas 5 horas mas o menos haciendo todo y arreglando unos errores de migraciones que me salieron.