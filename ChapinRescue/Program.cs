using ChapinRescue;

LectorXML lector = new LectorXML();
ListaEnlazada<Ciudad> ciudades = lector.CargarCiudades("xml/prueba1.xml");

Nodo<Ciudad> actual = ciudades.ObtenerPrimero();
while (actual != null)
{
    Console.WriteLine("Nombre: " + actual.Dato.Nombre);
    Console.WriteLine("Filas: " + actual.Dato.Filas);
    Console.WriteLine("Columnas: " + actual.Dato.Columnas);
    
    //Prueba de celdas especificas
    Celda celda1 = actual.Dato.ObtenerCelda(1, 1);
    Console.WriteLine("Celda (1,1) tipo: "+ celda1.Tipo);
    
    Celda celda2 = actual.Dato.ObtenerCelda(3, 2);
    Console.WriteLine("Celda (3,2) tipo: "+ celda2.Tipo);
    
    //Prueba de celda militar
    Celda celdaMilitar = actual.Dato.ObtenerCelda(2, 2);
    Console.WriteLine("Celda (2,2) tipo: "+ celdaMilitar.Tipo + ", capacidad: " + celdaMilitar.Capacidad);

    actual = actual.Siguiente;
}
