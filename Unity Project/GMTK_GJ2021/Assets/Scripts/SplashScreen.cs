using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Start() 
    {
        _startButton.onClick.AddListener(LoadGameScene);    
    }

    private void OnDestroy() 
    {
        _startButton.onClick.RemoveListener(LoadGameScene);
    }

    [Button]
    private void LoadGameScene()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
