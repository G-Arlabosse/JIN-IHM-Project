using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    const int ID_GAME_SCENE = 2;
    const int ID_MENU_SCENE = 1;

    public static ScenesManager instance;

    private void Start()
    {
        instance = this;

        LoadScene(ID_MENU_SCENE);
    }

 private void LoadScene(int idScene)
    {
        SceneManager.LoadScene(idScene, LoadSceneMode.Additive);
    }

    private void UnloadMenuScene(int idScene)
    {
        SceneManager.UnloadSceneAsync(idScene);
    }
    public void openMenu()
    {
        LoadScene(ID_MENU_SCENE);
        UnloadMenuScene(ID_GAME_SCENE);
    }

    public void openGame()
    {
        LoadScene(ID_GAME_SCENE);
        UnloadMenuScene(ID_MENU_SCENE);
    }
}
