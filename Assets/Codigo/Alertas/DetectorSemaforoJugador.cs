using UnityEngine;

public class SensorSemaforoJugador : MonoBehaviour
{
    // Aquí arrastras el semáforo original del Asset
    public SemaphoreSimulator semaforoOriginal;
    
    // 0 para carril principal, 1 para el que cruza
    public int miStage = 0;

    public bool EstaEnRojo()
    {
        if (semaforoOriginal == null) return false;
        // Si el Asset dice que hay amarillo o no es nuestro turno, es ROJO
        return semaforoOriginal.YELLOW_ON || semaforoOriginal.STAGE != miStage;
    }
}