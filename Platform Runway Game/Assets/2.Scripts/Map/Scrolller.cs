using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scroller : MonoBehaviour
{
    [SerializeField]
    Transform[] backGround;
    [SerializeField]
    float range = 10.2f;
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    Vector3 moveDr = Vector3.right;

    void Update()
    {
        // 각 배경을 개별적으로 이동
        for (int i = 0; i < backGround.Length; i++)
        {
            if (backGround[i] != null)
            {
                backGround[i].position += moveDr * speed * Time.deltaTime;

                // 범위를 벗어나면 다음 배경 위로 이동
                if (backGround[i].position.x <= -range)
                {
                    // 다음 배경 찾기 (순환)
                    int nextIndex = (i + 1) % backGround.Length;
                    if (backGround[nextIndex] != null)
                    {
                        backGround[i].position = backGround[nextIndex].position + Vector3.right * range;
                    }
                }
            }
        }
    }
}
