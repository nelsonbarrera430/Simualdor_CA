using UnityEngine;

public class SistemaAlertas : MonoBehaviour
{
    public float limiteVelocidad = 80f;
    public VisualizadorAlertas visualizador; 
    public PuntajeManager puntajeManager; 
    private Rigidbody rb;

    // Control de tiempo para no restar puntos infinitos en un solo segundo
    private float tiempoProximaPenalizacion = 0f;
    public float intervaloPenalizacion = 2f; 

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float velocidadActual = rb.velocity.magnitude * 3.6f;

        // Solo penaliza si superas el límite Y ya pasó el tiempo de espera
        if (velocidadActual > limiteVelocidad && Time.time > tiempoProximaPenalizacion) {
            MostrarInfraccion("¡EXCESO DE VELOCIDAD!", 2);
            tiempoProximaPenalizacion = Time.time + intervaloPenalizacion;
        }
    }

    void OnCollisionEnter(Collision collision) {
        // Si choca
        if (!collision.gameObject.CompareTag("Road")) {
            MostrarInfraccion("¡CHOQUE DETECTADO!", 5);
        }
    }

    void OnTriggerStay(Collider other) {
        SensorSemaforoJugador sensor = other.GetComponent<SensorSemaforoJugador>();
        
        if (sensor != null) {
            // Si el semáforo está en rojo y el carro se mueve, penaliza con tiempo de espera
            if (sensor.EstaEnRojo() && rb.velocity.magnitude > 0.5f && Time.time > tiempoProximaPenalizacion) {
                MostrarInfraccion("¡INFRACCIÓN! SEMÁFORO EN ROJO", 10);
                tiempoProximaPenalizacion = Time.time + intervaloPenalizacion;
            }
        }
    }

    // SOLO CAMBIÉ ESTA LÍNEA (AGREGUÉ PUBLIC)
    public void MostrarInfraccion(string mensaje, int puntos) {
        if (visualizador != null) visualizador.MostrarMensaje(mensaje);
        if (puntajeManager != null) puntajeManager.RegistrarInfraccion(puntos);
    }
}