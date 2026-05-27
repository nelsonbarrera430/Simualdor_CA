using UnityEngine;

public class ZonaNoAdelantar : MonoBehaviour
{
    [Header("Configuración de Penalización")]
    public int puntosDePenalizacion = 10; // Puntos que resta según tu sistema

    private void OnTriggerEnter(Collider other)
    {
        // Buscamos el sistema de alertas en el coche que cruzó la línea
        SistemaAlertas sistema = other.GetComponent<SistemaAlertas>();

        // Si no está directamente, buscamos en los padres del objeto
        if (sistema == null) sistema = other.GetComponentInParent<SistemaAlertas>();

        if (sistema != null)
        {
            // Llamamos a tu sistema de alertas con el mensaje exacto que pediste
            sistema.MostrarInfraccion("¡INFRACCIÓN! ESTÁS CRUZANDO AL CARRIL CONTRARIO", puntosDePenalizacion);
            
            Debug.Log("Infracción detectada: Adelantamiento en zona prohibida.");
        }
    }
}