using UnityEngine;
using TMPro; // Necesitas tener TextMeshPro instalado
using System.Collections;

public class VisualizadorAlertas : MonoBehaviour
{
    public TextMeshProUGUI textoAlerta; // Arrastra aquí el texto "Texto_Dentro_Imagen"
    public GameObject panelCompleto;      // Arrastra aquí el objeto "Imagen_Alerta"
    public float tiempoVisible = 3f;

    void Start()
    {
        // Empezamos con la alerta oculta
        if(panelCompleto != null) panelCompleto.SetActive(false);
    }

    public void MostrarMensaje(string mensaje)
    {
        // Si el panel no estaba activo, lo activamos
        if(panelCompleto != null && !panelCompleto.activeSelf) 
        {
            panelCompleto.SetActive(true);
        }
        
        StopAllCoroutines(); // Por si sale otra alerta rápido, que no se borre antes
        StartCoroutine(RutinaAlerta(mensaje));
    }

    IEnumerator RutinaAlerta(string mensaje)
    {
        textoAlerta.text = mensaje;
        
        yield return new WaitForSeconds(tiempoVisible);

        // Apagamos la alerta después del tiempo
        if(panelCompleto != null) panelCompleto.SetActive(false);
    }
}