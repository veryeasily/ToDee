using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float speed = 1.0f;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    float GetPercentagePlayerAcrossScreen()
    {
        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float leftEdge = transform.position.x - halfWidth;
        float width = halfWidth * 2.0f;

        return (player.transform.position.x - leftEdge) / width;
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        // Apparently Vectors are pass by value
        Vector3 endPosition = transform.position;
        float percentage = GetPercentagePlayerAcrossScreen();
        float width = mainCamera.orthographicSize * mainCamera.aspect * 2.0f;

        if (percentage < 0.25f)
        {
            endPosition.x = player.transform.position.x + 0.25f * width;
        }
        else if (percentage > 0.75f)
        {
            endPosition.x = player.transform.position.x - 0.25f * width;
        }

        transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * speed);
    }
}
