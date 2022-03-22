using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Connecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool IsConnected;
    public bool IsDrag;
    public GameObject ActiveDot;
    public GameObject ConnectedDot;
    public UILineRenderer ConnectLine;

    private void Start()
    {
        ActiveDot.SetActive(false);
        ConnectedDot.SetActive(IsConnected);
    }

    private void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveDot.SetActive(true);
        ConnectedDot.SetActive(IsConnected);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActiveDot.SetActive(false);
        ConnectedDot.SetActive(IsConnected);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActiveDot.SetActive(true);
        ConnectedDot.SetActive(IsConnected);
        IsDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsConnected)
        {
            ConnectedDot.SetActive(true);
        }
        IsDrag = false;
    }
}
