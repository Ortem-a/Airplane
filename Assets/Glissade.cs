using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glissade : MonoBehaviour
{
    public float Theta = 2f; // ���� ������� ��������, �������
    public float StartVelocity = 69.444f; // ��������� ��������, �/�
    public float StartDistance = 10000f; // ��������� ���������� �� ���, �
    public float StartAlignmentHeight = 500f; // ������ � ����� ������ ������������, �
    public float s0 = 0; // �������� s � ����� ������ ������������
    public float HeightKas; // ���������� �� ������ ������� �������� �� ��� � ������ ������� ��������� �������� ���
    public float DVhod; // ���������� ��������� �� ������ ��� (������� ����� � ��������)
    public float EndAlignmentHeight; // ������ � ����� ������������, ��

    public float s1;
    [Range(50f, 60f)]
    public float sGl; // 50-60 �
    public float sZad; // �������� ���������� �� ����� ������� �� ���
    // ����� ��������� �������� �������� ����� ����� ����������� ���
    public float sKas; // s_���, ����������, ��� ��������� ������������

    public float a0;
    public float a1;
    public float a2;
    public float a3;

    public float VerticalVelocity;

    private void Start()
    {
        transform.position = new Vector3(StartDistance, StartAlignmentHeight, 0);
    }

    public void SetVerticalVelocity()
    {
        VerticalVelocity = StartVelocity * Mathf.Tan(Theta);
    }

    public void SetS1() 
    {
        s1 = StartAlignmentHeight / Mathf.Tan(Theta);
    }

    public void SetSKas()
    {
        sKas = s1 - sGl + sZad;
    }

    public void SetCoefficientsA()
    {
        float tgTheta = Mathf.Tan(Theta);
        float a11 = sKas * sKas;
        float a12 = a11 * sKas;
        float a21 = 2 * sKas;
        float a22 = 3 * a11;
        float b1 = tgTheta * sKas - StartAlignmentHeight + HeightKas;
        float b2 = tgTheta;
        float det = a11 * a22 - a12 * a21;
        float det1 = b1 * a22 - b2 * a12;
        float det2 = b2 * a11 - b1 * a21;

        a0 = StartAlignmentHeight;
        a1 = -1 * tgTheta;
        a2 = det1 / det;
        a3 = det2 / det;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(StartDistance, StartAlignmentHeight, 0), Vector3.zero);

        Gizmos.color = Color.green;
        for (int s = 0; s < 1000; s++)
        {
            Vector3 center = new Vector3(
                -15*Mathf.Exp(-1*Mathf.Tan(Theta)*s/15),
                1000-s, 0);
            Gizmos.DrawSphere(center + new Vector3(0,15,0), 10);
        }
    }
}
