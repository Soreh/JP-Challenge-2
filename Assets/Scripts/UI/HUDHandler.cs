using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public enum HUDCursor
{
    Default, Look, PickUp, Fight
}

public class HUDHandler : MonoBehaviour
{
    public static HUDHandler Instance {get; private set;}
    public Image cursorImg;
    public Camera mainCamera;
    public GameObject LogView;
    public RectTransform ViewPort;
    public TextMeshProUGUI infoText;
    public PlayerController player;
    public TextMeshProUGUI textPrefab;
    public Scrollbar scroll;

    [SerializeField]
    private Sprite[] _cursors;
    [SerializeField] Image _leftIconePlaceholder;
    [SerializeField] Image _rightIconePlaceholder;
    [SerializeField] Sprite[] _leftIcones;
    [SerializeField] Sprite[] _rightIcones;
    private bool _showDefaultIcones = true;
    private HUDCursor _currentCursor;
    private float _messageTimer;
    public float defautlMessageTimer = 10f;
    private string _lastMessageLog = "";
    private Ray _ray;
    private RaycastHit _hit;
    private GameObject _lastHit;
    [SerializeField] private float _rayMaxDistance = 2f;
    [SerializeField] private bool _isRayCasting = true;
    // Start is called before the first frame update
   
    void Start()
    {
        if ( mainCamera == null )
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        if ( player == null )
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        SetDefaultIcones();
        SwitchCursor(HUDCursor.Default);
        _messageTimer = defautlMessageTimer;
    }

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( _showDefaultIcones) {
            SetDefaultIcones();
        }
        
        _messageTimer -= Time.deltaTime;

        if (_messageTimer < 0) {
            EmptyLogs();
        }

        if (_isRayCasting)
        {
            ManageRayCast();
        }

    }
    public void SetRayCasting(bool isRaycasting)
    {
        _isRayCasting = isRaycasting;
    }

    public void SwitchCursor(HUDCursor cursor)
    {
        if (_currentCursor == cursor)
        {
            return;
        } else {
            cursorImg.sprite = _cursors[(int)cursor];
            _currentCursor = cursor;
        }
            
    }

    public void SetDefaultIcones()
    {
        if (player._leftHand == null ) {
            _leftIconePlaceholder.color = new Color(0,0,0,0);
        } else {
            _leftIconePlaceholder.sprite = player._leftHand.icone;
            _leftIconePlaceholder.color = new Color(255,255,255,1);
        }
        if (player._righttHand == null) {
            _rightIconePlaceholder.color = new Color(0,0,0,0);
        } else {
            _rightIconePlaceholder.sprite = player._righttHand.icone;
            _rightIconePlaceholder.color = new Color(255,255,255,1);
        }
    }

    public void SetLeftIcone(int index)
    {
        _leftIconePlaceholder.sprite = _leftIcones[index];
        _leftIconePlaceholder.color = new Color(255,255,255,1);
    }

    public void SetRightIcone(int index)
    {
        _rightIconePlaceholder.sprite = _rightIcones[index];
        _rightIconePlaceholder.color = new Color(255,255,255,1);
    }

    public void LogText(string txt)
    {
        if (txt != _lastMessageLog) {
            _lastMessageLog = txt;
            // infoText.text += "<br><b>New message :</b><br>" + txt;
            // LogView.SetActive(true);
            // _messageTimer = defautlMessageTimer;
            TextMeshProUGUI newText = Instantiate(textPrefab, LogView.transform);
            newText.text += txt;
            StartCoroutine(DelayScroll());
        }
    }

    private void EmptyLogs()
    {
        // infoText.text = "";
        // _lastMessageLog = "";
        // LogView.SetActive(false);
    }

    void ManageRayCast()
    {
        _ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(_ray.origin, _ray.direction * _rayMaxDistance, Color.magenta);
        if (Physics.Raycast(_ray, out _hit, _rayMaxDistance))
        {
            GameObject hitGameObject = _hit.transform.gameObject;
            AbstractMouseTrigger ms = _hit.transform.gameObject.GetComponent<AbstractMouseTrigger>();
            if (hitGameObject.CompareTag("MouseTrigger")) {
                player.canAttack = false;
                _showDefaultIcones = false;
                if (_lastHit == null || _lastHit != hitGameObject ) {
                    _lastHit = hitGameObject;
                    ms.HandleHover();
                }
                if (Mouse.current.leftButton.wasPressedThisFrame) {
                    ms.HandLeftClick();
                }
                if (Mouse.current.rightButton.wasPressedThisFrame) {
                    ms.HandleRightClick();
                }
            } else {
                _lastHit = hitGameObject;
                player.canAttack = true;
                SwitchCursor(HUDCursor.Default);
                SetDefaultIcones();
            }
        } else {
            _lastHit = null;
            player.canAttack = true;
            SwitchCursor(HUDCursor.Default);
            SetDefaultIcones();
        }
    }

    IEnumerator KeepDisplayingMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        LogText("");
    }

    IEnumerator DelayScroll()
    {
        yield return new WaitForSeconds(0.03f);
        scroll.value = 0;
    }
}
