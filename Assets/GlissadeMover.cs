using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlissadeMover : MonoBehaviour
{
    public float Theta = 2f; // угол наклона глиссады, градусы
    public float HorizontalVelocity = 69.444f; // начальная скорость, м/с
    public float StartDistance = 10000f; // начальное расстояние до ВПП, м
    public float StartAlignmentHeight = 400f; // высота в точке начала выравнивания, м
    public float VerticalVelocity; // вертикальная компонента скорости, м
    public float ChangeTrackHeight = 20f; // высота, при которой изменяется траектория движения, м
    public float VerticalVelocity2 = 5f; // вертикальная компонента скорости, м

    private Transform _t;
    private float _tgTheta;
    private static int _numberOfPoint = 100;
    private Vector3[] _points = new Vector3[_numberOfPoint];

    private void Start()
    {
        _t = GetComponent<Transform>();
        _t.position = new Vector3(0, StartAlignmentHeight, 0);
        _tgTheta = Mathf.Tan(Theta * Mathf.PI / 180);
        VerticalVelocity = HorizontalVelocity * _tgTheta;
        StartAlignmentHeight = _tgTheta * StartDistance;
        DrawTrack();
    }

    private void Update()
    {
        if (_t.position.y > 10)
        {
            Vector3 point = new Vector3(HorizontalVelocity, -VerticalVelocity, 0);
            Debug.DrawLine(new Vector3(0, StartAlignmentHeight, 0), _t.position, Color.blue);
            _t.position += point * Time.deltaTime * 20;
        }
        else if (_t.position.y <= 10 && _t.position.y >= 1)
        {
            // кривая от высоты 10 до высоты 1
            Vector3 point = new Vector3(HorizontalVelocity, -VerticalVelocity2, 0);
            _t.position += point * Time.deltaTime;
            HorizontalVelocity -= 0.1f;
            VerticalVelocity2 -= 0.01f;
        }
        else
        {
            Vector3 point = new Vector3(HorizontalVelocity, -VerticalVelocity2, 0);
            _t.position += point * Time.deltaTime;
            VerticalVelocity2 -= 0.1f;
            HorizontalVelocity -= 0.1f;
        }

        if (HorizontalVelocity <= 0f)
        {
            HorizontalVelocity = 0f;
        }
        if (VerticalVelocity2 <= 0f)
        {
            VerticalVelocity2 = 0f;
        }
        if (_t.position.y <= 0f)
        {
            _t.position = new Vector3(_t.position.x, 0f, 0f);
        }
    }

    private void DrawTrack()
    {
        Vector3 previousPoint = new Vector3(0, Line1(0), 0);
        Vector3 point;
        for (int x = 1; x < 100_000; x++)
        {
            if (Line1(x) > ChangeTrackHeight)
            {
                point = new Vector3(x, Line1(x), 0);
                Debug.DrawLine(previousPoint, point, Color.green, 100f);
            }
            else
            {
                point = new Vector3(x, Curve(x), 0);
                Debug.DrawLine(previousPoint, point, Color.green, 100f);
            }
            previousPoint = point;
        }
    }

    private float Line1(float x) => -_tgTheta * x + StartAlignmentHeight;
    private float Line2(float x) => -Mathf.Tan(1 * Mathf.PI / 180) * x + StartAlignmentHeight;
    private float Curve(float x) => ChangeTrackHeight * Mathf.Exp(-1 * Mathf.Tan(Theta) * x / ChangeTrackHeight);
}
