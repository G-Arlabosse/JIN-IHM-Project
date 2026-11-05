using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField]
    private Vector2 cameraPosition;

    [SerializeField]
    private Camera cam;

    private bool cameraMoving;
    private float timeSinceTransition;
    private readonly float transitionTime = 0.2f;


    private void Start()
    {
        //Assert.IsNotNull(cam);
        cameraMoving = false;
        timeSinceTransition = 0f;
    }

    private void Update()
    {
        if (cameraMoving)
        {
            timeSinceTransition += Time.deltaTime;
            float t = Mathf.Clamp01(timeSinceTransition / transitionTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cameraPosition.x, cameraPosition.y, -10), t);
            if (t >= 1f)
            {
                cameraMoving = false;
                timeSinceTransition = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cam.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
        cameraMoving = true;
    }
}
