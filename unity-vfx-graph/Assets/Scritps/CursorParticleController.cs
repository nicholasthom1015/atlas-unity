using UnityEngine;

public class CursorParticleController : MonoBehaviour
{
    public float distance = 10;

    void Update ()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(distance);
        transform.position = pos;
    }
}