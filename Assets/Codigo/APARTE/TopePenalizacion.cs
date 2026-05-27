using UnityEngine;

// =============================================================================
// TopePenalizacion.cs
// Adjuntar al GameObject del tope/reductor de velocidad.
// Requisitos del tope en Unity:
//   - MeshCollider con isTrigger = FALSE (colisión física real)
//   - IMPORTANTE: asigna el tag "Road" al tope para evitar que SistemaAlertas
//     lo cuente también como "CHOQUE DETECTADO" y reste puntos dos veces.
// =============================================================================

public class TopePenalizacion : MonoBehaviour
{
    [Header("Configuración de Penalización")]
    // Puntos a restar si el jugador se pasa del límite. Ajustable desde el Inspector.
    public int puntosA_Restar = 5;

    [Header("Límites de Velocidad (km/h)")]
    // Más de este valor → infracción
    public float velocidadMaxima = 30f;
    // Menos de este valor → paso correcto
    public float velocidadCorrecta = 20f;

    [Header("Anti-Spam")]
    // Segundos de espera antes de poder penalizar de nuevo con este mismo tope
    public float tiempoEntrePenalizaciones = 3f;

    // =========================================================================
    // CONEXIÓN CON EL SISTEMA GLOBAL DE PUNTAJE
    // Este script busca PuntajeManager y SistemaAlertas automáticamente en Start().
    // Si en el futuro cambias el sistema de puntaje por otro script, reemplaza
    // PuntajeManager por tu nuevo script en la variable de abajo y en AplicarPenalizacion().
    // =========================================================================
    private PuntajeManager puntajeManager;
    private SistemaAlertas sistemaAlertas;

    private float tiempoUltimaPenalizacion = -10f;

    void Start()
    {
        puntajeManager = FindObjectOfType<PuntajeManager>();
        sistemaAlertas  = FindObjectOfType<SistemaAlertas>();

        if (puntajeManager == null)
            Debug.LogWarning("[TopePenalizacion] No se encontró PuntajeManager en la escena.");
        if (sistemaAlertas == null)
            Debug.LogWarning("[TopePenalizacion] No se encontró SistemaAlertas en la escena.");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sube por la jerarquía del objeto colisionado buscando el Rigidbody del carro
        Rigidbody rbCarro = collision.collider.GetComponentInParent<Rigidbody>();
        if (rbCarro == null) return;

        float velocidadKmh = rbCarro.velocity.magnitude * 3.6f;

        if (velocidadKmh > velocidadMaxima)
        {
            // --- INFRACCIÓN: se pasó el tope a alta velocidad ---
            Debug.Log($"[Tope] Velocidad al impactar: {velocidadKmh:F1} km/h");
            Debug.Log("¡Te tragaste un tope! Redúcela velocidad");

            // Cooldown: evita restar puntos múltiples veces por un mismo golpe
            if (Time.time >= tiempoUltimaPenalizacion + tiempoEntrePenalizaciones)
            {
                tiempoUltimaPenalizacion = Time.time;
                AplicarPenalizacion();
            }
        }
        else if (velocidadKmh < velocidadCorrecta)
        {
            // --- CORRECTO: pasó despacio ---
            Debug.Log($"[Tope] Velocidad al impactar: {velocidadKmh:F1} km/h");
            Debug.Log("Pasaste el tope correctamente");
        }
        // Entre velocidadCorrecta y velocidadMaxima: zona de precaución, sin acción.
    }

    void AplicarPenalizacion()
    {
        // =====================================================================
        // PUNTO DE CONEXIÓN CON EL SISTEMA GLOBAL DE PUNTAJE
        // Opción 1 (recomendada): llama a SistemaAlertas, que muestra la alerta
        // en pantalla Y descuenta los puntos a través de PuntajeManager.
        // Opción 2 (fallback): va directo a PuntajeManager si no hay SistemaAlertas.
        // =====================================================================

        if (sistemaAlertas != null)
        {
            sistemaAlertas.MostrarInfraccion("¡TE TRAGASTE UN TOPE!", puntosA_Restar);
            return;
        }

        // Fallback directo al gestor de puntaje
        if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosA_Restar);
    }
}
