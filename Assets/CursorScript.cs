using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Sprite idleSprite, holdSprite; //Idle, MouseDown
    [SerializeField] private Image icon;

    [SerializeField] private float offsetX, offsetY;
    void Start()
    {
        icon = GetComponent<Image>();
        Cursor.visible = false;
        icon.sprite = idleSprite;
        FindCanvas();
    }
    void Update()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x - offsetX, cursorPos.y - offsetY);
        if (Input.GetMouseButtonDown(0))
        {
            icon.sprite = holdSprite;
        }
        if (Input.GetMouseButtonUp(0))
        {
            icon.sprite = idleSprite;
        }
    }

    public void FindCanvas()
    {
        this.transform.SetParent(FindAnyObjectByType<Canvas>().transform);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }
}
