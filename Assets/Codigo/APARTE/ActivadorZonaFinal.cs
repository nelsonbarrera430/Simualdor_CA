using UnityEngine;

public class ActivadorZonaFinal : MonoBehaviour
{
    public int numeroCheckpoint = 1;
    public GameObject zonaFinal;

    void Start()
    {
        if (zonaFinal != null) zonaFinal.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckpointManager.RegistrarCheckpoint(numeroCheckpoint);
        if (zonaFinal != null) zonaFinal.SetActive(true);
    }
}
