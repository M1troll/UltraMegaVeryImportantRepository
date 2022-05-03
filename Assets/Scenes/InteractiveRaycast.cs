using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab;

    [SerializeField]
    private GameObject selectedInteractiveBox = null;

    private Camera cam;

    public void LeftClick()
    {
        Instantiate(prefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
            {
                if (hit.collider.tag == "InteractivePlane")
                {
                    Instantiate(prefab, hit.point + Vector3.up * 0.5f, Quaternion.identity);
                }
                else if (hit.collider.GetComponentInParent<InteractiveBox>())
                {
                    if (selectedInteractiveBox != null)
                    {
                        if (!Equals(selectedInteractiveBox, hit.collider.gameObject))
                        {
                            selectedInteractiveBox.GetComponentInParent<InteractiveBox>().AddNext(hit.collider.gameObject.GetComponentInParent<InteractiveBox>());
                        }
                        selectedInteractiveBox = null;
                    }
                    else
                    {
                        selectedInteractiveBox = hit.collider.gameObject;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
            {
                if (hit.collider.GetComponentInParent<InteractiveBox>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
