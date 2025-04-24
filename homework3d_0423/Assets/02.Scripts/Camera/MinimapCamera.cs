using Unity.VisualScripting;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Camera _minimapCamera;

    public Transform Target;
    public float YOffset = 10f;

    private void Start()
    {
        _minimapCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = Target.position;
        newPosition.y += YOffset;

        transform.position = newPosition;

        transform.position = newPosition;
        Vector3 newEulerAngles = Target.eulerAngles;
        newEulerAngles.x = 90;
        newEulerAngles.z = 0;

        transform.eulerAngles = newEulerAngles;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ZoomIn();
        }

        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        _minimapCamera.orthographicSize--;
    }

    public void ZoomOut()
    {
        _minimapCamera.orthographicSize++;
    }
}
