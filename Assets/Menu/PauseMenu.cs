using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Time.timeScale = 0;
            canvas.enabled = !canvas.enabled;
        }
    }

    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
