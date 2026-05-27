using UnityEngine;

public class DetectaCurva : MonoBehaviour
{
    [Header("Configuración de la Curva")]
    public Transform contenedorPuntos; // Aquí arrastras el objeto padre 'Camino_Linea_Infraccion'
    public float distanciaFalta = 0.8f; // Radio de peligro (ajústalo según el ancho de tu carril)
    public int puntosPenalizacion = 5;

    [Header("Control de Tiempo")]
    public float intervaloEspera = 2f;
    private float tiempoProximaPenalizacion = 0f;

    private SistemaAlertas sistemaAlertas;
    private Transform[] puntos;

    void Start()
    {
        sistemaAlertas = GetComponent<SistemaAlertas>();
        if (sistemaAlertas == null) sistemaAlertas = GetComponentInParent<SistemaAlertas>();

        // Guardamos todos los puntos hijos en un arreglo para recorrerlos rápido
        if (contenedorPuntos != null)
        {
            int cuenta = contenedorPuntos.childCount;
            puntos = new Transform[cuenta];
            for (int i = 0; i < cuenta; i++)
            {
                puntos[i] = contenedorPuntos.GetChild(i);
            }
        }
    }

    void Update()
    {
        if (puntos == null || puntos.Length == 0 || sistemaAlertas == null) return;

        // Si ya pasó el tiempo de espera para volver a penalizar
        if (Time.time > tiempoProximaPenalizacion)
        {
            // Medimos la distancia actual del carro hacia cada punto de la curva
            foreach (Transform punto in puntos)
            {
                float distancia = Vector3.Distance(transform.position, punto.position);

                // Si el carro se acerca al radio de la línea, es infracción
                if (distancia < distanciaFalta)
                {
                    sistemaAlertas.MostrarInfraccion("¡INFRACCIÓN! NO PISES LA LÍNEA EN CURVA", puntosPenalizacion);
                    tiempoProximaPenalizacion = Time.time + intervaloEspera;
                    break; // Salimos del bucle para no procesar más puntos en este frame
                }
            }
        }
    }

    // Dibuja esferas rojas en el editor para que puedas ver el radio de peligro de cada punto
    void OnDrawGizmosSelected()
    {
        if (contenedorPuntos == null) return;
        Gizmos.color = Color.red;
        foreach (Transform hijo in contenedorPuntos)
        {
            if (hijo != null) Gizmos.DrawWireSphere(hijo.position, distanciaFalta);
        }
    }
}