using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; 

public class MenuManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelLogin;
    public GameObject panelRegistro;

    [Header("Inputs")]
    public TMP_InputField inputCedulaLogin;
    public TMP_InputField inputCedulaRegistro;

    void Start()
    {
        panelLogin.SetActive(true);
        panelRegistro.SetActive(false);
    }

    public void IrARegistro()
    {
        panelLogin.SetActive(false);
        panelRegistro.SetActive(true);
    }

    public void VolverALogin()
    {
        panelRegistro.SetActive(false);
        panelLogin.SetActive(true);
    }

    public void IniciarSesion() 
    {
        StartCoroutine(EnviarPeticion("http://localhost:3000/login", inputCedulaLogin.text));
    }

    public void RegistrarUsuario() 
    {
        StartCoroutine(EnviarPeticion("http://localhost:3000/registrar", inputCedulaRegistro.text));
    }

    IEnumerator EnviarPeticion(string url, string cedula) 
    {
        if (string.IsNullOrEmpty(cedula))
        {
            Debug.LogWarning("La cédula está vacía");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("cedula", cedula);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) 
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success) 
            {
                Debug.Log("Respuesta: " + www.downloadHandler.text);
                
                // Guardamos en el archivo externo GameData
                GameData.CedulaActual = cedula;

                // Cambiamos a la escena de configuración
                SceneManager.LoadScene("02_Configuracion");
            } 
            else 
            {
                Debug.LogError("Error en la comunicación: " + www.error);
            }
        }
    }
}