using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UIInterface : MonoBehaviour
{
    public GameObject turret1;
    public GameObject turret2;

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
            }
            else
                Destroy(focusObject);

            focusObject = null;
        }
    }
}
