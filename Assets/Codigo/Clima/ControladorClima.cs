using UnityEngine;

public class ControladorClima : MonoBehaviour
{
    [Header("Referencias de Iluminación")]
    // Aquí arrastrarás tu Material "HDRI_1" de la foto
    public Material materialSkybox; 
    // Aquí arrastrarás la Luz Direccional (el Sol) de tu escena
    public Light luzSol; 
    
    [Header("Configuración Día")]
    public float exposicionDia = 0.29f; // El valor de tu foto
    public float intensidadLuzDia = 1.0f;

    [Header("Configuración Noche")]
    public float exposicionNoche = 0.05f; // Un valor bajo para que se vea oscuro
    public float intensidadLuzNoche = 0.1f; // Casi apagado

    // Nombre de la variable interna del Shader de Unity para la exposición
    private const string nombreExposicion = "_Exposure"; 

    void Start()
    {
        // Esto es por seguridad, para asegurar que el Material esté asignado
        if (RenderSettings.skybox != materialSkybox)
        {
            RenderSettings.skybox = materialSkybox;
        }
    }

    // Funciones públicas para llamar desde botones o desde el Gestor de Modos
    public void ConfigurarModoDia()
    {
        CambiarExposicion(exposicionDia);
        CambiarLuz(intensidadLuzDia);
    }

    public void ConfigurarModoNoche()
    {
        CambiarExposicion(exposicionNoche);
        CambiarLuz(intensidadLuzNoche);
    }

    // Función interna que hace el trabajo sucio
    void CambiarExposicion(float valor)
    {
        if (materialSkybox != null)
        {
            // Esta línea cambia el slider "Exposure" de tu foto por código
            materialSkybox.SetFloat(nombreExposicion, valor);
            // Esto actualiza la iluminación de la escena para que no se vea raro
            DynamicGI.UpdateEnvironment(); 
        }
    }

    void CambiarLuz(float intensidad)
    {
        if (luzSol != null)
        {
            luzSol.intensity = intensidad;
        }
    }
}