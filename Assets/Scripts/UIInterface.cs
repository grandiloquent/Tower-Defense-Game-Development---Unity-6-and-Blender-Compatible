using UnityEngine;
using UnityEngine.EventSystems;
public class UIInterface : MonoBehaviour
{

    public GameObject cubeTurret;
    public GameObject cubeGatling;
    public GameObject flamer;
    public GameObject turretMenu;
    GameObject focusedObject;
    GameObject itemPrefab;
  
    void Start()
    {

    }
    public void CreateTurret()
    {

        itemPrefab = cubeTurret;
        CreatePrefab();
    }
    public void CreateGatling()
    {
        itemPrefab = cubeGatling;
        CreatePrefab();
    }
   public void CreateFlamer()
    {
        itemPrefab = cubeGatling;
        CreatePrefab();
    }
    void CreatePrefab()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        focusedObject = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
        focusedObject.GetComponent<Collider>().enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
          
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Turret"))
            {
                turretMenu.SetActive(true);
                turretMenu.transform.position =Input.mousePosition;
                return;
            }

        }
        else if (focusedObject && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            focusedObject.transform.position = hit.point ;
        }
        else if (focusedObject && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Platform")
            && hit.normal.Equals(new Vector3(0, 1, 0)))
            {
                hit.collider.gameObject.tag = "Occupied";
                focusedObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                focusedObject.transform.position.y,
                hit.collider.gameObject.transform.position.z);
                focusedObject.GetComponent<Collider>().enabled = true;
            }
            else
            {
                Destroy(focusedObject);
            }
            focusedObject = null;

        }
    }
    void FixedUpdate()
    {

    }
}