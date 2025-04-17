using UnityEngine;

public class BoxSize: MonoBehaviour
{
    void Awake()
    {
        var boundsSize = GetComponent<Renderer>().bounds.size;
        Debug.Log("Bounds Size: " + boundsSize);
    }
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
}