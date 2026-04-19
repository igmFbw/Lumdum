using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineHolder : Singleton<OutLineHolder>
{
    public Material material;
    [SerializeField] private float lerpDuration = 0.3f;

    private Coroutine colorLerpCoroutine;

    public void SetColor(Color color)
    {
        if (material == null) return;

        // 如果正在渐变，先停掉
        if (colorLerpCoroutine != null)
        {
            StopCoroutine(colorLerpCoroutine);
            colorLerpCoroutine = null;
        }

        material.SetColor("_OutlineColor", color);
    }

    public void SetColorSmooth(Color targetColor)
    {
        if (material == null) return;

        if (colorLerpCoroutine != null)
            StopCoroutine(colorLerpCoroutine);

        Color startColor = material.GetColor("_OutlineColor");
        colorLerpCoroutine = StartCoroutine(LerpColorCoroutine(startColor, targetColor, lerpDuration));
    }

    private IEnumerator LerpColorCoroutine(Color from, Color to, float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            Color currentColor = Color.Lerp(from, to, t);

            material.SetColor("_OutlineColor", currentColor);
            yield return null;
        }

        material.SetColor("_OutlineColor", to);
        colorLerpCoroutine = null;
    }
}