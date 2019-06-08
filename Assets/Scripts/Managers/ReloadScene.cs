using UnityEngine.SceneManagement;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }
}
