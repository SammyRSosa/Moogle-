# Moogle!

![](moogle.png)

> Proyecto de Programación I.
> Facultad de Matemática y Computación - Universidad de La Habana.
> Cursos 2021, 2022.

Moogle! es una aplicación *totalmente original* cuyo propósito es buscar inteligentemente un texto en un conjunto de documentos.

Manual de Usuario : Moogle!

Este motor de busqueda funciona simple pero eficientemente devolviendo para una entrada elegida por ti escogiendo en un contenido de documentos los que mas se asemejen a tu consulta.Estos resultados contaran con el titulo de los mismos y ademas el trozo del texto q mediante los mismos parametros que encontramos el documentos se asemeja mas a tu entrada.Podemos asegurarte la funcionalidad del codigo asegurandote que usamos probadas y aceptadas formulas para ayudarnos a escoger tu respuesta asi como el calculo del tfidf, la distancia de Levenshtein y la similitud del coseno.

Para mejorar tu busqueda sientete libre de usar operadores que haran mas especificas las respuesta,nuestro codigo soporta los siguientes:

!palabra: usalo solo si deseas que esta palabra no aparezca en ningun documento de la respuesta

*palabra: usalo n veces delante de una palabra para aumentar la importancia de la palabra a la hora de la busqueda

^palabra: en el caso que la palabra sea tan importante q el documento solo es relevante si lo tiene utiliza este operador

Si la busqueda no devuelve resultados o no son satisfactorios sientete libre de revisar la consulta como un consejo para modificar tu entrada
