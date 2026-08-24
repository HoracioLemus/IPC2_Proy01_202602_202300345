using ChapinRescue;

LectorXML lector = new LectorXML();
ListaEnlazada<Ciudad> ciudades = new ListaEnlazada<Ciudad>();
ListaEnlazada<Robot> robots = new ListaEnlazada<Robot>();

Console.WriteLine ("==============Sistema de Control de Robots Chapin Rescue/Fighter==============");
Console.WriteLine("Ingrese la ruta del Archivo de configuracion XML para analisis: ");
string ruta = Console.ReadLine();

ciudades = lector.CargarCiudades(ruta);
robots = lector.CargarRobots(ruta);

Console.WriteLine("Archivo cargado correctamente...");
Console.WriteLine();

//Mostrar ciudades disponibles
Console.WriteLine("Ciudades disponibles: ");
int contador = 1;
Nodo<Ciudad> nodoCiudad = ciudades.ObtenerPrimero();
while (nodoCiudad != null)
{
    Console.WriteLine(contador + "." + nodoCiudad.Dato.Nombre);
    contador++;
    nodoCiudad = nodoCiudad.Siguiente;
}

Console.WriteLine("Seleccione el numero de la Ciudad: ");
int seleccionCiudad = int.Parse(Console.ReadLine());

//Buscar la ciudad seleccionada
Ciudad ciudadElegida = null;
contador = 1;
nodoCiudad = ciudades.ObtenerPrimero();
while (nodoCiudad != null)
{
    if (contador == seleccionCiudad)
    {
        ciudadElegida = nodoCiudad.Dato;
    }

    contador++;
    nodoCiudad = nodoCiudad.Siguiente;
}

Console.WriteLine("Ciudad seleccionada: " + ciudadElegida.Nombre);

Console.WriteLine("Tipos de Mision Disponibles: ");
Console.WriteLine("1. Rescate");
Console.WriteLine("2. Extraccioón de Recuersos");
Console.WriteLine("Seleccione la mision a efectuar");
int TipoMision = int.Parse(Console.ReadLine());

