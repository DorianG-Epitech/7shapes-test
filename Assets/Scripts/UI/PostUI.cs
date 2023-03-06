using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostUI : MonoBehaviour
{
    [SerializeField] PostController m_depot;
    [SerializeField] RadialLoading m_stockLoading;
    private RectTransform m_canvasRect;
    
    private void Start()
    {
        m_canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        UpdatePosition();
    }

    private void Update()
    {
        m_stockLoading.SetText(m_depot.Stock.ToString());
    }

    public void ResetRadialLoading(float duration)
    {
        if (m_stockLoading.gameObject.activeSelf == false)
            m_stockLoading.gameObject.SetActive(true);
        m_stockLoading.Reset(duration);
    }

    public void UpdatePosition()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(m_depot.transform.position);
        Vector2 screenPos = new Vector2(
            ((viewportPos.x * m_canvasRect.sizeDelta.x) - (m_canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * m_canvasRect.sizeDelta.y) - (m_canvasRect.sizeDelta.y * 0.5f)));
        (transform as RectTransform).anchoredPosition = screenPos;
    }
}
