namespace ChapinRescue;
//Lista enlazada generica, plantilla para la lista enlazada
public class ListaEnlazada<T>
{
    private Nodo<T> primero; //Primero de la lista o null si esta vacia
    
    //Constructor
    public ListaEnlazada()
    {
        primero = null;
    }
    
    //Agregar un nuevo dato al final
    public void Agregar(T dato)
    {
        Nodo<T> nuevo = new Nodo<T>(dato);
        if (primero == null)
        {
            primero = nuevo;
        }
        else
        {
            Nodo<T> actual = primero;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }
    
    //Devolver el primer nodo de la lista para recorerla desde afuera
    public Nodo<T> ObtenerPrimero()
    {
        return primero;
    }
    
    //Contar cantidad de elementos de la lista
    public int Contar()
    {
        int contador = 0;
        Nodo<T> actual = primero;
        while (actual != null)
        {
            contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
}