# Aplicacion de Paises (ASP.NET en FrontEnd + API en BackEnd)

Este proyecto es una aplicacion web desarrollada con MVC en FrontEnd y API en BackEnd que permite:


- Buscar paises por nombre

- Buscar paises por continente

- Ver detalles de cada pais (bandera, escudo, capital, poblacion, etc)

- Agregar paises a una lista de favoritos

- Guardar los favoritos en una base de datos SQL Server


## API (BackEnd)


La API cuenta con un controlador **Pais** con endpoints que consumen la API publica de [**RestCountries**](https://restcountries.com/) para usar
sus respuestas en un modelo propio para ser utilizado en el FrontEnd.

Además de los endpoints anteriores, la solución incluye un segundo controlador llamado **Favoritos**, encargado de guardar registros en una base de datos local SQL Server.

### Endpoints

<img width="1314" height="212" alt="image" src="https://github.com/user-attachments/assets/c9582766-5d0d-4987-a25c-42c3bb1ab7a7" />

#### *GET /api/pais/nombre/{nombre}*

Retorna los detalles de un pais a partir de su nombre en español.

#### *GET /api/pais/region/{region}*

Retorna una lista de paises a partir del nombre en ingles de un continente

#### *GET /api/pais/continentes*

Retorna una lista con todos los continentes

<img width="1299" height="157" alt="image" src="https://github.com/user-attachments/assets/2295db57-b592-4355-86f7-6a04d98ffc8c" />

#### *GET /api/favoritos*
Retorna todos los paises guardados en la base de datos local.

#### *POST /api/favoritos*
Recibe una lista de paises desde la aplicación MVC y los almacena en SQL Server.


## MVC (FrontEnd)

El frontend fue hecho usando MVC, y utilizando la API como modelo de datos.Es decir, la aplicación MVC no consulta directamente a RestCountries,
sino que consume los endpoints de la API

La vista principal presenta tres opciones

 **Buscar Paises**  
Permite buscar un pais por su nombre. Por defecto, al ingresar se muestran los detalles de **Argentina**.  
En esta opcion se puede:
- Ver bandera, capital, poblacion, idiomas,etc.
- Agregar o quitar el país de la lista de favoritos.

 **Buscar por Continente**  
Permite navegar países filtrados por continentes.

**Favoritos**  
Muestra la lista de paises que el usuario fue guardando durante la navegacion.  
Desde esta pantalla se puede:
- Ver los países favoritos.
- Eliminar países de la lista.
- Enviar todos los favoritos a la base de datos para ser guardados localmente, mediante una petición POST a la API.

<img width="1000" height="500" alt="image" src="https://github.com/user-attachments/assets/efe13f4f-e09c-4a82-a806-2b80a7badb13" />

<img width="1000" height="500" alt="image" src="https://github.com/user-attachments/assets/4cb4a810-999b-4279-91d9-20ca88c4d5c4" />

<img width="1000" height="500" alt="image" src="https://github.com/user-attachments/assets/4010e2da-666d-4234-94da-ccbac77ff648" />

<img width="1000" height="500" alt="image" src="https://github.com/user-attachments/assets/13055b96-cf98-4461-87fa-fef358b94481" />

---

**Nota**: El proyecto fue hecho con Ef Core. Si se descarga el repositorio de GitHub hay que ejecutar el comando Update-Database en la consola del administrador de paquetes
para crear la base de datos local. De lo contrario la aplicacion mostrara error al intentar guardar los registros elegidos en Favoritos al clickear el boton Guardar .




