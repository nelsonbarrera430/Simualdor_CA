using UnityEngine;

public class Senal80 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        SistemaAlertas sistema = other.GetComponentInParent<SistemaAlertas>();
        if (sistema != null)
        {
            sistema.limiteVelocidad = 80f;
            Debug.Log("Límite cambiado a 50 km/h");
        }
    }
}