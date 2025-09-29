
using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;       // Скорость передвижения
    [SerializeField] private float jumpHeight = 1.5f;    // Высота прыжка
    [SerializeField] private float gravity = -9.81f;     // Сила гравитации
    [SerializeField] private Transform cameraTransform;  // Ссылка на камеру, прикреплённую к объекту игрока

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Движение относительно камеры
        Vector3 move = cameraTransform.right * horizontal + cameraTransform.forward * vertical;
        move.y = 0f;
        controller.Move(moveSpeed * Runner.DeltaTime * move.normalized);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Runner.DeltaTime;
        controller.Move(velocity * Runner.DeltaTime);
    }
}
