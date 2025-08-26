using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //get player 1
   
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    
    [Header("Position")]
    [SerializeField] private float height = 1f;     // how high above stage
    [SerializeField] private float distance = -15f; // fixed Z distance back
    [SerializeField] private float smooth = 5f;     // how snappy the camera is

    [Header("Zoom")]
    [SerializeField] private float minSize = 2f;    // closest zoom
    [SerializeField] private float maxSize = 10f;    // farthest zoom
    [SerializeField] private float zoomLimiter = 10f; // bigger = slower zoom change

    private Camera cam;
    
    
    //get player 2
    
    //variable for midpoint
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        if (!player1 || !player2) return;

        // midpoint between fighters
        Vector3 midpoint = (player1.position + player2.position) / 2f;

        // target position (lock to stage plane)
        Vector3 targetPos = new Vector3(midpoint.x, height, distance);

        // smooth move
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smooth);

        // zoom: based on distance between fighters
        float dist = Vector3.Distance(player1.position, player2.position);
        float targetSize = Mathf.Lerp(minSize, maxSize, dist / zoomLimiter);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * smooth);
    }
    
}