if (TipoMision == 1)
{
    Console.WriteLine("Iniciando Misión de Rescate...");

    //Busqueda de entrada
    ListaEnlazada<Celda> entradas = ciudadElegida.BuscarCeldasPorTipo('E');
    Celda entradaElegida = entradas.ObtenerPrimero().Dato; //Usar primera disponible

    //Busqueda Unidades Civiles
    ListaEnlazada<Celda> civiles = ciudadElegida.BuscarCeldasPorTipo('C');

    if (civiles.Contar() == 0)
    {
        Console.WriteLine("Esta Ciudad no tiene unidades civiles para rescatar.");
    }
    else
    {
        Celda civilElegida;
        if (civiles.Contar() == 1)
        {
            civilElegida = civiles.ObtenerPrimero().Dato;
        }
        else
        {
            Console.WriteLine("Unidades civiles disponibles: ");
            int i = 1;
            Nodo<Celda> nodoCivil = civiles.ObtenerPrimero();
            while (nodoCivil != null)
            {
                Console.WriteLine(i + ". Fila " + nodoCivil.Dato.Fila + ", Columna " + nodoCivil.Dato.Columna);
                i++;
                nodoCivil = nodoCivil.Siguiente;
            }
            Console.WriteLine("Seleccione la Unidad Civil a rescatar: ");
            int seleccionCivil = int.Parse(Console.ReadLine());

            i = 1;
            nodoCivil = civiles.ObtenerPrimero();
            civilElegida = null;
            while (nodoCivil != null)
            {
                if (i == seleccionCivil)
                {
                    civilElegida = nodoCivil.Dato;
                }

                i++;
                nodoCivil = nodoCivil.Siguiente;
            }
        }

        //Robots de rescate Disponibles
        ListaEnlazada<Robot> robotsRescue = new ListaEnlazada<Robot>();
        Nodo<Robot> nodoRobot = robots.ObtenerPrimero();
        while (nodoRobot != null)
        {
            if (nodoRobot.Dato is ChapinRescue.ChapinRescue)
            {
                robotsRescue.Agregar(nodoRobot.Dato);
            }

            nodoRobot = nodoRobot.Siguiente;
        }

        if (robotsRescue.Contar() == 0)
        {
            Console.WriteLine("No hay robots ChapinRescue disponibles.");
        }
        else
        {
            Robot robotElegido;

            if (robotsRescue.Contar() == 1)
            {
                robotElegido = robotsRescue.ObtenerPrimero().Dato;
            }
            else
            {
                Console.WriteLine("Robots ChapinRescue disponibles: ");
                int i = 1;
                Nodo<Robot> nodoR = robotsRescue.ObtenerPrimero();
                while (nodoR != null)
                {
                    Console.WriteLine(i + ". " + nodoR.Dato.Nombre);
                    i++;
                    nodoR = nodoR.Siguiente;
                }
                Console.WriteLine("Seleccione Robot a Utilizar: ");
                int seleccionRobot = int.Parse(Console.ReadLine());

                i = 1;
                nodoR = robotsRescue.ObtenerPrimero();
                robotElegido = null;
                while (nodoR != null)
                {
                    if (i == seleccionRobot)
                    {
                        robotElegido = nodoR.Dato;
                    }
                    i++;
                    nodoR = nodoR.Siguiente;
                }
            }

            //Ejecutar la Mision
            Mision mision = new Mision();
            Celda resultado = mision.BuscarCaminoRescate(ciudadElegida, entradaElegida, civilElegida);

            if (resultado == null)
            {
                Console.WriteLine("Mision Imposible");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Tipo de Mision: Rescate");
                Console.WriteLine("Unidad Civil rescatada: " + civilElegida.Fila + "," + civilElegida.Columna);
                Console.WriteLine("Robot Utilizado: " + robotElegido.Nombre + " (ChapinRescue)");

                GeneradorGraphviz generador = new GeneradorGraphviz();
                string contenidoDot = generador.GenerarDot(ciudadElegida, resultado);
                File.WriteAllText("rescate.dot", contenidoDot);
                Console.WriteLine("Grafico generado: rescate.dot");
            }
        }
    }
}
else if (TipoMision == 2)
{
    Console.WriteLine("Iniciando Misión de Extracción de Recursos...");
    
    //Busqueda de entrada
    ListaEnlazada<Celda> entradasExt = ciudadElegida.BuscarCeldasPorTipo('E');
    Celda entradaElegidaExt = entradasExt.ObtenerPrimero().Dato;
    
    //Busqueda Recursos
    ListaEnlazada<Celda> recursos = ciudadElegida.BuscarCeldasPorTipo('R');

    if (recursos.Contar()==0)
    {
        Console.WriteLine("Esta Ciudad no tiene recursos para extraer.");
    }
    else
    {
        Celda recursoElegido;
        if (recursos.Contar() == 1)
        {
            recursoElegido = recursos.ObtenerPrimero().Dato;
        }
        else
        {
            Console.WriteLine("Recursos Disponibles: ");
            int i = 1;
            Nodo<Celda> nodoRecurso = recursos.ObtenerPrimero();
            while (nodoRecurso != null)
            {
                Console.WriteLine(i+ ".Fila "+ nodoRecurso.Dato.Fila + ", Columna "+ nodoRecurso.Dato.Columna);
                i++;
                nodoRecurso = nodoRecurso.Siguiente;
            }
            Console.WriteLine("Seleccione Recursos para Extraer: ");
            int seleccionRecurso = int.Parse(Console.ReadLine());

            i = 1;
            nodoRecurso = recursos.ObtenerPrimero();
            recursoElegido = null;
            while (nodoRecurso != null)
            {
                if (i == seleccionRecurso)
                {
                    recursoElegido = nodoRecurso.Dato;
                }

                i++;
                nodoRecurso = nodoRecurso.Siguiente;
            }
        }
        
        //Robots ChapinFighter Disponibles
        ListaEnlazada<Robot> robotsFighter = new ListaEnlazada<Robot>();
        Nodo<Robot> nodoRobotF = robots.ObtenerPrimero();
        while (nodoRobotF !=null)
        {
            if (nodoRobotF.Dato is ChapinFighter)
            {
                robotsFighter.Agregar(nodoRobotF.Dato);
            }
            nodoRobotF = nodoRobotF.Siguiente;
        }

        if (robotsFighter.Contar() == 0)
        {
            Console.WriteLine("No hay robots ChapinFighter Disponibles.");
        }
        else
        {
            ChapinFighter robotFighterElegido;

            if (robotsFighter.Contar() == 1)
            {
                robotFighterElegido = (ChapinFighter)robotsFighter.ObtenerPrimero().Dato;
            }
            else
            {
                Console.WriteLine("Robots ChapinFighter Disponibles: ");
                int i = 1;
                Nodo<Robot> nodoRF = robotsFighter.ObtenerPrimero();
                while (nodoRF != null)
                {
                    ChapinFighter f = (ChapinFighter)nodoRF.Dato;
                    Console.WriteLine(i + ". " + f.Nombre + " (Capacidad: " + f.Capacidad + ")");
                    i++;
                    nodoRF = nodoRF.Siguiente;
                }
                Console.WriteLine("Seleccione Robot a Utilizar: ");
                int seleccionRF = int.Parse(Console.ReadLine());

                i = 1;
                nodoRF = robotsFighter.ObtenerPrimero();
                robotFighterElegido = null;
                while (nodoRF != null)
                {
                    if (i == seleccionRF)
                    {
                        robotFighterElegido = (ChapinFighter)nodoRF.Dato;
                    }

                    i++;
                    nodoRF = nodoRF.Siguiente;
                }
            }
            
            //Ejecutar Mision
            Mision misionExt = new Mision();
            Celda resultadoExt = misionExt.BuscarCaminoExtraccion(ciudadElegida, entradaElegidaExt, recursoElegido,
                robotFighterElegido);
            if(resultadoExt == null)
            {
                Console.WriteLine("Mision Imposible");
            }
            else
            {
                misionExt.AplicarCostoCombate(resultadoExt, robotFighterElegido);

                Console.WriteLine();
                Console.WriteLine("Tipo de Mision: Extraccion de Recursos");
                Console.WriteLine("Recurso extraido: "+recursoElegido.Fila + ","+recursoElegido.Columna);
                Console.WriteLine("Robot Utilizado: "+ robotFighterElegido.Nombre + "(ChapinFighter - Capacidad Final: "+ robotFighterElegido.Capacidad + ")");

                GeneradorGraphviz generadorExt = new GeneradorGraphviz();
                string contenidoDotExt = generadorExt.GenerarDot(ciudadElegida, resultadoExt);
                File.WriteAllText("extraccion.dot", contenidoDotExt);
                Console.WriteLine("Grafico generado: extraccion.dot");
            }
        }
    }
}
else
{
    Console.WriteLine("OPCION INVALIDA.");
}