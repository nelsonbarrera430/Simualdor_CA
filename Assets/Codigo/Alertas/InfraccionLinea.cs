using UnityEngine;

public class InfraccionLinea : MonoBehaviour
{
    [Header("Configuración de la Falta")]
    public string mensajeAlerta = "¡INFRACCIÓN! NO PISES LA LÍNEA DE DEMARCACIÓN";
    public int puntosAPenalizar = 5; // Los 5 puntos que me pediste

    [Header("Control de Tiempo")]
    private float tiempoProximaPenalizacion = 0f;
    public float intervaloEspera = 2f; // Espera 2 segundos para no quitar puntos infinitos en un solo roce

    private void OnTriggerStay(Collider other)
    {
        // Buscamos si el objeto que pisó la línea tiene el sistema de alertas
        SistemaAlertas sistema = other.GetComponent<SistemaAlertas>();
        
        if (sistema == null)
        {
            sistema = other.GetComponentInParent<SistemaAlertas>();
        }

        // Si encontramos el sistema Y ya pasó el tiempo de espera, penalizamos
        if (sistema != null && Time.time > tiempoProximaPenalizacion)
        {
            // Usamos tu método público original del SistemaAlertas
            sistema.MostrarInfraccion(mensajeAlerta, puntosAPenalizar);
            
            // Bloqueamos la penalización por 2 segundos para dar tiempo a que el carro corrija
            tiempoProximaPenalizacion = Time.time + intervaloEspera;
            
            Debug.Log($"Línea pisada. Penalización aplicada: -{puntosAPenalizar} puntos.");
        }
    }
}