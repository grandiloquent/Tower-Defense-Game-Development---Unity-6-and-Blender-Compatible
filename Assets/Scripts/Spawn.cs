using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject prefab;
    public Transform start;
    public float startDelay=1.0f;
    public float spawnRate = 0.3f;
    public int maxCount = 10;
    int count = 0;
    void Start()
    {
        InvokeRepeating("SpawnObject", startDelay, spawnRate);
    }
    void Update()
    {
    }
    void SpawnObject()
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        obj.GetComponent<FindHome>().destination = start;
        count++;
        if (count >= maxCount)
        {
            CancelInvoke("SpawnObject");
        }
    }

}