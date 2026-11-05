using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{

    private Button playButton;
    private Button quitButton;

    [SerializeField] private UIDocument uIDocument;

    private ScenesManager scenesManager;
    

    //[SerializeField]
    //private 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (uIDocument == null)
        {
            uIDocument = GetComponent<UIDocument>();

            Assert.IsNotNull(uIDocument);
        }

        playButton = uIDocument.rootVisualElement.Q("Play") as Button;
        quitButton = uIDocument.rootVisualElement.Q("Quit") as Button;

        Assert.IsNotNull(playButton);
        Assert.IsNotNull(quitButton);

        playButton.RegisterCallback<ClickEvent>(startGame);
        quitButton.RegisterCallback<ClickEvent>(quitGame);
    }

    private void startGame(ClickEvent evt)
    {
        ScenesManager.instance.openGame();
    }

    private void quitGame(ClickEvent evt)
    {   
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
