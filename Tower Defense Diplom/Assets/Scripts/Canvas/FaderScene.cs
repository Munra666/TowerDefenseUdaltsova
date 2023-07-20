using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaderScene : MonoBehaviour
{
    [Tooltip("Черный экран с визуализацией перехода")] public Image blackout;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Конец сцены
    /// </summary>
    /// <param name="scene"></param>
    public void FadeTo(int scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    /// <summary>
    /// Начало сцены
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Переход на другую сцену
    /// </summary>
    /// <param name="scene">Следующая сцена</param>
    /// <returns></returns>
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
