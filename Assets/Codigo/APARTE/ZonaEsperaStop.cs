using UnityEngine;

public class ZonaEsperaStop : MonoBehaviour
{
    public float tiempoEspera = 6f;

    private Rigidbody rbCarro;
    private bool carroEnZona = false;
    private float tiempoDetenido = 0f;
    private int ultimoSegundoMostrado = -1;
    private VisualizadorAlertas visualizador;

    void OnEnable()
    {
        carroEnZona          = false;
        rbCarro              = null;
        tiempoDetenido       = 0f;
        ultimoSegundoMostrado = -1;
        visualizador         = FindObjectOfType<VisualizadorAlertas>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb == null) return;
        rbCarro        = rb;
        carroEnZona    = true;
        tiempoDetenido = 0f;
        ultimoSegundoMostrado = -1;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        carroEnZona    = false;
        rbCarro        = null;
        tiempoDetenido = 0f;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!carroEnZona || rbCarro == null) return;

        if (rbCarro.velocity.magnitude < 0.1f)
        {
            tiempoDetenido += Time.deltaTime;

            int segundosRestantes = Mathf.CeilToInt(tiempoEspera - tiempoDetenido);
            segundosRestantes = Mathf.Max(segundosRestantes, 0);

            if (segundosRestantes != ultimoSegundoMostrado)
            {
                ultimoSegundoMostrado = segundosRestantes;
                if (visualizador != null)
                    visualizador.MostrarMensaje("Espera: " + segundosRestantes + "s");
            }

            if (tiempoDetenido >= tiempoEspera)
                gameObject.SetActive(false);
        }
        else
        {
            tiempoDetenido        = 0f;
            ultimoSegundoMostrado = -1;
        }
    }
}
