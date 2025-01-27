using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // Начальная позиция фона
    private Vector3 startPos;

    // Ширина фона, после которой он будет повторяться
    private float repeatWidth;

    // Метод, вызываемый при старте
    void Start()
    {
        // Сохраняем начальную позицию фона
        startPos = transform.position;

        // Определяем ширину фона, используя размер спрайта
        repeatWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Проверяем, если позиция фона меньше начальной позиции минус половина ширины фона
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Возвращаем фон в начальную позицию для создания эффекта бесконечного прокручивания
            transform.position = startPos;
        }
    }
}
