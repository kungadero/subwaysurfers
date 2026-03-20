using UnityEngine;
using System.Collections;

public class MagnetPowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject magnet;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private Collider magnetCollider;
    private Coroutine deactivateCoruotine;
    public void Active()
    {
        magnet.SetActive(true);
        magnetCollider.enabled = true;
        if (deactivateCoruotine != null)
        {
            StopCoroutine(deactivateCoruotine);
        }
        deactivateCoruotine= StartCoroutine(DeactivateAfterDuration());
    }
    public void Deactivate()
    {
        if (deactivateCoruotine != null)
        {
            StopCoroutine(deactivateCoruotine);
            deactivateCoruotine = null;
        }
        magnet.SetActive (false);
        magnetCollider.enabled = false;
    }
        private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}
