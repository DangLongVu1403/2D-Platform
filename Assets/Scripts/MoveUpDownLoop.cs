using UnityEngine;

public class MoveUpDownLoop : MonoBehaviour
{
    [SerializeField] float moveDistance = 1f;
    [SerializeField] float moveSpeed = 0.01f;
    Vector3 startPos;

    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
    }

    void Update()
    {
        float newY = startPos.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
