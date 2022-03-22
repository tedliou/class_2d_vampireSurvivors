using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Connecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region Public
    [Header("Stats")]
    public bool IsDragging;
    public Connecter Parant;
    public Connecter Child;

    [Header("Object")]
    public GameObject ConnectedDot;
    #endregion

    #region Private
    private Board _board;
    private PlayerAction _playerAction;
    #endregion

    #region Message
    private void Awake()
    {
        _board = GetComponentInParent<Board>();
        _playerAction = new PlayerAction();
        _playerAction.Enable();
    }

    private void Start()
    {
        IsDragging = false;
        Parant = null;
        Child = null;
    }

    private void Update()
    {
        if (_board.IsDragging)
        {
            _board.UpdateConnectPosition(GetMosePosition());
        }
    }
    #endregion

    public Vector2 GetMosePosition() => _playerAction.Player_Map.Mouse.ReadValue<Vector2>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_board.IsDragging)
        {
            // ����Q�h���s�u
            if (Parant != null) return;

            // ����s��ۤv
            if (_board.ParantConnecter != this)
            {
                // �^���s�u
                _board.ChildConnecter = this;

                // ø�s�u�q
                _board.ConnectLine.UpdatePosition(transform.position);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_board.IsDragging)
        {
            // ����s��ۤv
            if (_board.ParantConnecter != this)
            {
                // �^���s�u
                _board.ChildConnecter = null;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_board.IsDragging)
        {

        }
        else
        {
            _board.ConnectLine = _board.StartConnect(transform.position, this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_board.IsDragging)
        {
            _board.EndConnect();
        }
        else
        {

        }
    }
}
