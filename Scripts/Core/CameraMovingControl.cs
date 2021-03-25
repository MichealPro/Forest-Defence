using UnityEngine;

public class CameraMovingControl : MonoBehaviour
{
    public Camera Camera;
    public bool Rotation;

    [SerializeField] float zoomSpeed;
    [SerializeField] float orthographicSizeMin;
    [SerializeField] float orthographicSizeMax;
    [SerializeField] float fovMin;
    [SerializeField] float fovMax;

    protected Plane Plane;

    private void Awake()
    {
        if (Camera == null)
        {
            Camera = Camera.main;
        }
    }

    void Update()
    {
        PcControl();

        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
        //update plane
        if (Input.touchCount >= 1)
        {
            Plane.SetNormalAndPosition(transform.up, transform.position);
        }

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //scroll
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Camera.transform.Translate(Delta1, Space.World);
            }
        }
        //pinch
        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
            // zoom
            var zoom = Vector3.Distance(pos1, pos2) / Vector3.Distance(pos1b, pos2b);
            // edge
            if (zoom == 0 || zoom > 10) return;
            // Move camera amount the mid ray
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            if (Rotation && pos2b != pos2)
            {
                Camera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
            }
        }
    }
    protected Vector3 PlanePositionDelta(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
        {
            return Vector3.zero;
        }

        var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.ScreenPointToRay(touch.position);
        if(Plane.Raycast(rayBefore,out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }
        return Vector3.zero;
    }
    protected Vector3 PlanePosition(Vector3 screenPos)
    {
        var rayNow = Camera.ScreenPointToRay(screenPos);
        if(Plane.Raycast(rayNow, out var enterNow))
        {
            return rayNow.GetPoint(enterNow);
        }
        return Vector3.zero;
    }
    private void PcControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Camera.transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Camera.transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Camera.transform.position += new Vector3(1, 0, 0);
        }
        if (Camera.orthographic)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Camera.orthographicSize += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Camera.orthographicSize -= zoomSpeed;
            }
            Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Camera.fieldOfView += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Camera.fieldOfView -= zoomSpeed;
            }
            Camera.fieldOfView = Mathf.Clamp(Camera.fieldOfView, fovMin, fovMax);
        }
    }
}
