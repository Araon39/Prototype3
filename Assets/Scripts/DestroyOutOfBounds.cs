using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Проверяем, если позиция объекта по оси Y меньше -5
        if (transform.position.y < -5)
        {
            // Уничтожаем объект
            Destroy(gameObject);
        }
    }
}
