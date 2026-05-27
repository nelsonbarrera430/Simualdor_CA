using UnityEngine;
using System.Collections;

public class PeatonCruce : MonoBehaviour
{
    [Header("Ruta")]
    public Transform puntoInicio;
    public Transform puntoFinal;
    public float velocidad = 3f;

    [Header("Animaciones - nombres exactos del Animator Controller")]
    public string paramCorriendo = "Corriendo";
    public string paramMuerte    = "Muerte";

    [Header("Penalizacion al atropellar (opcional)")]
    public int puntosAtropello = 10;

    private Animator animator;
    private bool enMovimiento = false;
    private bool muerto       = false;
    private SistemaAlertas sistemaAlertas;
    private PuntajeManager  puntajeManager;

    void Start()
    {
        animator       = GetComponent<Animator>();
        sistemaAlertas = FindObjectOfType<SistemaAlertas>();
        puntajeManager = FindObjectOfType<PuntajeManager>();

        if (puntoInicio != null)
            transform.position = puntoInicio.position;

        gameObject.SetActive(false);
    }

    public void IniciarCruce()
    {
        if (muerto) return;
        gameObject.SetActive(true);

        if (puntoInicio != null)
            transform.position = puntoInicio.position;

        if (puntoFinal != null)
            transform.LookAt(new Vector3(puntoFinal.position.x, transform.position.y, puntoFinal.position.z));

        enMovimiento = true;

        if (animator != null)
            animator.SetBool(paramCorriendo, true);
    }

    void Update()
    {
        if (!enMovimiento || muerto || puntoFinal == null) return;

        transform.position = Vector3.MoveTowards(transform.position, puntoFinal.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, puntoFinal.position) < 0.1f)
        {
            enMovimiento = false;
            if (animator != null)
                animator.SetBool(paramCorriendo, false);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (muerto) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        muerto       = true;
        enMovimiento = false;

        if (animator != null)
        {
            animator.SetBool(paramCorriendo, false);
            animator.SetTrigger(paramMuerte);
        }

        if (sistemaAlertas != null)
            sistemaAlertas.MostrarInfraccion("!ATROPELLASTE A UN PEATON!", puntosAtropello);
        else if (puntajeManager != null)
            puntajeManager.RegistrarInfraccion(puntosAtropello);

        StartCoroutine(DesactivarDespues(3f));
    }

    IEnumerator DesactivarDespues(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        gameObject.SetActive(false);
        muerto = false;
    }
}
