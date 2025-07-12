using UnityEngine;

public class FollowRig : MonoBehaviour
{
    public Transform xrOrigin;
    public Vector3 offset;

    void Update()
    {
        if (xrOrigin != null)
        {
            Vector3 targetPos = xrOrigin.position + offset;
            transform.position = targetPos;

            // Solo rota con el eje Y (giro horizontal del jugador)
            float yRotation = xrOrigin.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
