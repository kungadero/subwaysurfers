using UnityEngine;
using UnityEngine.Events;

public class PlatformLimits : MonoBehaviour
{
    [SerializeField]
    private string PlatformsTag = "Ground";
    [SerializeField]
    private UnityEvent onPlatformDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlatformsTag))
        {
            other.gameObject.SetActive(false);
            onPlatformDetected?.Invoke();
        }
    }
}
