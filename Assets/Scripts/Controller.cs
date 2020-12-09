using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour // контроллер -- управляющий класс пылесоса. Сюда впихнуть все алгоритмы
{
    public RawImage image;
    // Контроллер имеет доступ к лазеру и приемнику 
    public Map RoomMap = new Map();

    private Laser MyLaser;  //доступ к лазеру
    private Transmitter MyTransmiter; // доступ к наследнику

    public float R = (float)(2); // размер пылесоса

    public char OutSideBrightness = 'd'; 


    void Start()
    {
        MyLaser = GetComponentInChildren<Laser>(); // доступ к лазеру и приемнику через иерархию
        MyTransmiter = GetComponentInChildren<Transmitter>();
        image = gameObject.AddComponent<RawImage>();
        if (OutSideBrightness == 'd')
            MyLaser.minbright = 5; // Темная комната, Максимальная вдимость 12,47м
        else if (OutSideBrightness == 'l')
            MyLaser.minbright = 150; // В комнате включены 2 лампочки по 50W, Максимальная видимость 5,23м
        else
            MyLaser.minbright = 12000; // На улице в ясную погоду в полдень, Максимальная видимость < 1м
    }

    // Update is called once per frame
    void Update()
    {
        RoomMap.PushPath(transform.position);
        Vector2 Whall = GetWhall();
        if (Whall != Vector2.zero) {
            RoomMap.PushWhall(Whall);
        }
        RoomMap.UpdateMap();

        image.texture = RoomMap.GetMap();
        (image.texture as Texture2D).Apply();

        // Save
        if (Input.GetKey("space")) {
            RoomMap.SaveMap();
        }
    }



    public Vector2 GetWhall() // смотрит вперед и возвращает координаты препятствия и если его нет нуль-вектор (0; 0)
    {
        GameObject Point = MyLaser.Light();
        if (Point) {
            float angle = (float)MyTransmiter.Find(Point);
            MyLaser.DestroyLight(Point);
            if (angle != -1) {
                float dist = 2 * R * Mathf.Tan(Mathf.Deg2Rad * angle);
                Vector2 Whall = transform.TransformPoint((Vector2.up) * dist + new Vector2(R, 0));
                print($"Whall: {Whall.x}_{Whall.y}");
                return Whall;
            }
        }

        return Vector2.zero;
    }
}
