using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpController : MonoBehaviour
{
    public Transform holdAreaObj;
    public GameObject holdObj;
    private InputActionAsset inputAsset;
    private InputActionMap player;

    private Rigidbody pickUpObjRB;
    private Camera cam;
    private float dist;

    private void Start()
    {
        inputAsset = this.GetComponentInParent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        cam = GetComponent<Camera>();
        dist = Vector3.Distance(cam.transform.position, holdAreaObj.position);
    }

    private void Update()
    {

        if (player.FindAction("Interaction").triggered)
        {
            if (holdObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
                {
                    if (hit.collider.CompareTag("CompanionCube"))
                    {
                        PickUpItem(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                DropItem();
            }
        }

        if (holdObj != null)
        {
            MovePickedObj();
        }
    }

    void MovePickedObj()
    {
        Vector3 targetPosition = holdAreaObj.transform.position;

        // Check the distance between the held cube and the target position
        if (Vector3.Distance(holdObj.transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDir = targetPosition - holdObj.transform.position;
            pickUpObjRB.AddForce(moveDir * 100f);   // Apply force to move the cube towards the target position
        }

    }

    void PickUpItem(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>() == null) return;

        pickUpObjRB = pickUpObj.GetComponent<Rigidbody>();
        pickUpObjRB.useGravity = false;
        pickUpObjRB.drag = 5f;

        pickUpObjRB.transform.parent = holdAreaObj;

        holdObj = pickUpObj;
        //pickUpObjRB.constraints = RigidbodyConstraints.FreezeRotation;
        holdObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void DropItem()
    {
        pickUpObjRB.useGravity = true;
        pickUpObjRB.drag = 1;
        //pickUpObjRB.constraints = RigidbodyConstraints.None;

        pickUpObjRB.transform.parent = null;

        holdObj = null;
    }
}