using UnityEngine;

// =============================================================================
// DistanciaSegura.cs
// Adjuntar al GameObject de la ZONA DE ENTRADA (BoxCollider con isTrigger = TRUE).
//
// Configuración en Unity Inspector:
//   1. Crea un GameObject vacío en la escena → agrégale BoxCollider → marca isTrigger.
//      Ese es tu "trigger de entrada". Ponle este script.
//   2. Arrastra en "Objeto Activable" el Game Object vacío que está frente al carro.
//   3. PuntajeManager y SistemaAlertas se buscan automáticamente (no hay que arrastrar).
//   4. Crea otro GameObject vacío con BoxCollider (isTrigger) → ese es el "trigger de
//      salida" → ponle el script DesactivadorDistancia.cs y arrastra este GameObject
//      en el campo "Distancia Segura" de ese script.
// =============================================================================

public class DistanciaSegura : MonoBehaviour
{
    [Header("Objeto a activar / desactivar")]
    [Tooltip("Game Object vacío que está en el carro (el sensor de enfrente). Se activa al entrar y se desactiva al salir.")]
    [SerializeField] private GameObject objetoActivable;

    void Start()
    {
        // Aseguramos que el sensor empiece desactivado
        if (objetoActivable != null)
            objetoActivable.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Solo reacciona al carro del jugador (busca Rigidbody del carro)
        Rigidbody rbCarro = other.GetComponentInParent<Rigidbody>();
        if (rbCarro == null) return;

        // Solo activa el sensor; la penalización la maneja SensorCercania.cs
        if (objetoActivable != null)
            objetoActivable.SetActive(true);
    }

    // Llamado por DesactivadorDistancia cuando el carro sale por la zona de salida
    public void Desactivar()
    {
        if (objetoActivable != null)
            objetoActivable.SetActive(false);
    }
}
