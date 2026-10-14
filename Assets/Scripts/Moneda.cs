
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    private bool recogida = false;

    void Update()
    {
        // Hacer girar la moneda
        transform.Rotate(
            Vector3.up,
            velocidadRotacion * Time.deltaTime,
            Space.World
        );
    }

    private void OnTriggerEnter(Collider otro)
    {
        // Comprobar si es el jugador
        if (recogida || !otro.CompareTag("Player"))
            return;

        recogida = true;

        // Sumar una moneda al contador
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.RecogerMoneda();
        }

        // Eliminar la moneda
        Destroy(gameObject);
    }
}
