using System.Xml;
using ChapinRescue;
namespace ChapinRescue;

public class LectorXML
{
    //Carga de datos del XML
    public ListaEnlazada<Ciudad> CargarCiudades(string rutaArchivo)
    {
        ListaEnlazada<Ciudad> ciudades = new ListaEnlazada<Ciudad>();
        XmlDocument doc = new XmlDocument();
        doc.Load(rutaArchivo);
        
        //Busqueda de nodos ciudad
        XmlNodeList nodosCiudad = doc.GetElementsByTagName("ciudad");
        foreach (XmlNode nodoCiudad in nodosCiudad)
        {
            XmlNode nodoNombre = nodoCiudad["nombre"];
            string nombre = nodoNombre.InnerText;
            int filas = int.Parse(nodoNombre.Attributes["filas"].Value);
            int columnas = int.Parse(nodoNombre.Attributes["columnas"].Value);
            Ciudad ciudad = new Ciudad(nombre, filas, columnas);
            
            
            CargarMalla(nodoCiudad, ciudad);
            ciudades.Agregar(ciudad);
        }
        return ciudades;
    }
    
    //Lee las filas de un nodo y las convierte en la malla de celdas
    private void CargarMalla(XmlNode nodoCiudad, Ciudad ciudad)
    {
        XmlNodeList nodosFila = nodoCiudad.SelectNodes("fila");
        
        foreach (XmlNode nodoFila in nodosFila)
        {
            int numeroFila = int.Parse(nodoFila.Attributes["numero"].Value);
            String contenido = nodoFila.InnerText;
            
            ListaEnlazada<Celda> fila = new ListaEnlazada<Celda>();

            for (int i = 0; i < contenido.Length; i++)
            {
                char caracter = contenido[i];
                int numeroColumna = i + 1;
                Celda celda = new Celda(numeroFila, numeroColumna, caracter);
                fila.Agregar(celda);
            }
            ciudad.AgregarFila(fila);
        }
    }
}