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
    public Scroll_PU[] Scrolls_PU;
    public GameObject door;
    public GameObject EndPanel;
    public GameObject PausePanel;
    public TextMeshProUGUI gameOverTxt;
    public TextMeshProUGUI youWinTxt;
    public GameInputSystem gameInput;
    private List<Scroll_PU> m_scrolls_to_respawn;
    private bool isPaused;
    [SerializeField] private PlayerController _playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        PausePanel.SetActive(false);
        foreach (Scroll_PU scroll in Scrolls_PU)
        {
            scroll.gameObject.SetActive(false);
            AddScrollToRespawn(scroll);
        }
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
        _playerCtrl.canAttack = false;
        gameInput.FreeCursor();
        HUDHandler.Instance.SetRayCasting(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().CanMove = false;
        isPaused = true;
    }

    public void switchPause()
    {
        if (isPaused)
        {
            isPaused = false;
            HUDHandler.Instance.SetRayCasting(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().CanMove = true;
            _playerCtrl.canAttack = true;
            gameInput.LockCursor();
            PausePanel.SetActive(false);
        } else {
            PauseGame();
            HUDHandler.Instance.SetRayCasting(false);
            PausePanel.SetActive(true);
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
        Animator doorAnim = door.GetComponentInChildren<Animator>();
        doorAnim.SetTrigger("Open_tr");
        door.GetComponent<InfosPoint>().isInteractable = false;
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
        m_scrolls_to_respawn.Add(scroll_pu);
    }

    public void RespawnScrolls()
    {  
        if (m_scrolls_to_respawn != null) 
        {
            foreach (Scroll_PU pu in m_scrolls_to_respawn)
            {
                pu.gameObject.SetActive(true);
            } 
            m_scrolls_to_respawn = null;
        }

    }
}
