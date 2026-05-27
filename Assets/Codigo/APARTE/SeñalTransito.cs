using UnityEngine;

public class SeñalTransito : MonoBehaviour
{
    public enum TipoZona { Activacion, BarreraStop, BarreraSiga }

    [Header("Tipo de zona")]
    public TipoZona tipoZona;

    [Header("Solo en Activacion")]
    public GameObject señalStop;
    public GameObject señalSiga;
    [Range(0f, 1f)]
    public float probabilidadStop = 0.5f;

    [Header("Penalizacion")]
    public int puntosInfraccion = 5;

    private static bool señalActiva = false;
    private static bool esStopActivo = false;
    private static bool yaDisparado = false;
    private static SeñalTransito instanciaActivacion;

    private SistemaAlertas sistemaAlertas;
    private PuntajeManager puntajeManager;

    void Start()
    {
        sistemaAlertas = FindObjectOfType<SistemaAlertas>();
        puntajeManager = FindObjectOfType<PuntajeManager>();

        if (tipoZona == TipoZona.Activacion)
        {
            instanciaActivacion = this;
            if (señalStop != null) señalStop.SetActive(false);
            if (señalSiga != null) señalSiga.SetActive(false);
            señalActiva = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Rigidbody>() == null) return;

        switch (tipoZona)
        {
            case TipoZona.Activacion:
                esStopActivo = Random.value < probabilidadStop;
                señalActiva  = true;
                yaDisparado  = false;
                if (señalStop != null) señalStop.SetActive(esStopActivo);
                if (señalSiga != null) señalSiga.SetActive(!esStopActivo);
                break;

            case TipoZona.BarreraStop:
                if (!señalActiva || !esStopActivo || yaDisparado) return;
                yaDisparado = true;
                AplicarPenalizacion("!NO TE DETUVISTE EN EL RETEN!");
                ResetearSeñales();
                break;

            case TipoZona.BarreraSiga:
                if (!señalActiva || esStopActivo || yaDisparado) return;
                yaDisparado = true;
                AplicarPenalizacion("!NO HABIA QUE DETENERSE!");
                ResetearSeñales();
                break;
        }
    }

    void ResetearSeñales()
    {
        señalActiva = false;
        if (instanciaActivacion == null) return;
        if (instanciaActivacion.señalStop != null) instanciaActivacion.señalStop.SetActive(false);
        if (instanciaActivacion.señalSiga != null) instanciaActivacion.señalSiga.SetActive(false);
    }

    void AplicarPenalizacion(string mensaje)
    {
        if (sistemaAlertas != null)
        {
            sistemaAlertas.MostrarInfraccion(mensaje, puntosInfraccion);
            return;
        }
        if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosInfraccion);
    }
}
