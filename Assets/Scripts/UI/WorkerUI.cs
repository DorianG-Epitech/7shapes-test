using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerUI : MonoBehaviour
{
    [SerializeField] WorkerController m_worker;
    [SerializeField] Text m_text;
    private RectTransform m_canvasRect;

    private void Start()
    {
        m_canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdatePosition();
        m_text.text = m_worker.Stock.ToString();
    }

    public void UpdatePosition()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(m_worker.transform.position);
        Vector2 screenPos = new Vector2(
            ((viewportPos.x * m_canvasRect.sizeDelta.x) - (m_canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * m_canvasRect.sizeDelta.y) - (m_canvasRect.sizeDelta.y * 0.5f)));
        (transform as RectTransform).anchoredPosition = screenPos;
    }
}
