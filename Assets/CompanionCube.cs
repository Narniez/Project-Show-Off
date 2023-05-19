using UnityEngine;

public class CompanionCube : MonoBehaviour
{
    public Transform holdAreaObj;

    private Transform pickUpObj;
    private Rigidbody pickUpObjRB;

    private float distInteraction = 2f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pickUpObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, distInteraction)) {
                    if (hit.collider.CompareTag("CompanionCube")) {
                        PickUpItem(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void PickUpItem(GameObject pickUpObj) {
        if (pickUpObj.GetComponent<Rigidbody>() == null) return;

        pickUpObjRB = pickUpObj.GetComponent<Rigidbody>();
        pickUpObjRB.useGravity = false;
        pickUpObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        pickUpObj.transform.parent = holdAreaObj;
    }
}
