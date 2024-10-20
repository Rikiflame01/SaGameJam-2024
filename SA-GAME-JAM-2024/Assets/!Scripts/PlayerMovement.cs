using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public SplineContainer splinePath;
    public float maxDistanceFromSpline = 2f;
    public float distanceToReachEnd = 1f;
    public int splineResolution = 100;
    public string sceneToLoad; 

    private Rigidbody rb;
    private Vector3[] evaluatedPoints;

    private void OnEnable()
    {
        EventsManager.StartListening("ResetPlayer", TeleportToStart);
    }

    private void OnDisable()
    {
        EventsManager.StopListening("ResetPlayer", TeleportToStart);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        EvaluateSplinePoints();

        Vector3 startPosition = splinePath.EvaluatePosition(0f);
        rb.position = startPosition;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(moveX, 0, moveZ);

        MovePlayer(movement);

        CheckPlayerDistanceFromSpline();
    }

    void MovePlayer(Vector3 movement)
    {
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    void EvaluateSplinePoints()
    {
        evaluatedPoints = new Vector3[splineResolution + 1];
        for (int i = 0; i <= splineResolution; i++)
        {
            float t = (float)i / splineResolution;
            evaluatedPoints[i] = splinePath.EvaluatePosition(t);
        }
    }

    void CheckPlayerDistanceFromSpline()
    {
        Vector3 playerPosition = rb.position;

        float minDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        foreach (var point in evaluatedPoints)
        {
            float distance = Vector3.Distance(playerPosition, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }


        if (minDistance > maxDistanceFromSpline)
        {
            Debug.Log("Player moved too far from the path! Teleporting to start.");
            TeleportToStart();
        }

        if (Vector3.Distance(playerPosition, evaluatedPoints[splineResolution]) <= distanceToReachEnd)
        {
            Debug.Log("Player has reached the end of the spline!");
            LoadNextScene();
        }
    }

    void TeleportToStart()
    {
        Vector3 startPosition = splinePath.EvaluatePosition(0f);
        rb.position = startPosition;
        EventsManager.TriggerEvent("ResetButtons");
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene to load not set!");
        }
    }
}
