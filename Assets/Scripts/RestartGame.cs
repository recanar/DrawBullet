using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()//restart game when button pressed
    {
        SceneManager.LoadScene(0);
    }
}
