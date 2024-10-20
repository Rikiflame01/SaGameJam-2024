using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cursor Settings")]
    public Texture2D defaultCursorTexture;

    public Vector2 cursorHotspot = Vector2.zero;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetCursor(defaultCursorTexture);
    }

    public void SetCursor(Texture2D cursorTexture)
    {
        if (cursorTexture == null)
        {
            Debug.LogWarning("No cursor texture provided.");
            return;
        }

        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void UseDefaultCursor()
    {
        SetCursor(defaultCursorTexture);
    }
}
