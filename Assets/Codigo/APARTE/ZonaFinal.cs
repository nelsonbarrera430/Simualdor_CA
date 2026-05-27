using UnityEngine;

public class ZonaFinal : MonoBehaviour
{
    public float tiempoEspera = 6f;
    public PanelFinal panelFinalRef;

    private Rigidbody rbCarro;
    private bool carroEnZona = false;
    private bool yaActivado = false;
    private float tiempoDetenido = 0f;
    private int ultimoSegundoMostrado = -1;
    private PanelFinal panelFinal;
    private VisualizadorAlertas visualizador;

    void OnEnable()
    {
        yaActivado = false;
        carroEnZona = false;
        rbCarro = null;
        tiempoDetenido = 0f;
        ultimoSegundoMostrado = -1;
        if (panelFinalRef != null) panelFinal = panelFinalRef;
        else panelFinal = FindObjectOfType<PanelFinal>();
        visualizador = FindObjectOfType<VisualizadorAlertas>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb == null) return;
        rbCarro = rb;
        carroEnZona = true;
        tiempoDetenido = 0f;
        ultimoSegundoMostrado = -1;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        carroEnZona = false;
        rbCarro = null;
        tiempoDetenido = 0f;
    }

    void Update()
    {
        if (!carroEnZona || yaActivado || rbCarro == null) return;

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
            {
                yaActivado = true;
                if (panelFinal != null) panelFinal.Mostrar();
            }
        }
        else
        {
            tiempoDetenido = 0f;
            ultimoSegundoMostrado = -1;
        }
    }
}
