using UnityEngine;

public class Shoot : MonoBehaviour
{
    GameObject currentTarget;
    public GameObject core;
    public GameObject gun;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goob"))
        {
            currentTarget = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other == currentTarget)
        {
            currentTarget = null;
        }
    }
    void Start()
    {

    }
    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 aimAt = new Vector3(currentTarget.transform.position.x, core.transform.position.y, currentTarget.transform.position.z);
            core.transform.LookAt(aimAt);
        }
    }
    void FixedUpdate()
    {

    }
}