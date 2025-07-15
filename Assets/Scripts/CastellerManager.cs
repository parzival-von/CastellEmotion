using UnityEngine;

public class CastellerManager : MonoBehaviour
{
    public GameObject[] castellers;

    public void FerCaureTots()
    {
        foreach (var casteller in castellers)
        {
            Rigidbody rb = casteller.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            Animator anim = casteller.GetComponent<Animator>();
            if (anim != null) anim.enabled = false;
        }

        Debug.Log("🧍‍♂️ Tots els castellers han caigut.");
    }
}

