using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostController : MonoBehaviour
{
    [SerializeField] PostUI m_ui;
    [SerializeField] Transform m_depot;
    [Header("Status")]
    [SerializeField] int m_stock;
    [Range(0.1f, 10f)] [SerializeField] float m_refillRate = 1;

    public Transform depot => m_depot;
    public int Stock => m_stock;

    public IEnumerator RefillStock()
    {
        m_ui.ResetRadialLoading(1f / m_refillRate);
        yield return new WaitForSeconds(1f / m_refillRate);
        m_stock += 1;
        StartCoroutine(RefillStock());
    }

    public void UpdateStockUI()
    {
        m_ui.UpdatePosition();
    }

    // takes _amount elements from the stock
    public int Pick(int _amount)
    {
        if(m_stock >= _amount)
        {
            m_stock -= _amount;
            return _amount;
        }
        int remaining = m_stock;
        m_stock = 0;
        return remaining;
    }

    // adds _amount elements to the stock
    public void Drop(int _amount)
    {
        m_stock += _amount;
    }
}
