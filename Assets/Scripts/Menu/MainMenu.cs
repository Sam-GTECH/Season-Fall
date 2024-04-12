using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
}
