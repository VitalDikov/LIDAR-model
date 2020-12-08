using UnityEngine;

public class Transmitter : MonoBehaviour
{
    // Параметры камеры
    public const float FOV = 90; // область видимости, градусы
    public const float Resolution = 920; // разрешение
    public const float DegPerPixel = FOV / Resolution;

    void Start() { }

    void Update() { }

    public float Find(GameObject Point) // возвращает угол до точки, отброшенной лазером
    {

        float start = (float)(transform.rotation.eulerAngles.z);

        for (float angle = start; angle <= start + FOV; angle += DegPerPixel) {

            Vector2 fwd = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd);
            if (hit.collider) {
                if (hit.collider.gameObject.tag == "Point") {
                    return angle - start + DegPerPixel;
                }
            }
        }

        return -1;
    }
}
