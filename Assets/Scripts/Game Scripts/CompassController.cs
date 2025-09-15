using UnityEngine;

public class CompassController : MonoBehaviour
{
    private Transform target;
    public Transform player;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null || player == null) return;

        Vector3 direction = target.position - player.position;
        direction.y = 0; // keep compass flat

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
