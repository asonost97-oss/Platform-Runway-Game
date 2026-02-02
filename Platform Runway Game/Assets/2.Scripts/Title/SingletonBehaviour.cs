using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SingletoneBehaviour<T> : MonoBehaviour where T : SingletoneBehaviour<T>
{
    protected bool m_IsDestroyOnLoad = false;
    protected static T m_Instance;

    public static T Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        Init();
    }

    //가상함수
    //이 클래스를 상속해서 만드는 클래스들이 이 함수를 확장해서 여러가지 다른 처리들까지 추가할 수 있게 해주려는 의도
    protected virtual void Init()
    {
        if (m_Instance == null)
        {
            m_Instance = (T)this;

            if (!m_IsDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
        }
        else
        {
            //null이 아닌데 Init함수를 호출하게 된다면 이미 인스턴스가 있는데 다른 인스턴스를 또만들어 주려는 의도라고 판단해서 그렇게 하려는 이 인스턴스 자체를 삭제
            Destroy(gameObject);
        }

    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        m_Instance = null;
    }
}
