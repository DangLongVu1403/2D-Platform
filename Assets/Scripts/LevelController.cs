using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        int unlockLevel = PlayerPrefs.GetInt("unlockLevel",1);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 > unlockLevel)
            {
                buttons[i].interactable = false;
            }
        }
    }
    public void OpenLevel(int level)
    {
        string scene = "Level" + level;
        SceneManager.LoadScene(scene);
    }

    //public void UpdateButton(int level)
    //{
    //    buttons[level].interactable = true;
    //    PlayerPrefs.SetInt("unlockLevel", level + 1);
    //}
}
