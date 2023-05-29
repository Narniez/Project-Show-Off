using UnityEngine;

public class PerfectPosition : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    public Vector3 offset;

    private void Start()
    {
        transform.localPosition = offset;
    }
}
