using UnityEngine;

public class RespiracioAnimada : MonoBehaviour
{
    public float minScale = 1f;
    public float maxScale = 1.25f;
    public float speed = 1.5f;

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
