
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemigoPatrulla : MonoBehaviour
{
    public float velocidad = 2f;
    public float distancia = 3f;

    private Vector3 posicionInicial;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        posicionInicial = transform.position;
    }

    void FixedUpdate()
    {
        float recorrido = Mathf.PingPong(
            Time.fixedTime * velocidad,
            distancia * 2f
        ) - distancia;

        Vector3 destino = posicionInicial +
            Vector3.right * recorrido;

        rb.MovePosition(destino);
    }
}
