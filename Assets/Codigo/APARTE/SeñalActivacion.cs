using UnityEngine;

public class SeñalActivacion : MonoBehaviour
{
    public GameObject señalStop;
    public GameObject señalSiga;
    [Range(0f, 1f)]
    public float probabilidadStop = 0.5f;
    public GameObject zonaEspera;

    void Start()
    {
        if (señalStop != null) señalStop.SetActive(false);
        if (señalSiga != null) señalSiga.SetActive(false);
        if (zonaEspera != null) zonaEspera.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bool esStop = Random.value < probabilidadStop;
        if (señalStop != null) señalStop.SetActive(esStop);
        if (señalSiga != null) señalSiga.SetActive(!esStop);
        if (zonaEspera != null) zonaEspera.SetActive(esStop);
    }
}
