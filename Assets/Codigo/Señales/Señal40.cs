using UnityEngine;

public class Senal40 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        SistemaAlertas sistema = other.GetComponentInParent<SistemaAlertas>();
        if (sistema != null)
        {
            sistema.limiteVelocidad = 40f;
            Debug.Log("Límite cambiado a 40 km/h");
        }
    }
}