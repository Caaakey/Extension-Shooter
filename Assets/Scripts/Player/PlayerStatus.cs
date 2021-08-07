using UnityEngine;
using YourName.SurvivalShooter;
using YourName.SurvivalShooter.Characters;

public class PlayerStatus : MonoBehaviour
{
    private static PlayerStatus m_Instance = null;
    public static PlayerStatus Get
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<PlayerStatus>();

            return m_Instance;
        }
    }

    public float MaxHP;
    public int Money;
    public int Level;
    public int Experience;
    public bool IsDeath = false;
    [SerializeField] private AudioClip m_DeathClip;
    private AudioSource m_AudioSource;
    private Transform m_PlayerTransform;
    private float m_CurrentHP = 0;

    public float CurrentHP
    {
        get => m_CurrentHP;
        set
        {
            m_CurrentHP = value;
            if (m_CurrentHP >= MaxHP) m_CurrentHP = MaxHP;
        }
    }

    public PlayerMovement Movement { get; private set; } = null;
    public PlayerShooting Shooting { get; private set; } = null;
    public Transform PlayerTransform { get => m_PlayerTransform; }

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        Movement = FindObjectOfType<PlayerMovement>();
        Shooting = FindObjectOfType<PlayerShooting>();
        m_PlayerTransform = Movement.transform;

        CurrentHP = MaxHP;

        DontDestroyOnLoad(this);
    }

    public void Hit(float damage)
    {
        if (IsDeath) return;

        CurrentHP -= damage;
        if (CurrentHP <= 0) Death();
        else m_AudioSource.Play();
    }

    public void Death()
    {
        IsDeath = true;
        Movement.Death();

        Movement.enabled = false;
        Shooting.enabled = false;

        m_AudioSource.clip = m_DeathClip;
        m_AudioSource.Play();

        FindObjectOfType<CameraFollow>().enabled = false;
    }

}
