namespace ChapinRescue;
//Contiene el nombre, dimensiones y malla de celdas
public class Ciudad
{
    public string Nombre;
    public int Filas;
    public int Columnas;
    public ListaEnlazada<ListaEnlazada<Celda>> Malla;
    
    //Constructor
    public Ciudad(String nombre, int filas, int columnas)
    {
        Nombre = nombre;
        Filas = filas;
        Columnas = columnas;
        Malla = new ListaEnlazada<ListaEnlazada<Celda>>();
    }
    
    //Agrega una fila ya completa a la malla
    public void AgregarFila(ListaEnlazada<Celda> fila)
    {
        Malla.Agregar(fila);
    }
    
    //Busca y devuelve una celda especifica
    
    public Celda ObtenerCelda(int fila, int columna)
    {
        Nodo<ListaEnlazada<Celda>> nodoFila = Malla.ObtenerPrimero();
        int contadoerFila = 1;
        
        while (nodoFila != null && contadoerFila < fila)
        {
            nodoFila = nodoFila.Siguiente;
            contadoerFila++;
        }
        if (nodoFila == null)
        {
            return null; //Fila no encontrada
        }
        
        Nodo<Celda> nodoColumna = nodoFila.Dato.ObtenerPrimero();
        int contadorColumna = 1;
        
        while (nodoColumna != null && contadorColumna < columna)
        {
            nodoColumna = nodoColumna.Siguiente;
            contadorColumna++;
        }
        if (nodoColumna == null)
        {
            return null; //Columna no encontrada
        }
        
        return nodoColumna.Dato;
    }
}