using UnityEngine;

public class BarreraSiga : MonoBehaviour
{
    public GameObject señalSiga;
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
        if (señalSiga == null || !señalSiga.activeSelf) return;

        if (sistemaAlertas != null)
            sistemaAlertas.MostrarInfraccion("!NO HABIA QUE DETENERSE!", puntosInfraccion);
        else if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosInfraccion);

    }
}
