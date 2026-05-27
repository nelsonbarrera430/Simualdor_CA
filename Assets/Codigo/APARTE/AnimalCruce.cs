using UnityEngine;
using System.Collections;

public class AnimalCruce : MonoBehaviour
{
    public Transform puntoInicio;
    public Transform puntoFinal;
    public float velocidad = 4f;
    public int puntosAtropello = 5;

    private bool enMovimiento = false;
    private bool atropellado  = false;
    private SistemaAlertas sistemaAlertas;
    private PuntajeManager puntajeManager;

    void Start()
    {
        sistemaAlertas = FindObjectOfType<SistemaAlertas>();
        puntajeManager = FindObjectOfType<PuntajeManager>();
        if (puntoInicio != null)
            transform.position = puntoInicio.position;
        gameObject.SetActive(false);
    }

    public void IniciarCruce()
    {
        atropellado = false;
        gameObject.SetActive(true);
        if (puntoInicio != null)
            transform.position = puntoInicio.position;
        if (puntoFinal != null)
            transform.LookAt(new Vector3(puntoFinal.position.x, transform.position.y, puntoFinal.position.z));
        enMovimiento = true;
    }

    void Update()
    {
        if (!enMovimiento || atropellado || puntoFinal == null) return;

        transform.position = Vector3.MoveTowards(transform.position, puntoFinal.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, puntoFinal.position) < 0.1f)
        {
            enMovimiento = false;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (atropellado) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        atropellado  = true;
        enMovimiento = false;

        if (sistemaAlertas != null)
            sistemaAlertas.MostrarInfraccion("!ATROPELLASTE UN ANIMAL!", puntosAtropello);
        else if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosAtropello);

        StartCoroutine(DesactivarDespues(2f));
    }

    IEnumerator DesactivarDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        gameObject.SetActive(false);
        atropellado = false;
    }
}
