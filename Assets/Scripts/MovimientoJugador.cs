
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 4f;
    public float velocidadCorrer = 8f;
    public float aceleracion = 10f;
    public float velocidadGiro = 12f;

    [Header("Salto")]
    public float alturaSalto = 1.8f;
    public float gravedad = -9.81f;




    private CharacterController controlador;
    private float velocidadActual;
    private float velocidadVertical;

    void Awake()
    {
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. Leer teclado
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current.aKey.isPressed) horizontal -= 1;
        if (Keyboard.current.dKey.isPressed) horizontal += 1;
        if (Keyboard.current.wKey.isPressed) vertical += 1;
        if (Keyboard.current.sKey.isPressed) vertical -= 1;

        // 2. Obtener direccion de la camara
        Vector3 adelante = Vector3.forward;
        Vector3 derecha = Vector3.right;

        if (Camera.main != null)
        {
            adelante = Camera.main.transform.forward;
            derecha = Camera.main.transform.right;

            adelante.y = 0f;
            derecha.y = 0f;

            adelante.Normalize();
            derecha.Normalize();
        }

        Vector3 direccion =
            (adelante * vertical + derecha * horizontal).normalized;

        // 3. Caminar o correr
        bool correr =
            Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed;

        float velocidadObjetivo =
            correr ? velocidadCorrer : velocidadCaminar;

        if (direccion.sqrMagnitude < 0.01f)
            velocidadObjetivo = 0f;

        velocidadActual = Mathf.MoveTowards(
            velocidadActual,
            velocidadObjetivo,
            aceleracion * Time.deltaTime
        );

        // 4. Girar al personaje
        if (direccion.sqrMagnitude > 0.01f)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacion,
                velocidadGiro * Time.deltaTime
            );
        }

        // 5. Saltar y aplicar gravedad
        if (controlador.isGrounded && velocidadVertical < 0f)
            velocidadVertical = -2f;

        if (controlador.isGrounded &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            velocidadVertical = Mathf.Sqrt(
                alturaSalto * -2f * gravedad
            );
        }

        velocidadVertical += gravedad * Time.deltaTime;

        // 6. Mover al jugador
        Vector3 movimiento = direccion * velocidadActual;
        movimiento.y = velocidadVertical;

        controlador.Move(movimiento * Time.deltaTime);
    }
}
