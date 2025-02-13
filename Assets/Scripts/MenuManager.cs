using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void loadTestScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
