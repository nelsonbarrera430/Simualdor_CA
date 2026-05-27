using UnityEngine;

public class Senal90 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        SistemaAlertas sistema = other.GetComponentInParent<SistemaAlertas>();
        if (sistema != null)
        {
            sistema.limiteVelocidad = 90f;
            Debug.Log("Límite cambiado a 90 km/h");
        }
    }
}
