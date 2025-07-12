using UnityEngine;

public class HandFollower : MonoBehaviour
{
    public Transform target; // Objeto del controlador XR

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}

