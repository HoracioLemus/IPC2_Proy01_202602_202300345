namespace ChapinRescue;
//Nodo generico, plantilla para la lista enlazada
//Guardara un dato de cualquier tipo y una referencia al siguiente nodo
public class Nodo<T>
{
   public T Dato;
   public Nodo<T> Siguiente;
   
   //Constructor
   public Nodo(T dato)
   {
       Dato = dato;
       Siguiente = null;
   }
}