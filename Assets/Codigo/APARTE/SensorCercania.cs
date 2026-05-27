using UnityEngine;

// =============================================================================
// SensorCercania.cs
// Adjuntar al Game Object vacío que está FRENTE AL CARRO del jugador.
// Ese objeto debe tener BoxCollider con isTrigger = TRUE.
//
// El objeto empieza desactivado. DistanciaSegura.cs lo activa cuando el jugador
// entra en la zona de peligro, y DesactivadorDistancia.cs lo apaga al salir.
//
// Cuando el sensor está activo y detecta un carro con tag "Car" → alerta + -5 pts.
// =============================================================================

public class SensorCercania : MonoBehaviour
{
    [Header("Penalización")]
    public int puntosARestar = 5;

    [Header("Anti-spam")]
    public float tiempoEntrePenalizaciones = 3f;

    private PuntajeManager puntajeManager;
    private SistemaAlertas sistemaAlertas;
    private float tiempoUltimaPenalizacion = -10f;

    void Start()
    {
        puntajeManager = FindObjectOfType<PuntajeManager>();
        sistemaAlertas  = FindObjectOfType<SistemaAlertas>();

        if (puntajeManager == null)
            Debug.LogWarning("[SensorCercania] No se encontró PuntajeManager en la escena.");
        if (sistemaAlertas == null)
            Debug.LogWarning("[SensorCercania] No se encontró SistemaAlertas en la escena.");
    }

    void OnTriggerEnter(Collider other)
    {
        // Solo reacciona a objetos con tag "Car"
        if (!other.CompareTag("Car")) return;

        if (Time.time >= tiempoUltimaPenalizacion + tiempoEntrePenalizaciones)
        {
            tiempoUltimaPenalizacion = Time.time;
            AplicarPenalizacion();
        }
    }

    private void AplicarPenalizacion()
    {
        if (sistemaAlertas != null)
        {
            sistemaAlertas.MostrarInfraccion("¡Estás muy cerca!", puntosARestar);
            return;
        }

        if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosARestar);
    }
}
