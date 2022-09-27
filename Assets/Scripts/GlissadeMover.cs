using UnityEngine;
using System.Diagnostics;

public class GlissadeMover : MonoBehaviour
{
    public float Theta = 2f; // угол наклона глиссады, градусы
    public float HorizontalVelocity = 69.444f; // начальная скорость, м/с
    public float StartDistance = 10000f; // начальное расстояние до ВПП, м
    public float DistanceFromVPPStart = 300f; // расстояние от начала ВПП
    public float StartAlignmentHeight = 400f; // высота в точке начала выравнивания, м
    public float VerticalVelocity; // вертикальная компонента скорости, м
    public float ChangeTrackHeight = 20f; // высота, при которой изменяется траектория движения, м
    public float VerticalVelocity2 = 5f; // вертикальная компонента скорости, м
    public GameObject DataObject;
    public GameObject ScreenshotObject;

    private float _realDistance; // расстояние от старта до точки касания с ВПП
    private Transform _t;
    private float _tgTheta;
    private Vector3 _point;
    private Data _data;
    private bool _firstTime; // флаг (чтобы запись в файлы происходила только 1 раз)
    private Stopwatch _stopwatch; // таймер

    private void Start()
    {
        _realDistance = StartDistance + DistanceFromVPPStart;
        _data = DataObject.GetComponent<Data>();
        _t = GetComponent<Transform>();
        _tgTheta = Mathf.Tan(Theta * Mathf.PI / 180);
        StartAlignmentHeight = _tgTheta * _realDistance;
        VerticalVelocity = HorizontalVelocity * _tgTheta;
        _t.position = new Vector3(0f, StartAlignmentHeight, 0f);
        _firstTime = true;
        _stopwatch = Stopwatch.StartNew();
    }

    private void Update()
    {
        if (_t.position.y > 10)
        {
            _point = new Vector3(HorizontalVelocity, -VerticalVelocity, 0);
            UnityEngine.Debug.DrawLine(new Vector3(0, StartAlignmentHeight, 0), _t.position, Color.blue);
            _t.position += _point * Time.deltaTime;

            // запись инфы для статистики
            _data.VerticalVelocity = 
                Data.Append(_data.VerticalVelocity, 
                    new Vector3(_stopwatch.ElapsedMilliseconds, VerticalVelocity, 0));
        }
        else if (_t.position.y <= 10 && _t.position.y >= 1)
        {
            // кривая от высоты 10 до высоты 1
            _point = new Vector3(HorizontalVelocity, -VerticalVelocity2, 0);
            _t.position += _point * Time.deltaTime;
            HorizontalVelocity -= 0.1f;
            VerticalVelocity2 -= 0.006f;

            // запись инфы для статистики
            _data.VerticalVelocity =
                Data.Append(_data.VerticalVelocity, 
                    new Vector3(_stopwatch.ElapsedMilliseconds, VerticalVelocity2, 0));
        }
        else
        {
            /*
            if (_t.position.y > 0)
            {
                _point = new Vector3(HorizontalVelocity, -VerticalVelocity2, 0);

                // запись инфы для статистики
                _data.VerticalVelocity =
                    Data.Append(_data.VerticalVelocity, new Vector3(_stopwatch.ElapsedMilliseconds, 0.1f, 0));
            }
            else
            {
                _point = new Vector3(HorizontalVelocity, 0f, 0f);

                // запись инфы для статистики
                _data.VerticalVelocity =
                        Data.Append(_data.VerticalVelocity,
                            new Vector3(_stopwatch.ElapsedMilliseconds, 0f, 0));
            }
            */

            _point = new Vector3(HorizontalVelocity, -VerticalVelocity2, 0);
            // запись инфы для статистики
            _data.VerticalVelocity =
                Data.Append(_data.VerticalVelocity, 
                    new Vector3(_stopwatch.ElapsedMilliseconds,VerticalVelocity2, 0));

            _t.position += _point * Time.deltaTime;
            //VerticalVelocity2 -= 0.1f;
            VerticalVelocity2 -= 0.006f;
            HorizontalVelocity -= 0.05f;
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
            VerticalVelocity2 = 0f;
        }

        // запись инфы для статистики
        _data.Coordinates = Data.Append(_data.Coordinates, _t.position);
        _data.HorizontalVelocity = 
            Data.Append(_data.HorizontalVelocity,
                new Vector3(_stopwatch.ElapsedMilliseconds, HorizontalVelocity, 0));

        // если самолет на земле (координата и вертикальная скорости == 0)
        // И 
        // остановился (горизонтальная скорость = 0)
        // И
        // если запись происходит первый раз
        // то записываем в файлы
        if (_t.position.y == 0f && HorizontalVelocity == 0f && VerticalVelocity2 == 0f && _firstTime)
        {
            Data.WriteDataInFile(_data);
            _firstTime = false; // запись произошла -> обновить флаг
            _stopwatch.Stop(); // остановить счетчик времени
            ScreenshotObject.SetActive(false);
        }
    }

    private float Line1(float x) => -_tgTheta * x + StartAlignmentHeight;
    private float Line2(float x) => -Mathf.Tan(1 * Mathf.PI / 180) * x + StartAlignmentHeight;
    private float Curve(float x) => ChangeTrackHeight * Mathf.Exp(-1 * Mathf.Tan(Theta) * x / ChangeTrackHeight);
}
