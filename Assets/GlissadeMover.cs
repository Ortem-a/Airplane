using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlissadeMover : MonoBehaviour
{
    public float Theta = 2f; // угол наклона глиссады, градусы
    public float HorizontalVelocity = 69.444f; // начальна€ скорость, м/с
    public float StartDistance = 10000f; // начальное рассто€ние до ¬ѕѕ, м
    public float StartAlignmentHeight = 500f; // высота в точке начала выравнивани€, м
    public float VerticalVelocity = 5f;

    private Transform _t;
    private float _tgTheta;
    private static int _numberOfPoint = 100;
    private Vector3[] _points = new Vector3[_numberOfPoint];

    private void Start()
    {
        _t = GetComponent<Transform>();
        _t.position = new Vector3(StartDistance, StartAlignmentHeight, 0);
        _tgTheta = Mathf.Tan(Theta * Mathf.PI / 180);
        VerticalVelocity = HorizontalVelocity * _tgTheta;

        for (int s = 0; s < _numberOfPoint; s++)
        {
             _points[s] = new Vector3(
                -15 * Mathf.Exp(-1 * Mathf.Tan(Theta) * s / 15),
                - s + 15, 0);
        }
    }

    private void Update()
    {
        if (_t.position.y > 15)
        {
            Vector3 point = new Vector3(-HorizontalVelocity, -VerticalVelocity, 0);
            Debug.DrawLine(new Vector3(StartDistance, StartAlignmentHeight, 0), _t.position, Color.blue);
            _t.position += point * Time.deltaTime * 100;
        }
        else
        {
            if (_t.position.y <= 10 && HorizontalVelocity != 0f) // коснулись земли
            {
                VerticalVelocity = 0f;
                HorizontalVelocity -= 10f * Time.deltaTime;
            }

            for (int i = 0; i < _numberOfPoint-1; i++)
            {
                Debug.DrawLine(_points[i], _points[i+1], Color.red, 10f);
            }

            if (HorizontalVelocity <= 0f)
            {
                HorizontalVelocity = 0f;
            }

            Vector3 point = new Vector3(-HorizontalVelocity, -VerticalVelocity, 0);
            _t.position += point * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int s = 0; s < _numberOfPoint; s++)
        {
            Gizmos.DrawSphere(new Vector3(
               -15 * Mathf.Exp(-1 * Mathf.Tan(Theta) * s / 15),
               - s + 15, 0), 1f);
        }

        Gizmos.DrawLine(new Vector3(StartDistance, StartAlignmentHeight, 0), new Vector3(-3.8887f, 15f, 0));
    }
}
