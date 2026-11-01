using UnityEngine;

public class MotorCarreteras : MonoBehaviour
{
    public GameObject[] contenedorCallesArray;
    public GameObject contenedorCallesGO;
    public GameObject calleAnterior;
    public GameObject calleNueva;
    public GameObject mCamGO;
    public Camera mCamComp;
    public float velocidad;
    public float tamañoCalle;
    public bool inicioJuego;
    public bool juegoTerminado;
    public bool salioDePantalla;
    public Vector3 medidaLimitePantalla;

    int contadorCalles = 0;
    int numeroSelectorCalles;

    void Start()
    {
        InicioJuego();
    }

    void InicioJuego()
    {
        contenedorCallesGO = GameObject.Find("ContenedorCalles");
        mCamGO = GameObject.Find("Main Camera");
        mCamComp = mCamGO.GetComponent<Camera>();
        VelocidadMotorCarretera();
        MedirPantalla();
        BuscoCalles();
    }

    void VelocidadMotorCarretera()
    {
        velocidad = 18;
    }

    void BuscoCalles()
    {
        contenedorCallesArray = GameObject.FindGameObjectsWithTag("Calle");
        for (int i = 0; i < contenedorCallesArray.Length; i++)
        {
            contenedorCallesArray[i].gameObject.transform.parent = contenedorCallesGO.transform;
            contenedorCallesArray[i].gameObject.SetActive(false);
            contenedorCallesArray[i].gameObject.name = "CalleOFF_" + i;
        }
        CrearCalles();
    }

    void CrearCalles()
    {
        contadorCalles++;
        numeroSelectorCalles = Random.Range(0, contenedorCallesArray.Length);
        GameObject Calle = Instantiate(contenedorCallesArray[numeroSelectorCalles]);
        Calle.SetActive(true);
        Calle.name = "Calle" + contadorCalles;
        Calle.transform.parent = gameObject.transform;
        PosicionoCalles();
    }

    void PosicionoCalles()
    {
        calleAnterior = GameObject.Find("Calle" + (contadorCalles - 1));
        calleNueva = GameObject.Find("Calle" + contadorCalles);
        MidoCalle();
        calleNueva.transform.position = new Vector3(calleAnterior.transform.position.x, calleAnterior.transform.position.y + tamañoCalle, 0);

        salioDePantalla = false;
    } 

    void MidoCalle()
    {
        for (int i = 0; i < calleAnterior.transform.childCount; i++)
        {
            if (calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null)
            {
                float tamañoPieza = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                tamañoCalle = tamañoCalle + tamañoPieza;
            }
        }
    }

    void MedirPantalla()
    {
        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y -0.5f, 0);
    }

    void Update()
    {
        if (inicioJuego == true && juegoTerminado == false)
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        if (calleAnterior.transform.position.y + tamañoCalle < medidaLimitePantalla.y && salioDePantalla == false)
        {
            salioDePantalla = true;
            DestruyoCalles();
        }
    }

    void DestruyoCalles()
    {
        Destroy(calleAnterior);
        tamañoCalle = 0;
        calleAnterior = null;
        CrearCalles();
    }
}
