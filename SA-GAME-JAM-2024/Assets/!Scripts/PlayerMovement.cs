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

    public bool endOfLevel = false;

    private Rigidbody rb;
    private Vector3[] evaluatedPoints;
    private bool isPlayingSparkleSound = false;

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

        HandleSparkleSound();
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
            if (endOfLevel == true) { return; }
            Debug.Log("Player has reached the end of the spline!");
            endOfLevel = true;
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
        EventsManager.TriggerEvent("EnableLevelButtons");
        Debug.Log("Loading next scene...");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level 1")
        {
            SoundManager.instance.StopAllLoopingEffects();
            SoundManager.instance.PlaySFX("mtlpp");
        }
    }

    void HandleSparkleSound()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving && !isPlayingSparkleSound && endOfLevel != true)
        {
            SoundManager.instance.loopingSfxSource.loop = true;
            SoundManager.instance.PlayLoopingSFX("sparkle");
            SoundManager.instance.loopingSfxSource.volume = 1.5f;
            SoundManager.instance.loopingSfxSource.Play();
            isPlayingSparkleSound = true;
        }
        else if (!isMoving && isPlayingSparkleSound)
        {
            SoundManager.instance.loopingSfxSource.Stop();
            SoundManager.instance.loopingSfxSource.loop = false;
            isPlayingSparkleSound = false;
        }
    }

}
