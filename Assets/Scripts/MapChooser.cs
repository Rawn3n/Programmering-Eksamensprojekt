using UnityEngine;
using UnityEngine.SceneManagement;


public class MapChooser : MonoBehaviour
{
    public void RandomLevel()
    {
        SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCount)); // test sceneCountInBuildSettings
    }
    
}
