using UnityEngine;

public class ActivadorPeaton : MonoBehaviour
{
    public PeatonCruce peaton;
    [Range(0f, 1f)]
    public float probabilidad = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Random.value > probabilidad) return;
        if (peaton != null) peaton.IniciarCruce();
    }
}
