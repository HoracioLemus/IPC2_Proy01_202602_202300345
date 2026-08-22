namespace ChapinRescue;
//Robot especializado en combate
public class ChapinFighter : Robot
{
    public int Capacidad;
    //Constructor
    public ChapinFighter(string nombre, int capacidad) : base(nombre)
    {
        Capacidad = capacidad;
    }
}