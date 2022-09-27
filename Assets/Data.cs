using UnityEngine;
using System.IO;

public class Data : MonoBehaviour
{
    public Vector2[] Coordinates; // массив пар координат точек (х, у)
    public Vector2[] HorizontalVelocity; // массив горизонтальных скоростей (время, скорость)
    public Vector2[] VerticalVelocity; // массив вертикальных скоростей (время, скорость)

    // Добавить новое значение в конец списка
    public static Vector2[] Append(Vector2[] vectors, Vector3 vector)
    {
        Vector2[] newVectors = new Vector2[vectors.Length + 1];

        for (int i = 0; i < vectors.Length; i++)
        {
            newVectors[i] = vectors[i];
        }
        newVectors[vectors.Length].x = vector.x;
        newVectors[vectors.Length].y = vector.y;

        return newVectors;
    }

    // записать полученные данные скоростей и координат по файлам
    public static void WriteDataInFile(Data data)
    {
        try
        {
            StreamWriter streamWriter = new StreamWriter("C:/UnityProjects/Airplane/Graphics/coordinates.txt");
            for (int i = 0; i < data.Coordinates.Length; i++)
            {
                streamWriter.WriteLine(data.Coordinates[i].x + ";" + data.Coordinates[i].y);
            }

            streamWriter = new StreamWriter("C:/UnityProjects/Airplane/Graphics/h_velocity.txt");
            for (int i = 0; i < data.HorizontalVelocity.Length; i++)
            {
                streamWriter.WriteLine(data.HorizontalVelocity[i].x + ";" + data.HorizontalVelocity[i].y);
            }

            streamWriter = new StreamWriter("C:/UnityProjects/Airplane/Graphics/v_velocity.txt");
            for (int i = 0; i < data.VerticalVelocity.Length; i++)
            {
                streamWriter.WriteLine(data.VerticalVelocity[i].x + ";" + data.VerticalVelocity[i].y);
            }
            streamWriter.Close();
        }
        finally
        {
            Debug.Log("Запись завершена.");
        }
    }
}
