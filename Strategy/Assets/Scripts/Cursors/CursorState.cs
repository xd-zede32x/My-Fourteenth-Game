using UnityEngine;

public class CursorState : MonoBehaviour
{
    public void ChangeCursorState(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}