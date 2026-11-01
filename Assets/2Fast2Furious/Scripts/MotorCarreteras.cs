using UnityEngine;

public class MotorCarreteras : MonoBehaviour
{
    public GameObject contenedorCallesGO;
    public GameObject[] contenedorCallesArray;
    public float velocidad;
    public bool inicioJuego;
    public bool JuegoTerminado;

    void Start()
    {
        InicioJuego();
    }

    void InicioJuego()
    {
        contenedorCallesGO = GameObject.Find("ContenedorCalles");
        VelocidadMotorCarretera();
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
    }

    void Update()
    {
        if (inicioJuego == true && JuegoTerminado == false)
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
    }
}
