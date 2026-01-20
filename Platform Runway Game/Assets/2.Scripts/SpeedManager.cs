using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField]
    float speed = 0f;
    [SerializeField]
    Vector3 moveDr = Vector3.zero;

    private void Update()
    {
        transform.position += moveDr * speed * Time.deltaTime;
    }

    public void MoveDr(Vector3 dr)
    {
        moveDr = dr;
    }
}
