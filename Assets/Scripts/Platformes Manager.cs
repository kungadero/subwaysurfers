using UnityEngine;

public class PlatformesManager : MonoBehaviour
{
    [SerializeField]
    private Transform platformsPivot;
    [SerializeField]
    private InstantiatePoolObjects[] platformPrefabs;
    [SerializeField]
    private int initialPlatforms = 5;
    [SerializeField]
    private float speed = 5f;
    private bool isRunning = true;
    private GameObject lastPlatform;
    private void Start()
    {
      InstantiatePlatform(initialPlatforms);
      transform.position = platformsPivot.position;
    }
    public void InstantiatePlatform(int number)
    {
        for (int i = 0; i < number; i++)
        {
            InstantiatePoolObjects instantiatePool= platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            Vector3 SpawnPosition = Vector3.zero;
            if (lastPlatform != null)
            {
                SpawnPosition = lastPlatform.transform.localPosition + lastPlatform.GetComponent<Collider>().bounds.size.z * Vector3.forward * 0.5f;                
            }
            instantiatePool.InstantiateObject(SpawnPosition);
            GameObject newPlatform= instantiatePool.GetCurrentObject();
            newPlatform.transform.SetParent(transform);
            newPlatform.transform.localPosition = SpawnPosition + newPlatform.GetComponent<Collider>().bounds.size.z * Vector3.forward * 0.5f;
            lastPlatform = newPlatform;
            }
    }
    private void Update()
    {
        if (isRunning)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void StopPlatforms()
    {
        isRunning = false;
    }

 }
