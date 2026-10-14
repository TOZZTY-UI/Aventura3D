
using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraJugador : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform objetivo;

    [Header("Distancia")]
    public float distancia = 5f;

    [Header("Mouse")]
    public float sensibilidad = 0.15f;
    public float suavidad = 12f;

    [Header("Limites verticales")]
    public float anguloMinimo = -30f;
    public float anguloMaximo = 70f;

    private float rotacionHorizontal;
    private float rotacionVertical;

    void Start()
    {
        rotacionHorizontal = transform.eulerAngles.y;

        rotacionVertical = transform.eulerAngles.x;

        if (rotacionVertical > 180f)
            rotacionVertical -= 360f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 mouse = Mouse.current.delta.ReadValue();

        rotacionHorizontal += mouse.x * sensibilidad;
        rotacionVertical -= mouse.y * sensibilidad;

        rotacionVertical = Mathf.Clamp(
            rotacionVertical,
            anguloMinimo,
            anguloMaximo
        );
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        Quaternion rotacion = Quaternion.Euler(
            rotacionVertical,
            rotacionHorizontal,
            0f
        );

        Vector3 posicionDeseada =
            objetivo.position -
            rotacion * Vector3.forward * distancia;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavidad * Time.deltaTime
        );

        transform.rotation = rotacion;
    }
}
