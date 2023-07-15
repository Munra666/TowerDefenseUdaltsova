using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaderScene : MonoBehaviour
{
    public Image blackout;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(int scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        float alfa = 1f;

        while(alfa > 0f)
        {
            alfa -= Time.deltaTime;
            blackout.color = new Color(0f, 0f, 0f, alfa);
            yield return 0;
        }
    }

    private IEnumerator FadeOut(int scene)
    {
        float alfa = 0f;

        while (alfa < 1f)
        {
            alfa += Time.deltaTime;
            blackout.color = new Color(0f, 0f, 0f, alfa);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
