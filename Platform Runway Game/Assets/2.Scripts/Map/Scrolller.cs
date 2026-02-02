using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scroller : MonoBehaviour
{
    public enum Level
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
    }

    [SerializeField]
    Transform[] backGround;
    [SerializeField]
    float range = 10.2f;
    [SerializeField]
    float baseSpeed = 5f;
    [SerializeField]
    Vector3 moveDr = Vector3.right;
    [SerializeField]
    float levelUpInterval = 10f;

    float speed;
    Level currentLevel = Level.Level1;
    float levelTimer;

    public Vector3 MoveDr => moveDr;
    public float Speed => speed;
    public Level CurrentLevel => currentLevel;

    void Start()
    {
        ApplyLevel(currentLevel);
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        if (levelTimer >= levelUpInterval)
        {
            levelTimer = 0f;
            IncreaseLevel();
        }

        for (int i = 0; i < backGround.Length; i++)
        {
            if (backGround[i] != null)
            {
                backGround[i].position += moveDr * speed * Time.deltaTime;

                if (backGround[i].position.x <= -range)
                {
                    int nextIndex = (i + 1) % backGround.Length;
                    if (backGround[nextIndex] != null)
                    {
                        backGround[i].position = backGround[nextIndex].position + Vector3.right * range;
                    }
                }
            }
        }
    }

    void IncreaseLevel()
    {
        int next = (int)currentLevel + 1;
        int maxLevel = (int)Level.Level10;
        if (next <= maxLevel)
        {
            currentLevel = (Level)next;
            ApplyLevel(currentLevel);
        }
    }

    void ApplyLevel(Level level)
    {
        speed = baseSpeed + (int)level;
    }
}
