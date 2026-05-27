using UnityEngine;
using TMPro; // Si usas TextMeshPro
using System.Collections;

namespace MisAlertas // Esto es opcional, pero ayuda al orden
{
    public class MostrarAlerta : MonoBehaviour
    {
        // TODO el código debe ir aquí adentro
        public TextMeshProUGUI textoAlerta;
        public GameObject panelFondo;
        public float tiempoVisible = 3f;

        void Start()
        {
            if (textoAlerta != null) textoAlerta.text = "";
            if (panelFondo != null) panelFondo.SetActive(false);
        }

        public void ActivarAlerta(string mensaje)
        {
            StopAllCoroutines();
            StartCoroutine(RutinaAlerta(mensaje));
        }

        IEnumerator RutinaAlerta(string mensaje)
        {
            textoAlerta.text = mensaje;
            if (panelFondo) panelFondo.SetActive(true);

            yield return new WaitForSeconds(tiempoVisible);

            textoAlerta.text = "";
            if (panelFondo) panelFondo.SetActive(false);
        }
    }
}