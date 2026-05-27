using UnityEngine;

public class Senal30 : MonoBehaviour
{
    // Solo se ejecuta cuando el carro entra en el Box Collider (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        // Buscamos el script SistemaAlertas en el objeto que entró (el carro)
        // Usamos GetComponentInParent por si el collider está en un hijo del carro
        SistemaAlertas sistema = other.GetComponentInParent<SistemaAlertas>();

        if (sistema != null)
        {
            // Cambiamos la variable QUE ESTÁ EN EL CARRO
            sistema.limiteVelocidad = 30f;
            Debug.Log("Límite de velocidad actualizado a 30 km/h por la señal.");
        }
    }
}