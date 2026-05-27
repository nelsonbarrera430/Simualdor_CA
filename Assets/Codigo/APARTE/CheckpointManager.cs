using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instancia;

    public Transform jugador;

    private Vector3[] posiciones = new Vector3[]
    {
        new Vector3(-326.29f,  0.23f,  -114.2f),
        new Vector3(-185.64f,  0.24f,   -23.5f),
        new Vector3( 272.81f,  0.276f,  -66.079f)
    };

    private Vector3[] rotaciones = new Vector3[]
    {
        new Vector3( 0.181f, -807.678f, -2.43f),
        new Vector3( 1.751f, -721.064f,  0.037f),
        new Vector3( 0.037f, -811.065f, -1.751f)
    };

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        int cp = PlayerPrefs.GetInt("CheckpointActual", 0);
        if (cp > 0) Respawnear(cp);
    }

    public static void RegistrarCheckpoint(int numero)
    {
        PlayerPrefs.SetInt("CheckpointActual", numero);
        PlayerPrefs.Save();
    }

    public void Respawnear(int numero)
    {
        if (jugador == null) return;
        int i = numero - 1;
        if (i < 0 || i >= posiciones.Length) return;
        jugador.position = posiciones[i];
        jugador.rotation = Quaternion.Euler(rotaciones[i]);
    }
}
