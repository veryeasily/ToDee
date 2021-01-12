using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject trackedCamera;
    private float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 camPosition = trackedCamera.transform.position;

        offsetX = transform.position.x - camPosition.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 camPosition = trackedCamera.transform.position;

        transform.position = new Vector2(camPosition.x + offsetX, camPosition.y);
    }
}
