namespace ChapinRescue;

public class Mision
{
    //Determinacion de si una celda se puede pasar en un rescate
    private bool EsTransitableParaRescate(char tipoCelda)
    {
        return tipoCelda == 'E' || tipoCelda == ' ' || tipoCelda == 'C';
    }
    
    //Busqueda de camino
    public Celda BuscarCaminoRescate(Ciudad ciudad, Celda entrada, Celda civil)
    {
        ReiniciarVisitadas(ciudad);

        ListaEnlazada<Celda> cola = new ListaEnlazada<Celda>();
        entrada.Visitada = true;
        cola.Agregar(entrada);

        Nodo<Celda> nodoActual = cola.ObtenerPrimero();

        while (nodoActual != null)
        {
            Celda actual = nodoActual.Dato;

            if (actual.Fila == civil.Fila && actual.Columna == civil.Columna)
            {
                return actual;
            }
            
            //Revision en 4 direcciones
            ExplorarVecino(ciudad, actual, actual.Fila - 1, actual.Columna, cola);
            ExplorarVecino(ciudad, actual, actual.Fila + 1, actual.Columna, cola);
            ExplorarVecino(ciudad, actual, actual.Fila, actual.Columna - 1, cola);
            ExplorarVecino(ciudad, actual, actual.Fila, actual.Columna + 1, cola);
            
            nodoActual = nodoActual.Siguiente;
        }

        return null;
    }
    
    //Revision de celdas vecinas
    private void ExplorarVecino(Ciudad ciudad, Celda actual, int filaVecino, int columnaVecino, ListaEnlazada<Celda> cola)
    {
        if (filaVecino < 1 || filaVecino > ciudad.Filas || columnaVecino < 1 || columnaVecino > ciudad.Columnas)
        {
            return;
        }

        Celda vecino = ciudad.ObtenerCelda(filaVecino, columnaVecino);
        
        if (vecino != null && !vecino.Visitada && EsTransitableParaRescate(vecino.Tipo))
        {
            vecino.Visitada = true;
            vecino.CeldaAnterior = actual;
            cola.Agregar(vecino);
        }
    }
    
    //Limpieza de celdas visitadas
    private void ReiniciarVisitadas(Ciudad ciudad)
    {
        Nodo<ListaEnlazada<Celda>> nodoFila = ciudad.Malla.ObtenerPrimero();

        while (nodoFila !=null)
        {
            Nodo<Celda> nodoCelda = nodoFila.Dato.ObtenerPrimero();
            while (nodoCelda != null)
            {
                nodoCelda.Dato.Visitada = false;
                nodoCelda.Dato.CeldaAnterior = null;
                nodoCelda = nodoCelda.Siguiente;
            }
            nodoFila = nodoFila.Siguiente;
        }
    }
    
    //Determinar si se puede pasar en una celda en una mision de extraccion
    private bool EsTransitableParaExtraccion(char tipoCelda)
    {
        return tipoCelda == 'E' || tipoCelda == ' ' || tipoCelda == 'R';
    }
    
    //Busqueda de Recursos
    public Celda BuscarCaminoExtraccion(Ciudad ciudad, Celda entrada, Celda recurso, ChapinFighter robot)
    {
        ReiniciarVisitadas(ciudad);
        
        ListaEnlazada<Celda> cola = new ListaEnlazada<Celda>();
        entrada.Visitada = true;
        cola.Agregar(entrada);

        Nodo<Celda> nodoActual = cola.ObtenerPrimero();

        while (nodoActual !=null)
        {
            Celda actual = nodoActual.Dato;

            if (actual.Fila == recurso.Fila && actual.Columna == recurso.Columna)
            {
                return actual;
            }
            
            ExplorarVecinoExtraccion(ciudad, actual, actual.Fila - 1, actual.Columna, cola, robot);
            ExplorarVecinoExtraccion(ciudad, actual, actual.Fila + 1, actual.Columna, cola, robot);
            ExplorarVecinoExtraccion(ciudad, actual, actual.Fila, actual.Columna - 1, cola, robot);
            ExplorarVecinoExtraccion(ciudad, actual, actual.Fila, actual.Columna + 1, cola, robot);
            
            nodoActual = nodoActual.Siguiente;
        }

        return null;
    }
    
    //Revision de celdas vecinas para extraccion
    private void ExplorarVecinoExtraccion(Ciudad ciudad, Celda actual, int filaVecino, int columnaVecino,
        ListaEnlazada<Celda> cola, ChapinFighter robot)
    {
        if (filaVecino < 1 || filaVecino > ciudad.Filas || columnaVecino < 1 || columnaVecino > ciudad.Columnas)
        {
            return;
        }

        Celda vecino = ciudad.ObtenerCelda(filaVecino, columnaVecino);
        
        if (vecino == null || vecino.Visitada)
        {
            return;
        }

        if (vecino.Tipo == 'M')
        {
            if (robot.Capacidad > vecino.Capacidad)
            {
                vecino.Visitada = true;
                vecino.CeldaAnterior = actual;
                cola.Agregar(vecino);
            }
        }
        else if (EsTransitableParaExtraccion(vecino.Tipo))
        {
            vecino.Visitada = true;
            vecino.CeldaAnterior = actual;
            cola.Agregar(vecino);
        }
    }
    
    //Recorre el camino encontrado para restar la capacidad
    public void AplicarCostoCombate(Celda destino, ChapinFighter robot)
    {
        Celda actual = destino;
        while (actual != null)
        {
            if (actual.Tipo == 'M')
            {
                robot.Capacidad -= actual.Capacidad;
            }
            actual = actual.CeldaAnterior;
        }
    }
}