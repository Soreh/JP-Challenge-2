using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public enum HUDCursor
{
    Default, Look, PickUp
}

public class HUDHandler : MonoBehaviour
{
    public Image cursorImg;
    public Camera mainCamera;
    public TextMeshProUGUI infoText;
    public HUDCursor _testCursor = HUDCursor.Default;

    [SerializeField]
    private Sprite[] _cursors;
    private HUDCursor _currentCursor;
    private float _messageTimer;
    public float defautlMessageTimer = 10f;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private float _rayMaxDistance = 2f;
    [SerializeField] private bool _isRayCasting = true;
    // Start is called before the first frame update
   
    void Start()
    {
        if ( mainCamera == null )
        {
            mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        SwitchCursor(HUDCursor.Default);
        _messageTimer = defautlMessageTimer;
    }

    // Update is called once per frame
    void Update()
    {

        _messageTimer -= Time.deltaTime;

        if (_messageTimer < 0) {
            infoText.text = "";
        }

        if (_isRayCasting)
        {
            SwitchCursor(HUDCursor.Default);
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

    public void LogText(string txt, float duration = 0f)
    {
        infoText.text += "<br><b>New message :</b><br>" + txt;
        _messageTimer = defautlMessageTimer;
    }

    void ManageRayCast()
    {
        _ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(_ray.origin, _ray.direction * _rayMaxDistance, Color.magenta);
        if (Physics.Raycast(_ray, out _hit, _rayMaxDistance))
        {
            if (_hit.transform.gameObject.CompareTag("MouseTrigger")) {
                _hit.transform.gameObject.GetComponent<MouseTrigger>().ManageClickAndHover();
            }
        }
    }

    IEnumerator KeepDisplayingMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        LogText("");
    }
}
