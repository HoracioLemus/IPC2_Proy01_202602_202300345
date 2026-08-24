using System.Text;
using ChapinRescue;

public class GeneradorGraphviz
{
    public string GenerarDot(Ciudad ciudad, Celda destino)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("digraph Mision {");
        sb.AppendLine("splines=false;");
        sb.AppendLine("node [shape=box style=filled label=\"\" width=0.6 height=0.6];");

        MarcarCaminoEnTexto(destino);

        Nodo<ListaEnlazada<Celda>> nodoFila = ciudad.Malla.ObtenerPrimero();
        string primerNodoFilaAnterior = null;

        while (nodoFila != null)
        {
            sb.AppendLine("{ rank=same;");

            Nodo<Celda> nodoCelda = nodoFila.Dato.ObtenerPrimero();
            string nodoAnteriorEnFila = null;
            string primerNodoDeEstaFila = null;

            while (nodoCelda != null)
            {
                Celda celda = nodoCelda.Dato;
                string nombreNodo = "c_" + celda.Fila + "_" + celda.Columna;
                string color = ObtenerColor(celda);

                sb.AppendLine(nombreNodo + " [fillcolor=\"" + color + "\"];");

                if (primerNodoDeEstaFila == null)
                {
                    primerNodoDeEstaFila = nombreNodo;
                }

                if (nodoAnteriorEnFila != null)
                {
                    sb.AppendLine(nodoAnteriorEnFila + " -> " + nombreNodo + " [style=invis];");
                }

                nodoAnteriorEnFila = nombreNodo;
                nodoCelda = nodoCelda.Siguiente;
            }

            sb.AppendLine("}");

            if (primerNodoFilaAnterior != null)
            {
                sb.AppendLine(primerNodoFilaAnterior + " -> " + primerNodoDeEstaFila + " [style=invis];");
            }

            primerNodoFilaAnterior = primerNodoDeEstaFila;
            nodoFila = nodoFila.Siguiente;
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    private string ObtenerColor(Celda celda)
    {
        if (celda.EnCamino)
        {
            return "gold";
        }

        switch (celda.Tipo)
        {
            case '*': return "black";
            case 'E': return "green";
            case ' ': return "white";
            case 'C': return "blue";
            case 'R': return "gray";
            case 'M': return "red";
            default: return "white";
        }
    }

    private void MarcarCaminoEnTexto(Celda destino)
    {
        Celda actual = destino;
        while (actual != null)
        {
            actual.EnCamino = true;
            actual = actual.CeldaAnterior;
        }
    }
}