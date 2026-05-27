using UnityEngine;

// =============================================================================
// DesactivadorDistancia.cs
// Adjuntar al GameObject de la ZONA DE SALIDA (BoxCollider con isTrigger = TRUE).
//
// Configuración en Unity Inspector:
//   1. Crea un GameObject vacío → BoxCollider → isTrigger = TRUE.
//      Colócalo después de la zona de peligro (la "salida").
//   2. Arrastra en "Distancia Segura" el GameObject que tiene el script
//      DistanciaSegura.cs (el trigger de entrada).
// =============================================================================

public class DesactivadorDistancia : MonoBehaviour
{
    [Tooltip("Arrastra aquí el GameObject que tiene el script DistanciaSegura (zona de entrada).")]
    [SerializeField] private DistanciaSegura distanciaSegura;

    void Start()
    {
        if (distanciaSegura == null)
            Debug.LogWarning("[DesactivadorDistancia] No se asignó DistanciaSegura en el Inspector.");
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rbCarro = other.GetComponentInParent<Rigidbody>();
        if (rbCarro == null) return;

        if (distanciaSegura != null)
            distanciaSegura.Desactivar();
    }
}
