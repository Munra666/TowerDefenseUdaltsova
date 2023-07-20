using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Окно с настройками")] public Animator settingCanvas;
    [Tooltip("Окно с выбором новой игры")] public Animator newGameCanvas;
    [Space(5)]
    [Tooltip("Кнопка продолжить")] public Button continueButton;
    [Space(5)]
    [Tooltip("Переход сцен")] public FaderScene faderScene;

    private int levelToLoad = 1; //Индекс загружаемой сцены в зависимости от выбора карты

    private void Start()
    {
        if (PlayerPrefs.GetInt("DeleteAllSave") == 1)
            DeleteAllSave();

        if (PlayerPrefs.HasKey("SceneIndex"))
            continueButton.interactable = true;
        else
            continueButton.interactable = false;
    }

    /// <summary>
    /// Загрузка новой игры
    /// </summary>
    public void NewGame()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        settingCanvas.SetBool("isOpen", false);
        newGameCanvas.SetBool("isOpen", true);
    }

    public void StartGame()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);

        DeleteAllSave();

        faderScene.FadeTo(levelToLoad);
    }

    public void Continue()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        faderScene.FadeTo(PlayerPrefs.GetInt("SceneIndex"));
    }

    /// <summary>
    /// Открытие и закрытие окна настроек
    /// </summary>
    public void Setting()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        settingCanvas.SetBool("isOpen", true);
        newGameCanvas.SetBool("isOpen", false);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void Quit()
    {
        AudioManager.Instance.OneShotPlay(AudioManager.Instance.clickButton);
        Application.Quit();
    }

    /// <summary>
    /// Выбор карты
    /// </summary>
    /// <param name="indexScene">Индекс сцены с выбранной картой</param>
    public void ToggleMap(int indexScene)
    {
        levelToLoad = indexScene;
    }

    /// <summary>
    /// Удаляет все сохранения кроме лучшего результата
    /// </summary>
    private void DeleteAllSave()
    {
        int bestResult = PlayerPrefs.GetInt("bestResult");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("bestResult", bestResult);
    }
}
