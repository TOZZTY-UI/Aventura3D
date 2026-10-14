
using UnityEngine;

public class Peligro : MonoBehaviour
{
    public int danio = 1;

    private void OnTriggerEnter(Collider otro)
    {
        VidaJugador jugador =
            otro.GetComponent<VidaJugador>();

        if (jugador != null)
        {
            jugador.RecibirDanio(danio);
        }
    }

    private void OnTriggerStay(Collider otro)
    {
        VidaJugador jugador =
            otro.GetComponent<VidaJugador>();

        if (jugador != null)
        {
            jugador.RecibirDanio(danio);
        }
    }
}
