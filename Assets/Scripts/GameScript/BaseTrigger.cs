using UnityEngine;

public class BaseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ make sure XR Rig root has tag "Player"
        {
            Debug.Log("🏠 Player reached the base!");
            FindObjectOfType<GameManager>().PlayerWin();
        }
    }
}
