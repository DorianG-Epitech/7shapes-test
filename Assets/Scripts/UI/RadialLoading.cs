using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLoading : MonoBehaviour
{
    [SerializeField] Image m_image;
    [SerializeField] Text m_text;
    [SerializeField] float m_duration = 1f;


    public void Reset(float duration)
    {
        m_duration = duration;
        StopAllCoroutines();
        StartCoroutine(DoUpdate());
    }

    public void SetText(string text)
    {
        m_text.text = text;
    }

    IEnumerator DoUpdate()
    {
        float t = 0f;
        while (t < m_duration)
        {
            t += Time.deltaTime;
            m_image.fillAmount = t / m_duration;
            yield return null;
        }
    }
}
