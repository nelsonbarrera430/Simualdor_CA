using UnityEngine;

public class ActivadorAnimal : MonoBehaviour
{
    public AnimalCruce animal;
    [Range(0f, 1f)]
    public float probabilidad = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Random.value > probabilidad) return;
        if (animal != null) animal.IniciarCruce();
    }
}
