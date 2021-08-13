using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public enum MonsterType : int
    {
        ZomBunny = 0,
        ZomBear, Hellephant
    }

    private static SpawnManager m_Instance = null;
    public static SpawnManager Get
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<SpawnManager>();
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("SpawnManager").AddComponent<SpawnManager>();
                    DontDestroyOnLoad(m_Instance.gameObject);
                }
            }

            return m_Instance;
        }
    }

    private readonly Dictionary<MonsterType, int> m_MaxMonsterCount = new Dictionary<MonsterType, int>();
    private readonly Dictionary<MonsterType, int> m_CurrentMonsterCount = new Dictionary<MonsterType, int>();
    private IEnumerator[] m_UpdateSpawnMonsters = null;
    private string m_SpawnTextData = null;

    public string SetTextData { set => m_SpawnTextData = value; }
    //  Test
    //public void Start()
    //{
    //    string data = Resources.Load<TextAsset>("SpawnData/Stage1").text;
    //    SpawnMonster(data);
    //}

    public void StartSpawn() => SpawnMonster();

    private void SpawnMonster()
    {
        if (string.IsNullOrEmpty(m_SpawnTextData))
            throw new System.NullReferenceException("SpawnTextData is null");

        Transform spawnParent = GameObject.Find("SpawnPoint").transform;
        Transform[] spawnPoints = new Transform[spawnParent.childCount];
        for (int i = 0; i < spawnPoints.Length; ++i)
            spawnPoints[i] = spawnParent.GetChild(i);

        m_MaxMonsterCount.Clear();
        using (StringReader reader = new StringReader(m_SpawnTextData))
        {
            string str = reader.ReadLine();
            int length = int.Parse(str);
            m_UpdateSpawnMonsters = new IEnumerator[length];

            int routineCount = 0;
            while ((str = reader.ReadLine()) != null)
            {
                //  몬스터 코드, 마리 수, 딜레이 초 단위
                var line = str.Split(',');

                MonsterType type = (MonsterType)int.Parse(line[0]);
                int count = int.Parse(line[1]);
                float waitTime = float.Parse(line[2]);

                Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
                m_UpdateSpawnMonsters[routineCount++] = UpdateSpawnMonster(type, count, waitTime, spawnPosition);

                m_MaxMonsterCount[type] = count;
            }

            for (int i = 0; i < length; ++i)
                StartCoroutine(m_UpdateSpawnMonsters[i]);
        }
    }

    private IEnumerator UpdateSpawnMonster(MonsterType type, int count, float waitTime, Vector3 position)
    {
        var prefab = Resources.Load($"Prefabs/Enemies/{type}");

        while (count-- != 0)
        {
            Instantiate(prefab, position, Quaternion.identity, null);

            if (m_CurrentMonsterCount.ContainsKey(type)) m_CurrentMonsterCount[type]++;
            else m_CurrentMonsterCount.Add(type, 1);

            yield return new WaitForSeconds(waitTime);
        }

        yield break;
    }

}
