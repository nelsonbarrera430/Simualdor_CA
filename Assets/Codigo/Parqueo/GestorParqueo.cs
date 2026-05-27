using UnityEngine;
using TMPro;

public class GestorParqueo : MonoBehaviour
{
    [Header("Reglas de Evaluación")]
    public int maxMovimientos = 3; 
    public float anguloMaximoTolerancia = 15f; 

    [Header("Referencias UI")]
    public TextMeshProUGUI textoHUD;
    public TextMeshProUGUI textoDetallesExito;
    public GameObject panelExito;

    [Header("Estado Actual")]
    public bool estaEnZona = false;
    public bool tocandoAcera = false;
    public int movimientosRealizados = 0;
    
    private float direccionAnterior = 0;
    private Rigidbody rb;
    private Transform zonaObjetivo;

    void Start() {
        rb = GetComponent<Rigidbody>();
        if(panelExito != null) panelExito.SetActive(false);
    }

    void Update() {
        if (estaEnZona) {
            ContarManiobras();
            if (textoHUD != null) textoHUD.text = "Movimientos: " + movimientosRealizados;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ZonaParqueo")) {
            estaEnZona = true;
            zonaObjetivo = other.transform;
        }
        
        
        if (other.CompareTag("AceraSensor")) {
            tocandoAcera = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("ZonaParqueo")) estaEnZona = false;
        if (other.CompareTag("AceraSensor")) tocandoAcera = false;
    }

    public void ValidarParqueoFinal() {
        if (!estaEnZona || zonaObjetivo == null) return;

        // --- CORRECCIÓN DE ÁNGULO ---
        // Calculamos la diferencia de rotación
        float anguloFinal = Quaternion.Angle(transform.rotation, zonaObjetivo.rotation);
        
        // Si el ángulo da cerca de 180 o 90, es porque el cubo está mal rotado en el editor.
        // Esta lógica intenta normalizar el ángulo a la cara más cercana.
        if (anguloFinal > 90) anguloFinal = Mathf.Abs(anguloFinal - 180);
        if (anguloFinal > 45) anguloFinal = Mathf.Abs(anguloFinal - 90);

        float puntajeTotal = 100f;

        // 1. Evaluación de Maniobras
        if (movimientosRealizados > maxMovimientos) puntajeTotal -= 20f;

        // 2. Evaluación de Alineación (Ángulo)
        if (anguloFinal > anguloMaximoTolerancia) puntajeTotal -= 40f;

        // 3. Evaluación de Distancia (Acera)
        if (!tocandoAcera) puntajeTotal -= 40f;

        puntajeTotal = Mathf.Max(0, puntajeTotal);

        // Mostrar Resultados
        if (panelExito != null) {
            panelExito.SetActive(true);
            if (textoDetallesExito != null) {
                textoDetallesExito.text = 
                    $"RESULTADO TÉCNICO:\n" +
                    $"----------------------------\n" +
                    $"ÁNGULO: {anguloFinal:F2}° {(anguloFinal <= anguloMaximoTolerancia ? "✔" : "✘")}\n" +
                    $"MANIOBRAS: {movimientosRealizados} {(movimientosRealizados <= maxMovimientos ? "✔" : "✘")}\n" +
                    $"DISTANCIA: {(tocandoAcera ? "CORRECTA ✔" : "MUY LEJOS ✘")}\n\n" +
                    $"PUNTAJE FINAL: {puntajeTotal}/100";
            }
        }
    }

    void ContarManiobras() {
        float inputVertical = Input.GetAxisRaw("Vertical");
        // Solo contamos si el carro se está moviendo realmente
        if (rb.velocity.magnitude > 0.1f && Mathf.Abs(inputVertical) > 0.1f) {
            float direccionActual = Mathf.Sign(inputVertical);
            if (direccionAnterior != 0 && direccionActual != direccionAnterior) {
                movimientosRealizados++;
            }
            direccionAnterior = direccionActual;
        }
    }
}