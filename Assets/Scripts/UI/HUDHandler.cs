using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public enum HUDCursor
{
    Default, Look, PickUp
}

public class HUDHandler : MonoBehaviour
{
    public Image cursorImg;
    public Camera mainCamera;
    public HUDCursor _testCursor = HUDCursor.Default;
    [SerializeField]
    private Sprite[] _cursors;
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
    }

    // Update is called once per frame
    void Update()
    {

        if (_isRayCasting)
        {
            _ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(_ray.origin, _ray.direction * _rayMaxDistance, Color.magenta);
            if (Physics.Raycast(_ray, out _hit, _rayMaxDistance))
            {
                SwitchCursor(HUDCursor.Look);
            } else {
                SwitchCursor(HUDCursor.Default);
            }
        }

    }

    void SwitchCursor(HUDCursor cursor)
    {
        cursorImg.sprite = _cursors[(int)cursor];
    }
}
