using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class UIInterface : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turretPropertiesMenu;


    GameObject itemPrefab;
    GameObject focusObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateTurret1()
    {
        itemPrefab = turret1;
        CreateItemForButton();
    }

    public void CreateTurret2()
    {
        itemPrefab = turret2;
        CreateItemForButton();
    }

    public void CreateTurret3()
    {
        itemPrefab = turret3;
        CreateItemForButton();
    }

    public void CloseTurretPropertiesMenu()
    {
        turretPropertiesMenu.SetActive(false);
    }

    void CreateItemForButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
            return;

        focusObject = Instantiate(itemPrefab, hit.point, itemPrefab.transform.rotation);
        focusObject.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) &&
                hit.collider.gameObject.CompareTag("turret"))
            {
                turretPropertiesMenu.SetActive(true);
                turretPropertiesMenu.transform.position = Input.mousePosition;
            }
        }
        else if (focusObject && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit))
                return;

            focusObject.transform.position = hit.point;
        }
        else if (focusObject && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) &&
                hit.collider.gameObject.CompareTag("platform") &&
                hit.normal.Equals(new Vector3(0, 1, 0)))
            {
                hit.collider.gameObject.tag = "occupied";
                focusObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                                                                focusObject.transform.position.y,
                                                                hit.collider.gameObject.transform.position.z);
                focusObject.GetComponent<BoxCollider>().enabled = true;
                focusObject.GetComponent<SphereCollider>().enabled = true;
            }
            else
                Destroy(focusObject);

            focusObject = null;
        }
    }
}
