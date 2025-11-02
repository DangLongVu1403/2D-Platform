using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameController : MonoBehaviour
{
    Vector3 checkpointPos;
    Rigidbody2D playerRb;
    public ParticleController particleController;
    public Animator flashAnimator;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] private AudioSource winnerSound;
    private GameObject panelVictory;
    // Start is called before the first frame update
    void Start()
    {
        checkpointPos = transform.position;
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap") || collision.CompareTag("DeadZone"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("WinTag"))
        {
            Winner();
        }

    }

    void Winner()
    {
        winnerSound.PlayOneShot(winnerSound.clip);
        StartCoroutine(Transition(0.5f));
    }

    IEnumerator Transition(float duration)
    {
        // Nếu chưa có tham chiếu, cố gắng tìm lại PanelVictory
        if (panelVictory == null)
        {
#if UNITY_2023_1_OR_NEWER
            // Tìm tất cả Canvas kể cả trong DontDestroyOnLoad
            var allCanvas = GameObject.FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var canvas in allCanvas)
            {
                Transform panel = canvas.transform.Find("PanelVictory");
                if (panel != null)
                {
                    panelVictory = panel.gameObject;
                    break;
                }
            }
#endif
        }

        if (panelVictory != null)
        {
            panelVictory.SetActive(true);
            Animator panelAnimator = panelVictory.GetComponent<Animator>();
            if (panelAnimator != null)
                panelAnimator.SetTrigger("Victory");
        }
        else
        {
            Debug.LogWarning("⚠️ Không tìm thấy PanelVictory!");
        }

        yield return new WaitForSeconds(2f);
        panelVictory.SetActive(false);
        flashAnimator.SetTrigger("Transition");
        yield return new WaitForSeconds(duration);

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        string numberPart = System.Text.RegularExpressions.Regex.Match(currentSceneName, @"\d+$").Value;
        int currentLevel = int.Parse(numberPart);
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetInt("unlockLevel", nextLevel);
        string nextSceneName = currentSceneName.Replace(numberPart, nextLevel.ToString());
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    void Die()
    {
        dieSound.PlayOneShot(dieSound.clip);
        flashAnimator.SetTrigger("Flash");
        particleController.PlayDieParticle();
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.linearVelocity = Vector2.zero;
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }

    public void UpdateCheckPoint(Vector3 pos)
    {
        checkpointPos = pos;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
