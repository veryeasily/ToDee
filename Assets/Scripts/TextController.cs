using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public void OnPlayerMove()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

        }
    }
}
