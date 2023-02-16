using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {get; private set;}
    public GameObject door;
    public GameObject EndPanel;
    public TextMeshProUGUI gameOverTxt;
    public TextMeshProUGUI youWinTxt;
    public GameInputSystem gameInput;
    private List<Scroll_PU> m_scrolls_to_respawn;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
    }

    private void Awake() {
        if (Instance !=  null)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        gameInput.LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            switchPause();
        }
    }

    public void GameOver()
    {
        PauseGame();
        HUDHandler.Instance.SwitchCursor(HUDCursor.Fight);
        EndPanel.SetActive(true);
        gameOverTxt.gameObject.SetActive(true);
    }

    public void GameWon()
    {
        PauseGame();
        HUDHandler.Instance.SwitchCursor(HUDCursor.Fight);
        EndPanel.SetActive(true);
        youWinTxt.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        gameInput.FreeCursor();
        HUDHandler.Instance.SetRayCasting(false);
        isPaused = true;
    }

    public void switchPause()
    {
        if (isPaused)
        {
            isPaused = false;
            HUDHandler.Instance.SetRayCasting(true);
            gameInput.LockCursor();
        } else {
            PauseGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif

    }

    public void openDoor()
    {
        door.gameObject.transform.Translate(0f, 1.5f, 0f);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScrollToRespawn(Scroll_PU scroll_pu)
    {
        if (m_scrolls_to_respawn == null)
        {
            m_scrolls_to_respawn = new List<Scroll_PU>();
        }
        Scroll_PU copiedScroll = Instantiate(scroll_pu, scroll_pu.gameObject.transform.position, Quaternion.identity);
        copiedScroll.gameObject.SetActive(false);
        m_scrolls_to_respawn.Add(copiedScroll);
    }

    public void RespawnScrolls()
    {  
        foreach (Scroll_PU pu in m_scrolls_to_respawn)
        {
            pu.gameObject.SetActive(true);
        } 
        m_scrolls_to_respawn = null;

    }
}
