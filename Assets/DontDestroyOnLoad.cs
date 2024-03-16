using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroyOnLoad : MonoBehaviour
{
    private EventSystem[] eventSystems;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (gameObject.GetComponent<EventSystem>() && FindObjectsByType<EventSystem>(FindObjectsSortMode.None).Length > 1)
        {
            eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
            foreach (EventSystem eventSystem in eventSystems)
            {
                if (eventSystem.gameObject != this)
                {
                    Destroy(eventSystem.gameObject);
                }
            }
        }
    }
}
