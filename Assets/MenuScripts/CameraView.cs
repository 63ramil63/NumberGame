using UnityEngine;

public class CameraView : MonoBehaviour
{
    public static void ScaleToFillCamera(GameObject target, Camera cam = null, bool preserveAspect = true)
    {
        if (target == null) return;
        if (cam == null) cam = Camera.main;
        if (cam == null || !cam.orthographic)
        {
            Debug.LogError("Камера не ортографическая или не найдена!");
            return;
        }

        SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null)
        {
            Debug.LogError("На объекте нет SpriteRenderer или спрайта!");
            return;
        }

        // Размеры видимой области камеры в мировых единицах
        float worldHeight = 2f * cam.orthographicSize;
        float worldWidth = worldHeight * cam.aspect;

        // Исходный размер спрайта (без учёта масштаба)
        Vector2 spriteSize = sr.sprite.bounds.size;

        if (preserveAspect)
        {
            // Сохраняем пропорции: вписываем спрайт в экран с учётом отношения сторон
            float scaleFactor = Mathf.Min(worldWidth / spriteSize.x, worldHeight / spriteSize.y);
            target.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
        else
        {
            // Растягиваем ровно под размер экрана
            target.transform.localScale = new Vector3(
                worldWidth / spriteSize.x,
                worldHeight / spriteSize.y,
                1f
            );
        }
    }
}
