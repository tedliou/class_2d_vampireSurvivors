using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ConnectLine : MonoBehaviour
{
    public Connecter Parant;
    public UILineRenderer LineRenderer;

    public void Create(Vector2 startPos, Connecter parant)
    {
        LineRenderer = GetComponent<UILineRenderer>();
        LineRenderer.Points = new Vector2[2];
        LineRenderer.Points[0] = startPos;
        LineRenderer.Points[1] = startPos;
        LineRenderer.SetAllDirty();
        Parant = parant;
    }

    public void UpdatePosition(Vector2 childPos)
    {
        LineRenderer.Points[1] = childPos;
        LineRenderer.SetAllDirty();
    }
}
