using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Camera main;
    public GameObject actualGun;

    private void Awake()
    {
        actualGun = transform.GetChild(0).gameObject;
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag("DroppedGun"))
                {
                    ChangeGun(hitInfo.collider.gameObject, hitInfo.point);
                }

                if (hitInfo.collider.gameObject.CompareTag("Loot"))
                {
                    hitInfo.collider.gameObject.GetComponent<lootScript>().Loot();
                }
            }
        }
    }

    void ChangeGun(GameObject newGun, Vector3 position)
    {        
        GameObject temp = Instantiate(newGun.GetComponent<DroppedGunScript>().replacedGun, transform);   
        GameObject temp2 = Instantiate(Resources.Load("dropped" + actualGun.name) as GameObject);
        position.y += 2;
        temp2.transform.position = position;
        temp.transform.localPosition = Vector3.zero;
        Destroy(actualGun);
        actualGun = temp;
        temp.name = temp.tag;
        Destroy(newGun);
    }
}
