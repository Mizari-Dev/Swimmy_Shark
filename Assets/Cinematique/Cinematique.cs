using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cinematique : MonoBehaviour
{
    [SerializeField]
    List<Image> images = new List<Image>();


    void Start()
    {
        foreach(Image image in images)
        {
            image.enabled = false;
        }
        StartCoroutine(Cinematiques());
    }

    IEnumerator Cinematiques()
    {
        images[0].enabled = true;

        yield return new WaitForSeconds(3f);

        images[1].enabled = true;
        images[0].enabled = false;

        yield return new WaitForSeconds(3f);

        images[2].enabled = true;
        images[1].enabled = false;

        yield return new WaitForSeconds(3f);

        images[3].enabled = true;
        images[2].enabled = false;

        yield return new WaitForSeconds(3f);

        images[4].enabled = true;
        images[3].enabled = false;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
    }

}
