using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment;
    public GameObject backdrop;
    public Transform player;

    [SerializeField] int zPos = 50;
    [SerializeField] int segmentLength = 50;
    [SerializeField] float spawnDistance = 200f;
    [SerializeField] float destroyDistance = 50f;

    private List<GameObject> activeSegments = new List<GameObject>();
    private List<GameObject> activeBackdrops = new List<GameObject>();
    private bool creatingSegment = false;

    void Update()
    {
        // Spawn ahead of player
        if (player.position.z + spawnDistance > zPos && !creatingSegment)
        {
            StartCoroutine(SegmentGen());
        }

        CleanupSegments();
    }

    IEnumerator SegmentGen()
    {
        creatingSegment = true;

        int segmentNum = Random.Range(0, segment.Length);
        GameObject newSegment = Instantiate(
            segment[segmentNum],
            new Vector3(0, 0, zPos),
            Quaternion.identity
        );

        activeSegments.Add(newSegment);

        GameObject newBackdrop = Instantiate(
            backdrop,
            new Vector3(0, 0, zPos),
            Quaternion.identity
        );

        activeBackdrops.Add(newBackdrop);

        zPos += segmentLength;

        yield return new WaitForSeconds(0.1f);
        creatingSegment = false;
    }

    void CleanupSegments()
    {
        for (int i = activeSegments.Count - 1; i >= 0; i--)
        {
            GameObject seg = activeSegments[i];

            if (player.position.z - seg.transform.position.z > destroyDistance)
            {
                Destroy(seg);
                activeSegments.RemoveAt(i);
            }
        }

        for (int j = activeBackdrops.Count - 1; j >= 0; j--)
        {
            GameObject bd = activeBackdrops[j];

            if (player.position.z - bd.transform.position.z > destroyDistance)
            {
                Destroy(bd);
                activeBackdrops.RemoveAt(j);
            }
        }
    }
}


