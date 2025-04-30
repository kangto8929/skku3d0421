using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public CameraRotate CameraRotateScript;

    public Transform FpsView;
    public Transform TpsView;
    public Transform QuarterView;

    public float TransitionSpeed = 5f;

    private Transform _currentTarget;

    private void Start()
    {
        _currentTarget = TpsView;//초기 시점
        CameraRotateScript.enabled = false;
    }

    private void Update()
    {
        _currentTarget = FpsView;
        CameraRotateScript.enabled = true;

        if (_currentTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, _currentTarget.position, TransitionSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, _currentTarget.rotation, TransitionSpeed);
        }
    }

    /*private void Update()
    {
        

        // interpoling, smoothing 기법이 들어갈 예쩡
        transform.position = Target.position;

        
    }*/

}

/*using Unity.Mathematics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraRotate CameraRotateScript;

    public Transform FpsView;
    public Transform TpsView;
    public Transform QuarterView;

    public float TransitionSpeed = 5f;

    private Transform _currentTarget;

    private void Start()
    {
        _currentTarget = TpsView;//초기 시점
        CameraRotateScript.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            _currentTarget = FpsView;
            CameraRotateScript.enabled = true;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            _currentTarget = TpsView;
            CameraRotateScript.enabled = false;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _currentTarget = QuarterView;
            CameraRotateScript.enabled = false;
        }

        if(_currentTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, _currentTarget.position, TransitionSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, _currentTarget.rotation, TransitionSpeed);
        }
    }
}*/



