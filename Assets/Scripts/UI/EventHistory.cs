using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public struct RelocationEvent
{
    public WorldElement element;
    public Vector3 oldPosition;
    public Quaternion oldRotation;

    public RelocationEvent(WorldElement element, Vector3 oldPosition, Quaternion oldRotation)
    {
        this.element = element;
        this.oldPosition = oldPosition;
        this.oldRotation = oldRotation;
    }
}

public class EventHistory : MonoBehaviour
{
    public static EventHistory instance;

    [SerializeField] Transform m_container;
    [SerializeField] Button m_buttonPrefab;

    private List<GameObject> m_events = new List<GameObject>();
    private List<RelocationEvent> m_relocationEvents = new List<RelocationEvent>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddRelocationEvent(WorldElement element, Vector3 oldPosition, Quaternion oldRotation)
    {
        Button button = Instantiate(m_buttonPrefab, m_container);
        int index = m_events.Count;
        button.GetComponentInChildren<Text>().text = $"Relocated {element.name} to {element.transform.position}";
        button.onClick.AddListener(() => {
            for (int i = m_events.Count - 1; i >= index; i--)
            {
                m_relocationEvents[i].element.transform.position = m_relocationEvents[i].oldPosition;
                m_relocationEvents[i].element.transform.rotation = m_relocationEvents[i].oldRotation;
                m_relocationEvents[i].element.GetComponent<PostController>()?.UpdateStockUI();
                m_relocationEvents[i].element.GetComponent<WorkerController>()?.UpdateStockUI();
                Destroy(m_events[i]);
                m_events.RemoveAt(i);
                m_relocationEvents.RemoveAt(i);
            }
        });
        m_relocationEvents.Add(new RelocationEvent(element, oldPosition, oldRotation));
        m_events.Add(button.gameObject);
    }
}
