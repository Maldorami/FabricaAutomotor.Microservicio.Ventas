# Microservicio FabricaAutomotor.Microservicio.Ventas

Microservicio creado por Ramiro Maldonado para evaluación técnica.

# Cómo utilizar el Microservicio

Abriendo la solución en VS (recomiendo versión 2019) y ejecutando el proyecto de Ventas.API será suficiente para levantar el servicio.

El microservicio tiene expuesta una interfaz Swagger para facilitar su uso.

La inserción de las ventas se hace a través del endpoint InsertSale.
El cuerpo del request deberá ser similar a:
```
{
  "storeID": 0,
  "itemID": 0
}
```

Los otros endpoints que se podrán utilizar son:
```
GET/GetTotalSalesCount
GET/GetTotalSalesCountFromStore/{storeID}
GET/GetItemSalePercentageByStore
```

Además, para facilitar pruebas (en caso de que el usuario lo necesite), se deja comentado una función que ejecuta la inserción automática y random de X cantidad de ventas propuestas por quien ejecute la acción.

Como extra podrán utilizar el proyecto de test para confirmar funcionalidad.

#Log
Logs informacionales o de errores se encuentran configurados en:
```
Ventas.API/log4net.config
...
C:\\logfile\\FabricaAutomotor.Microservicio.Ventas.log"
```

En caso de querer cambiar el archivo de salida, símplemente cambie la ruta y/o nombre del archivo. 

# Objetivos y condiciones para el microservicio
Realizar los siguientes puntos usando como lenguaje .NET o NET CORE, no tiene límite de tiempo ni limitaciones de uso de frameworks.

1- Enviar la solución en un repo git (github o gitlab) con los commits con el detalle de lo que se está subiendo.

2- Crear uno o varios servicios rest que expongan la solución al problema planteado.

3- Comentar en código lo que se considere necesario para explicar cómo se hizo la solución.

4- Imprimir el tiempo de ejecución de cada método.

5- Mockear los datos

6- No es necesario realizar cruds de las entidades en la api

7- Se tiene en cuenta la profundidad de la solución brindada y profesionalidad de código.

8- NO REQUIERE FRONTEND

9- Agregar explicación de cómo deben usarse los servicios.

 

Problema:

Una fábrica de automóviles produce 4 modelos de coches (sedan, suv, offroad, sport) cuyos precios de venta son: 8.000 u$s, 9.500 u$s, 12.500 u$s y 18.200 u$s.

La empresa tiene 4 centros de distribución y venta. Se tiene una relación de datos correspondientes al tipo de vehículo vendido y punto de distribución en el que se produjo la venta del mismo.

El tipo “sport” incluye un impuesto extra del 7% que se debe adicionar al precio en la venta.

Realizar una api rest que contemple:

•            Insertar una venta

•            Obtener el volumen de ventas total.

•            Obtener el volumen de ventas por centro.

•            Obtener el porcentaje de unidades de cada modelo vendido en cada centro sobre el total de ventas de la empresa.

 
# Mock de datos
La decición del mock de datos y las funcionalidades de obtención de los datos, está basada en que teniendo en cuenta el objetivo del microservicio, la mayoría de la lógica podría plantearse directamente en functions y stored procedures de cualquier motor de BBDD.

Por cuestiones de complejidad se decidió simular ese entorno de la manera presentada.

