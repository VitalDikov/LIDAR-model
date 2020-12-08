using UnityEngine;


public class Laser : MonoBehaviour
{
    public GameObject myPrefab; // ссылка на точку, чтобы он знал, что создавать
    // математические парматеры лазера
    public float w = (float)0.02; // горловина пучка
    public float div = (float)(2.5e-3); // дивергенция пучка
    public float minbright = (float)5;

    void Start() { }
    void Update() { }

    public GameObject Light()
    {

        Vector2 fwd = transform.TransformDirection(Vector2.up);
        if (Physics2D.Raycast(transform.position, fwd)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd);
            print($"dist: {hit.distance}_R: {GetGaussRadius(hit.distance)}_Bright: {GetLightBrightness(hit.distance)}");
            // if(GetLightBrightness(hit.distance) < minbright)
            //     return null;
            GameObject Point = Instantiate(myPrefab, hit.point, Quaternion.identity);
            Point.GetComponent<CircleCollider2D>().radius = GetGaussRadius(hit.distance);
            print($"Laser: {hit.point.x}_{hit.point.y}");
            return Point;
        }

        return null;
    }

    public void DestroyLight(GameObject point)
    {
        Destroy(point);
    }

    private float GetLightBrightness(float dist)
    {
        return (float)(0.25 * 8 * Mathf.Exp((float)(-0.216 * dist / 10)) / (Mathf.PI * Mathf.Pow(GetGaussRadius(2 * dist) / 10, 2)));
    }
    private float GetGaussRadius(float dist)
    {
        return Mathf.Sqrt(w * w + (2 * div * dist) * (2 * div * dist));
    }
}
