using UnityEngine;
using TMPro;

public class PuntajeManager : MonoBehaviour
{
    public int puntajeActual = 10; 
    public TextMeshProUGUI textoPuntaje; 

    void Start()
    {
        ActualizarUI();
    }

    // Esta es la función que el SistemaAlertas va a buscar
    public void RegistrarInfraccion(int cantidad) 
    {
        puntajeActual -= cantidad;
        if (puntajeActual < 0) puntajeActual = 0;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = puntajeActual.ToString();
    }
}