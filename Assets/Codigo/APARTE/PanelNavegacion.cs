using UnityEngine;
using TMPro;
using System.Collections;

public class PanelNavegacion : MonoBehaviour
{
    public TextMeshProUGUI textoIndicacion;
    public float tiempoVisible = 3f;

    public void MostrarIndicacion(string mensaje)
    {
        StopAllCoroutines();
        StartCoroutine(Rutina(mensaje));
    }

    IEnumerator Rutina(string mensaje)
    {
        if (textoIndicacion != null) textoIndicacion.text = mensaje;
        yield return new WaitForSeconds(tiempoVisible);
        if (textoIndicacion != null) textoIndicacion.text = "";
    }
}
