using System;
using UnityEngine;

public class Lasso : MonoBehaviour
{
    private void OnEnable()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
    }
}