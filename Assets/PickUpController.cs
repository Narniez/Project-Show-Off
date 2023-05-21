using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform holdAreaObj;   // The target position where the cube should be held

    public GameObject holdObj;      // The currently held cube
    private Rigidbody pickUpObjRB;  // Rigidbody component of the held cube

    private Camera cam;             // Reference to the camera component

    private void Start()
    {
        cam = GetComponent<Camera>();   // Get the camera component attached to the same game object
    }

    private void Update()
    {
        float dist = Vector3.Distance(cam.transform.position, holdAreaObj.position);

        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (holdObj == null)
            {
                // If not holding any cube, raycast to check if there's a cube in front of the camera
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
                {
                    if (hit.collider.CompareTag("CompanionCube"))
                    {
                        PickUpItem(hit.collider.gameObject);  // Pick up the cube if it has the "CompanionCube" tag
                    }
                }
            }
            else
            {
                DropItem();  // Drop the currently held cube
            }
        }

        // Move the held cube if there is one
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
        if (pickUpObj.GetComponent<Rigidbody>() == null) return;  // Check if the cube has a Rigidbody component

        pickUpObjRB = pickUpObj.GetComponent<Rigidbody>();
        pickUpObjRB.useGravity = false;
        pickUpObjRB.drag = 5f;

        pickUpObjRB.transform.parent = holdAreaObj;  // Set the hold area as the parent of the cube to move it along with the area

        holdObj = pickUpObj;  // Set the held cube
        //pickUpObjRB.constraints = RigidbodyConstraints.FreezeRotation;  // Freeze rotation of the cube when held
        holdObj.transform.localRotation = Quaternion.Euler(0,0,0);
    }

    void DropItem()
    {
        pickUpObjRB.useGravity = true;
        pickUpObjRB.drag = 1;
        //pickUpObjRB.constraints = RigidbodyConstraints.None;  // Release constraints on the cube

        pickUpObjRB.transform.parent = null;  // Remove the hold area as the parent of the cube

        holdObj = null;  // Clear the held cube
    }
}
