using UnityEngine;

public class Shoot : MonoBehaviour
{
    GameObject currentTarget;
    public GameObject core;
    public GameObject gun;
    Quaternion coreStartRot;
    Quaternion gunStartRot;
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
        coreStartRot = core.transform.rotation;
        gunStartRot = gun.transform.rotation;

    }
    bool IsNullOrDestroyed(System.Object obj)
    {

        if (object.ReferenceEquals(obj, null)) return true;

        if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

        return false;
    }
    void Update()
    {
        if (currentTarget)
        {
            Vector3 aimAt = new Vector3(
                currentTarget.transform.position.x,
            core.transform.position.y,
            currentTarget.transform.position.z);

            float distToTarget = Vector3.Distance(aimAt, gun.transform.position);
            Vector3 relativeTarget = gun.transform.position + gun.transform.forward * distToTarget;
            relativeTarget = new Vector3(relativeTarget.x, core.transform.position.y, relativeTarget.z);
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation,
             Quaternion.LookRotation(relativeTarget - gun.transform.position), Time.deltaTime);
            core.transform.rotation = Quaternion.Slerp(core.transform.rotation,
            Quaternion.LookRotation(aimAt - core.transform.position), Time.deltaTime
             );

        }
        else
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation,
                        gunStartRot, Time.deltaTime);
            core.transform.rotation = Quaternion.Slerp(core.transform.rotation,
            coreStartRot, Time.deltaTime
             );
        }

    }
    void FixedUpdate()
    {

    }
}