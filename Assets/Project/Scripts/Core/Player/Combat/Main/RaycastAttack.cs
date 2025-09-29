using Fusion;
using UnityEngine;

public class RaycastAttack : NetworkBehaviour
{
    public float Damage = 10;

    [Header("References")]
    [SerializeField] private Camera playerCamera;

    void Update()
    {
        //if (HasStateAuthority == false)
            //return;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            ray.origin += playerCamera.transform.forward * 0.1f;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                if (hit.transform.TryGetComponent<Health>(out var health))
                {
                    health.DealDamageRpc(Damage);
                }
            }
        }
    }
}