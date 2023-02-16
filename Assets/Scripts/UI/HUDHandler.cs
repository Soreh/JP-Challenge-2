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
    public TextMeshProUGUI infoText;
    public PlayerController player;

    [SerializeField]
    private Sprite[] _cursors;
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
        player.canAttack = true;
        _messageTimer -= Time.deltaTime;

        if (_messageTimer < 0) {
            EmptyLogs();
        }

        if (_isRayCasting)
        {
            ManageRayCast();
        }

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

    public void LogText(string txt)
    {
        if (txt != _lastMessageLog) {
            _lastMessageLog = txt;
            infoText.text += "<br><b>New message :</b><br>" + txt;
            _messageTimer = defautlMessageTimer;
        }
    }

    private void EmptyLogs()
    {
        infoText.text = "";
        _lastMessageLog = "";
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
                SwitchCursor(HUDCursor.Default);
            }
        } else {
            _lastHit = null;
            SwitchCursor(HUDCursor.Default);
        }
    }

    IEnumerator KeepDisplayingMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        LogText("");
    }
}
