using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Hemos borrado la línea de UnityStandardAssets que daba error

public class CarControl : MonoBehaviour 
{
    private CarMove m_Car;

    private void Awake()
    {
        m_Car = GetComponent<CarMove>();
    }

    private void FixedUpdate()
    {
        // Cambiamos CrossPlatformInputManager por Input que es el de Unity por defecto
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

#if !MOBILE_INPUT
        // Cambiamos aquí también para el freno de mano (espacio)
        float handbrake = Input.GetAxis("Jump");
        m_Car.Move(v, v, handbrake, h);
#else
        m_Car.Move(v, v, 0f, h);
#endif
    }
}