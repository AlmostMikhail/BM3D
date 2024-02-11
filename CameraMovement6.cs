using UnityEngine;

public class CameraMovement6 : MonoBehaviour
{
    public Transform pivot;  // The pivot point to orbit around
    public float orbitSpeed = 5f;
    public float panSpeed = 2f;
    public float zoomSpeed = 5f;
    public float minFOV = 20f;
    public float maxFOV = 60f;
    private Vector3 lastMousePosition;
    private Vector3 lastRightMousePosition;
    public float scrollSpeed = 5.0f;
    private float startPinchDistance;
    private Camera mainCamera;
    private float startFOV;

    void Start()
    {
        mainCamera = Camera.main;

    }
    private void Update()
    {

        // // Pan the camera
        // if (Input.GetMouseButton(1))
        // {
        //     if (lastRightMousePosition != Vector3.zero)
        //     {
        //         float mouseX = Input.GetAxis("Mouse X");
        //         float mouseY = Input.GetAxis("Mouse Y");

        //         Vector3 pan = new Vector3(mouseX, mouseY, 0) * panSpeed;
        //         transform.Translate(pan);
        //     }
        //     lastRightMousePosition = Input.mousePosition;
        // }
        // else
        // {
        //     lastRightMousePosition = Vector3.zero;
        // }

        // Zoom in and out
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                startPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                startFOV = mainCamera.fieldOfView;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float deltaDistance = startPinchDistance - currentPinchDistance;

                // Calculate zoom amount based on the change in pinch distance
                float zoomAmount = deltaDistance * zoomSpeed;

                // Calculate new field of view
                float newFOV = startFOV + zoomAmount;

                // Clamp the field of view to avoid extreme zoom values
                newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);

                // Apply the new field of view to the camera
                mainCamera.fieldOfView = newFOV;
            }
        }
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta != 0)
        {
            Camera mainCamera = Camera.main;
            float currentFOV = mainCamera.fieldOfView;

            // Adjust the field of view based on the scroll input
            currentFOV -= scrollDelta * zoomSpeed;

            // Clamp the field of view to avoid extreme zoom values
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);

            // Apply the new field of view to the camera
            mainCamera.fieldOfView = currentFOV;
        }
    }
}