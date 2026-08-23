namespace ChapinRescue;
//Celda para posicion individual de la malla de Ciudad
public class Celda
{
    public int Fila;
    public int Columna;
    public char Tipo;
    public int Capacidad;
    public bool EnCamino;
    
    //Atributos para busqueda de caminos
    public bool Visitada;

    public Celda CeldaAnterior;
    
    //Constructor
    public Celda(int fila, int columna, char tipo)
    {
        Fila = fila;
        Columna = columna;
        Tipo = tipo;
        Capacidad = 0; //Se actualizara despues si es unidad militar
        Visitada = false;
        CeldaAnterior = null;
        EnCamino = false;
    }


}