using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Board : MonoBehaviour
{
    #region Public
    [Header("Stats")]
    public bool IsDragging;
    public Connecter ParantConnecter;
    public Connecter ChildConnecter;
    public ConnectLine ConnectLine;
    #endregion

    public Transform LineParant;
    public ConnectLine ConnectLinePrefab;

    public ConnectLine StartConnect(Vector2 startPos, Connecter connecter)
    {
        ConnectLinePrefab.Create(startPos, connecter);
        ConnectLine = Instantiate(ConnectLinePrefab, LineParant);
        ParantConnecter = connecter;
        IsDragging = true;
        return ConnectLine;
    }

    public void UpdateConnectPosition(Vector2 connectPos)
    {
        if (ChildConnecter != null)
        {
            connectPos = ChildConnecter.transform.position;
        }
        ConnectLine.UpdatePosition(connectPos);
    }

    public void EndConnect()
    {
        IsDragging = false;
        if (ChildConnecter == null) Destroy(ConnectLine.gameObject);
        if (ChildConnecter)
        {
            ParantConnecter.Child = ChildConnecter;
            ChildConnecter.Parant = ParantConnecter;
        }
        ParantConnecter = null;
        ChildConnecter = null;
    }
}
