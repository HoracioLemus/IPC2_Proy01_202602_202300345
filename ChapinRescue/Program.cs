using ChapinRescue;

LectorXML lector = new LectorXML();
ListaEnlazada<Ciudad> ciudades = lector.CargarCiudades("xml/prueba1.xml");
Ciudad ciudad = ciudades.ObtenerPrimero().Dato;

Mision mision = new Mision();

// Prueba de misión de rescate
Celda entrada = ciudad.ObtenerCelda(1, 1);
Celda civil = ciudad.ObtenerCelda(3, 3);

Celda resultadoRescate = mision.BuscarCaminoRescate(ciudad, entrada, civil);

if (resultadoRescate == null)
{
    Console.WriteLine("Rescate: Mision Imposible");
}
else
{
    Console.WriteLine("Rescate: camino encontrado");
    Celda actual = resultadoRescate;
    while (actual != null)
    {
        Console.WriteLine("(" + actual.Fila + "," + actual.Columna + ") tipo: " + actual.Tipo);
        actual = actual.CeldaAnterior;
    }
}

//Prueba de misión de extracción 
ListaEnlazada<Robot> robots = lector.CargarRobots("xml/prueba1.xml");
ChapinFighter robotFighter = null;

Nodo<Robot> nodoRobot = robots.ObtenerPrimero();
while (nodoRobot != null)
{
    if (nodoRobot.Dato is ChapinFighter fighter)
    {
        robotFighter = fighter;
    }
    nodoRobot = nodoRobot.Siguiente;
}

Celda recurso = ciudad.ObtenerCelda(1, 3);

Celda resultadoExtraccion = mision.BuscarCaminoExtraccion(ciudad, entrada, recurso, robotFighter);

if (resultadoExtraccion == null)
{
    Console.WriteLine("Extraccion: Mision Imposible");
}
else
{
    mision.AplicarCostoCombate(resultadoExtraccion, robotFighter);
    Console.WriteLine("Extraccion: camino encontrado, capacidad final: " + robotFighter.Capacidad);
}