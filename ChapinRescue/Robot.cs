namespace ChapinRescue;
//Clase base para los tipos de robot
public abstract class Robot
{
    public string Nombre;
    
    //Constructor
    public Robot(string nombre)
    {
        Nombre = nombre;
    }
}