
using System.Collections;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    [Header("Vidas")]
    public int vidasMaximas = 3;
    public float tiempoInvulnerable = 1.5f;

    private int vidasActuales;
    private bool invulnerable = false;

    void Start()
    {
        vidasActuales = vidasMaximas;

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.ActualizarVidas(
                vidasActuales, vidasMaximas
            );
        }
    }

    public void RecibirDanio(int cantidad)
    {
        if (invulnerable || vidasActuales <= 0)
            return;

        vidasActuales = Mathf.Max(
            0, vidasActuales - cantidad
        );

        Debug.Log("Vidas restantes: " + vidasActuales);

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.ActualizarVidas(
                vidasActuales, vidasMaximas
            );
        }

        if (vidasActuales <= 0)
        {
            Morir();
        }
        else
        {
            StartCoroutine(ProteccionTemporal());
        }
    }

    IEnumerator ProteccionTemporal()
    {
        invulnerable = true;

        yield return new WaitForSeconds(
            tiempoInvulnerable
        );

        invulnerable = false;
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto");

        if (GameManager.Instancia != null)
            GameManager.Instancia.GameOver();

        MovimientoJugador movimiento =
            GetComponent<MovimientoJugador>();

        if (movimiento != null)
            movimiento.enabled = false;
            Destroy(gameObject);
        }}