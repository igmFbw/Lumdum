using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public List<Enemy> enemies;
    [Header("渐变消失时间")]
    public float fadeDuration = 1f;

    // 外部调用：销毁所有敌人（渐变透明后销毁）
    public void DestroyAllEnemies()
    {
        if (enemies == null || enemies.Count <= 0) return;

        // 启动协程统一渐变销毁
        StartCoroutine(DestroyAllEnemiesFade());
    }

    // 协程：所有敌人图片渐变透明 → 销毁
    private IEnumerator DestroyAllEnemiesFade()
    {
        float elapsed = 0;

        // 渐变阶段：所有敌人图片逐渐变透明
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.chaseSpeed = 0;
                    enemy.patrolSpeed = 0;
                    FadeEnemyImage(enemy.GetComponentInChildren<Image>(), t);
                }
            }
            yield return null;
        }

        // 渐变完成 → 销毁所有敌人
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }

        // 清空列表
        enemies.Clear();
    }

    // 单个敌人图片渐变透明度（被协程调用）
    public void FadeEnemyImage(Image image, float t)
    {
        if (image == null) return;

        Color color = image.color;
        color.a = Mathf.Lerp(1f, 0f, t); // 从 1 渐变到 0
        image.color = color;
    }
}