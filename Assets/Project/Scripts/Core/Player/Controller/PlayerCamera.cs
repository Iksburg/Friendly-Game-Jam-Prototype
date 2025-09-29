using Fusion;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody;

    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 100f; // Чувствительность мыши
    [SerializeField] private float maxLookAngle = 85f; // Максимальный угол обзора

    private float xRotation = 0f;

    public override void Spawned()
    {
        //if (HasInputAuthority)
        //{
          //  Cursor.lockState = CursorLockMode.Locked;
           // Cursor.visible = false;
        //}
        //else
        //{
          //  GetComponent<Camera>().enabled = false;
            //if (TryGetComponent<AudioListener>(out var listener)) listener.enabled = false;
        //}
    }

    public override void FixedUpdateNetwork()
    {
        //if (!HasInputAuthority)
            //return;

        //if (GetInput(out NetworkInputData data))
        //{
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Поворот камерой вверх/вниз
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Поворот тела игрока (влево/вправо)
            playerBody.Rotate(Vector3.up * mouseX);
        //}
    }

    public struct NetworkInputData : INetworkInput
    {
        // Вынести в отдельный скрипт
        public Vector2 moveInput;
        public Vector2 lookInput;
        public NetworkButtons buttons;
    }
    
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // Возможно отедльный скрипт
        NetworkInputData data = new NetworkInputData();

        data.moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        data.lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (Input.GetButton("Jump"))
            data.buttons.Set(0, true);

        input.Set(data);
    }

}