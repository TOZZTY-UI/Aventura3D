
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

    [Header("Monedas")]
    public TMP_Text textoMonedas;
    public TMP_Text textoVictoria;

    [Header("Vidas")]
    public TMP_Text textoVidas;
    public TMP_Text textoGameOver;

    private int monedasRecogidas = 0;
    private int monedasTotales = 0;
    private bool juegoTerminado = false;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        monedasTotales = FindObjectsByType<Moneda>(
            FindObjectsSortMode.None
        ).Length;

        if (textoVictoria != null)
            textoVictoria.text = "";

        if (textoGameOver != null)
            textoGameOver.text = "";

        ActualizarContador();
    }

    void Update()
    {
        if (juegoTerminado &&
            Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }

    public void RecogerMoneda()
    {
        if (juegoTerminado) return;

        monedasRecogidas++;
        ActualizarContador();

        if (monedasTotales > 0 &&
            monedasRecogidas >= monedasTotales)
        {
            GanarJuego();

        }
    }

    void ActualizarContador()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text =
                "Monedas: " + monedasRecogidas +
                "/" + monedasTotales;
        }
    }

    public void ActualizarVidas(int actual, int maximo)
    {
        if (textoVidas != null)
        {
            textoVidas.text =
                "Vidas: " + actual + "/" + maximo;
        }
    }

    void GanarJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        if (textoVictoria != null)
            textoVictoria.text =
                "¡GANASTE!\nPresiona R para reiniciar";

        LiberarCursor();
    }

    public void GameOver()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;

        if (textoGameOver != null)
            textoGameOver.text =
                "GAME OVER\nPresiona R para reiniciar";

        LiberarCursor();
    }

    void LiberarCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
