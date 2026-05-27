using UnityEngine;

public class BarreraStop : MonoBehaviour
{
    public GameObject señalStop;
    public int puntosInfraccion = 5;

    private SistemaAlertas sistemaAlertas;
    private PuntajeManager puntajeManager;

    void Start()
    {
        sistemaAlertas = FindObjectOfType<SistemaAlertas>();
        puntajeManager = FindObjectOfType<PuntajeManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (señalStop == null || !señalStop.activeSelf) return;

        if (sistemaAlertas != null)
            sistemaAlertas.MostrarInfraccion("!NO TE DETUVISTE EN EL RETEN!", puntosInfraccion);
        else if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosInfraccion);

    }
}
