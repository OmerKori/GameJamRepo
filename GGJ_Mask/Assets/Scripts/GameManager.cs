using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static int currentLevel = 1;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public static void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level "+currentLevel);
    }
}
