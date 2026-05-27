using UnityEngine;
using TMPro;
using System.Collections;

public class IndicadorRuta : MonoBehaviour
{
    public TextMeshProUGUI textoAyuda;
    public string mensaje = "Sigue derecho";
    public float tiempoVisible = 3f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (textoAyuda != null) StopAllCoroutines();
        StartCoroutine(Mostrar());
    }

    System.Collections.IEnumerator Mostrar()
    {
        if (textoAyuda != null) textoAyuda.text = mensaje;
        yield return new WaitForSeconds(tiempoVisible);
        if (textoAyuda != null) textoAyuda.text = "";
    }
}
