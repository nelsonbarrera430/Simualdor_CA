using UnityEngine;
using TMPro;

public class VelocimetroHUD : MonoBehaviour
{
    [Header("Referencias")]
    public TextMeshProUGUI textoVelocidad; // El texto TMP que creamos
    public Rigidbody rbCarro;              // El Rigidbody principal del carro

    void Start()
    {
        // Si no arrastras el Rigidbody, el script intenta buscarlo en el objeto padre automáticamente
        if (rbCarro == null)
        {
            rbCarro = GetComponentInParent<Rigidbody>();
        }
    }

    void Update()
    {
        if (rbCarro != null && textoVelocidad != null)
        {
            // Convertimos la magnitud de velocidad a km/h de forma precisa
            float velocidadKMH = rbCarro.velocity.magnitude * 3.6f;

            // Mostramos el valor entero sin decimales en la pantalla del carro
            textoVelocidad.text = Mathf.RoundToInt(velocidadKMH) + " KM/H";
        }
    }
}