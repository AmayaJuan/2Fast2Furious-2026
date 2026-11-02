using UnityEngine;

public class CocheOdstaculo : MonoBehaviour
{
    public GameObject cronometroGO;
    public GameObject audiFX;
    public Cronometro cronometroScript;
    public AudioFX audioFXScript;

    void Start()
    {
        cronometroGO = GameObject.FindFirstObjectByType<Cronometro>().gameObject;
        cronometroScript = cronometroGO.GetComponent<Cronometro>();

        audiFX = GameObject.FindFirstObjectByType<AudioFX>().gameObject;
        audioFXScript = audiFX.GetComponent<AudioFX>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Coche>() != null)
        {
            audioFXScript.FXSonidoChoque();
            cronometroScript.tiempo = cronometroScript.tiempo - 20;
            Destroy(gameObject);
        }
    }
}
