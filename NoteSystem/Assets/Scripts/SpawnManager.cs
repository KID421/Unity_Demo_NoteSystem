using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("隨機生成物件")]
    public Transform spawnObjectRandom;
    [Header("生成間隔"), Range(0, 10)]
    public float interval = 3f;
    [Header("全部生成點")]
    public GameObject[] points;

    private int indexRandom;

    [Space(50)] // 分隔線：指定生成物件

    [Header("指定生成物件")]
    public Transform spawnObjectAssign;
    [Header("指定生成時間")]
    public float[] assignTimes;
    [Header("指定生成位置")]
    public int[] assignPoints;

    private int indexAssign;

    private void Awake()
    {
        InvokeRepeating("SpawnRandom", 0, interval);

        StartCoroutine(AssignSpawnObject());
    }

    /// <summary>
    /// 隨機生成
    /// </summary>
    private void SpawnRandom()
    {
        while (indexRandom == assignPoints[indexAssign])
        {
            indexRandom = Random.Range(0, points.Length);
            print(indexRandom);
        }
        
        Instantiate(spawnObjectRandom, points[indexRandom].transform.position, points[indexRandom].transform.rotation);
    }

    /// <summary>
    /// 指定生成
    /// </summary>
    private IEnumerator AssignSpawnObject()
    {
        for (int i = 0; i < assignPoints.Length; i++)
        {
            yield return new WaitForSeconds(assignTimes[indexAssign]);
            Transform point = points[assignPoints[indexAssign]].transform;
            Instantiate(spawnObjectAssign, point.position, point.rotation);
            if (i < assignPoints.Length - 1) indexAssign++;
        }
    }
}
