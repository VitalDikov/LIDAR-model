using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
    private List<Vector2> WhallList = new List<Vector2>();
    private List<Vector2> PathList = new List<Vector2>();
    private List<KeyValuePair<Vector2, Vector2>> LightList = new List<KeyValuePair<Vector2, Vector2>>();
    public Texture2D Picture;
    private const float PixelSize = (float)(0.1);
    private const int Border = 25;
    private const string Path = @"map.png";



    void Start()
    {
        Picture = new Texture2D(Border, Border);
    }

    public void PushWhall(Vector2 pt)
    {

        pt.x = (int)(pt.x / PixelSize);
        pt.y = (int)(pt.y / PixelSize);

        if (!WhallList.Contains(pt)) {
            WhallList.Add(pt);
        }
    }

    public void PushPath(Vector2 pt)
    {
        pt.x = (int)(pt.x / PixelSize);
        pt.y = (int)(pt.y / PixelSize);

        if (!PathList.Contains(pt)) {
            PathList.Add(pt);
        }
    }

    public void PushLight(Vector2 start, Vector2 finish)
    {

        start.x = (int)(start.x / PixelSize);
        start.y = (int)(start.y / PixelSize);
        finish.x = (int)(start.x / PixelSize);
        finish.y = (int)(start.y / PixelSize);

        var tmp = new KeyValuePair<Vector2, Vector2>(start, finish);
        if (!LightList.Contains(tmp)) {
            LightList.Add(tmp);
        }
    }

    public void SaveMap(string path)
    {
        System.IO.File.WriteAllBytes(path, Picture.EncodeToPNG());
    }
    public void SaveMap()
    {
        System.IO.File.WriteAllBytes(Path, Picture.EncodeToPNG());
    }

    public Texture2D GetMap() => this.Picture;

    public void UpdateMap()
    {
        int XMax = (int)WhallList.Max(item => item.x);
        int XMin = (int)WhallList.Min(item => item.x);
        int YMax = (int)WhallList.Max(item => item.y);
        int YMin = (int)WhallList.Min(item => item.y);

        int height = Mathf.Abs(YMax - YMin) + 2 * Border;
        int width = Mathf.Abs(XMax - XMin) + 2 * Border;

        int deltaX = -XMin;
        int deltaY = -YMin;

        Picture = new Texture2D(width, height);


        foreach (var ptPair in LightList) {

            Vector2 tmp1 = ptPair.Key;
            Vector2 tmp2 = ptPair.Value;




            tmp1.x += deltaX + Border;
            tmp1.y += deltaY + Border;
            tmp2.x += deltaX + Border;
            tmp2.y += deltaY + Border;

            DrawLine(tmp1, tmp2, Color.yellow);
        }


        foreach (Vector2 pt in PathList) {
            Picture.SetPixel((int)pt.x + deltaX + Border, (int)pt.y + deltaY + Border, Color.red);
        }


        foreach (Vector2 pt in WhallList) {
            Picture.SetPixel((int)pt.x + deltaX + Border, (int)pt.y + deltaY + Border, Color.black);
        }
    }

    public void DrawLine(Vector2 p1, Vector2 p2, Color col)
    {
        Vector2 t = p1;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2));
        float ctr = 0;

        while ((int)t.x != (int)p2.x || (int)t.y != (int)p2.y) {
            t = Vector2.Lerp(p1, p2, ctr);
            ctr += frac;
            Picture.SetPixel((int)t.x, (int)t.y, col);
        }
    }
}