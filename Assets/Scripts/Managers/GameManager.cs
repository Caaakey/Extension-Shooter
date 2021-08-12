using UnityEngine;

//  Singleton
//  이 객체는 반드시 단 하나인 디자인 패턴
public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance = null;
    public static GameManager Get
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("GameManager").AddComponent<GameManager>();
                    DontDestroyOnLoad(m_Instance.gameObject);
                }
            }
            return m_Instance;
        }
    }

    public int FloorLayerMask { get; private set; } = 0;
    public int EnemyLayerMask { get; private set; } = 0;
    public int NPCLayerMask { get; private set; } = 0;

    private void Awake()
    {
        //  Bitmask ??
        //  1 byte = 8 bit
        //  bit ? 0, 1
        //  0010101010101111001010110110010101000110101000101010 <- 매트릭스에서 보던 데이터들. 요게 비트
        //  2진수
        //  int -> 4byte -> 32bit
        //  0000 0000 0000 0000 0000 0000 0000 0000 -> int : 0
        //  0000 0000 0000 0000 0000 0000 0000 0001 -> int : 1
        //  0000 0000 0000 0000 0000 0000 0000 0010 -> int : 2
        //  0000 0000 0000 0000 0000 0000 0000 0100 -> int : 4
        //  시프트 연산자 (비트 단위 연산자)
        //  1 << 0 = 0;
        //  1 << 1 = 2;
        //  1 << 2 = 4;
        //  1 << 3 = 8 등등등 ..
        //  LayerMask ?
        //  유니티에서 tag 와 layer 가 있는데, layer 의 이름을 찾아서 값을 가지고 온다
        FloorLayerMask = 1 << LayerMask.NameToLayer("Floor");
        EnemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        NPCLayerMask = 1 << LayerMask.NameToLayer("NPC");
    }

}
