using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    // 맵 위치 범위 데이터 저장

    [SerializeField]
    Vector2 MinDr;
    [SerializeField]
    Vector2 MaxDr;

    public Vector2 minDr => MinDr; // property(생성자)
    public Vector2 maxDr => MaxDr;
}
