using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Сила прыжка игрока
    [SerializeField] private float jumpForce = 0;

    // Скорость игрока
    [SerializeField] private float speed = 0;

    // Режим применения силы
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    // Эффекты частиц для взрыва и грязи
    [SerializeField] private ParticleSystem fxExplosion;
    [SerializeField] private ParticleSystem fxDirtSplatter;

    // Звуки прыжка и столкновения
    [SerializeField] private AudioClip soundJump;
    [SerializeField] private AudioClip soundCrash;

    // Компоненты Rigidbody, Animator и AudioSource
    private Rigidbody rb;
    private Animator anim;
    private AudioSource audioSource;

    // Флаг, указывающий, находится ли игрок на земле
    private bool isOnGround = false;

    // Флаг, указывающий, окончена ли игра
    public bool isGameOver { get; private set; } = false;

    // Метод, вызываемый при старте
    void Start()
    {
        // Получаем компоненты Rigidbody, Animator и AudioSource
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Метод, вызываемый каждый кадр
    void Update()
    {
        // Если игра окончена, выходим из метода
        if (isGameOver) return;

        // Если нажата клавиша пробела и игрок на земле
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;

            // Применяем силу прыжка
            if (rb)
            {
                rb.AddForce(Vector3.up * jumpForce, forceMode);
            }

            // Запускаем анимацию прыжка
            if (anim)
            {
                anim.SetTrigger("Jump_trig");
            }

            // Останавливаем эффект грязи
            if (fxDirtSplatter)
            {
                fxDirtSplatter.Stop();
            }

            // Проигрываем звук прыжка
            if (audioSource && soundJump)
            {
                audioSource.PlayOneShot(soundJump, 0.75f);
            }
        }
    }

    // Метод, вызываемый при столкновении
    void OnCollisionEnter(Collision collision)
    {
        // Если игра окончена, выходим из метода
        if (isGameOver) return;

        // Если столкновение с землей
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

            // Запускаем эффект грязи
            if (fxDirtSplatter)
            {
                fxDirtSplatter.Play();
            }
        }
        // Если столкновение с препятствием
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Запускаем анимацию смерти
            if (anim)
            {
                anim.SetBool("Death_b", true);
            }

            // Запускаем эффект взрыва
            if (fxExplosion)
            {
                fxExplosion.Play();
            }

            // Останавливаем эффект грязи
            if (fxDirtSplatter)
            {
                fxDirtSplatter.Stop();
            }

            // Проигрываем звук столкновения
            if (audioSource && soundCrash)
            {
                audioSource.PlayOneShot(soundCrash, 1.0f);
            }

            // Применяем силу к препятствию, чтобы оно упало
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            if (collisionRb)
            {
                collisionRb.AddForce(Vector3.right, forceMode);
            }

            // Перезапускаем уровень через 3 секунды
            Invoke("RestartLevel", 3);

            // Устанавливаем флаг окончания игры
            isGameOver = true;
        }
    }

    // Метод для получения текущей скорости игрока
    public float GetSpeed()
    {
        return speed;
    }

    // Метод для увеличения скорости игрока
    public void IncreaseSpeed(float amount)
    {
        // Если игра окончена, выходим из метода
        if (isGameOver) return;

        // Увеличиваем скорость
        speed += amount;
        Debug.Log("New Speed: " + speed);

        // Обновляем фактор скорости анимации
        if (anim)
        {
            anim.SetFloat("RunSpeedAnimFactor_f", speed / 10f);
        }
    }

    // Метод для перезапуска уровня
    private void RestartLevel()
    {
        Debug.Log("Restart!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
