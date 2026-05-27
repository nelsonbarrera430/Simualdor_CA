using UnityEngine;

public class Senal60 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        SistemaAlertas sistema = other.GetComponentInParent<SistemaAlertas>();
        if (sistema != null)
        {
            sistema.limiteVelocidad = 60f;
            Debug.Log("Límite cambiado a 60 km/h");
        }
    }
}
