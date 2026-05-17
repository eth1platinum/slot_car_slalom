using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    [Header("Segments")]
    public GameObject startSegment;
    public GameObject[] segment;
    public GameObject backdrop;
    public Transform player;

    [Header("LED Strip")]
    public GameObject ledPrefab;
    public float ledSpacing = 0.5f;
    public float ledOffsetX = 10f;   // slightly inset from 10-wide edge
    public float ledHeight = 0.6f;   // avoids z-fighting

    public Color color1 = Color.magenta;
    public Color color2 = Color.yellow;
    public float emissionIntensity = 5f;

    [Header("Generation")]
    [SerializeField] int zPos = 0;
    [SerializeField] int segmentLength = 50;
    [SerializeField] float spawnDistance = 200f;
    [SerializeField] float destroyDistance = 50f;

    private List<GameObject> activeSegments = new List<GameObject>();
    private List<GameObject> activeBackdrops = new List<GameObject>();
    private bool creatingSegment = false;

    void Start()
    {
        SpawnInitialSegment();
    }

    void Update()
    {
        // Spawn ahead of player
        if (player.position.z + spawnDistance > zPos && !creatingSegment)
        {
            StartCoroutine(SegmentGen());
        }

        CleanupSegments();
    }

    void SpawnInitialSegment()
    {
        GameObject newStartSegment = Instantiate(
            startSegment,
            new Vector3(0, 0, zPos),
            Quaternion.identity
        );

        activeSegments.Add(newStartSegment);

        GenerateLEDStrip(newStartSegment);

        GameObject newBackdrop = Instantiate(
            backdrop,
            new Vector3(0, 0, zPos),
            Quaternion.identity
        );

        activeBackdrops.Add(newBackdrop);

        zPos += segmentLength;
    }

    // todo this and spawninitialsegment share code, refactor?
    IEnumerator SegmentGen()
    {
        creatingSegment = true;

        int segmentNum = Random.Range(0, segment.Length);

        // Spawn segment
        GameObject newSegment = Instantiate(
            segment[segmentNum],
            new Vector3(0, 0, zPos),
            Quaternion.identity
        );

        activeSegments.Add(newSegment);

        // Generate LEDs for this segment
        GenerateLEDStrip(newSegment);

        // Spawn backdrop
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

    void GenerateLEDStrip(GameObject parentSegment)
    {
        int ledCount = Mathf.FloorToInt(segmentLength / ledSpacing);

        // Keeps colour pattern continuous between segments
        int startIndex = Mathf.FloorToInt(parentSegment.transform.position.z / ledSpacing);

        for (int i = 0; i <= ledCount; i++)
        {
            float z = i * ledSpacing;

            Color ledColor =
                ((i + startIndex) % 2 == 0)
                ? color1
                : color2;

            // Left strip
            SpawnLED(
                new Vector3(-ledOffsetX, ledHeight, z),
                ledColor,
                parentSegment.transform
            );

            // Right strip
            SpawnLED(
                new Vector3(ledOffsetX, ledHeight, z),
                ledColor,
                parentSegment.transform
            );
        }
    }

    void SpawnLED(Vector3 localPos, Color color, Transform parent)
    {
        // Spawn as child using prefab rotation
        GameObject led = Instantiate(
            ledPrefab,
            parent
        );

        // Local positioning relative to segment
        led.transform.localPosition = localPos;
        led.transform.localRotation = ledPrefab.transform.localRotation;

        // Material setup
        Renderer rend = led.GetComponent<Renderer>();

        if (rend != null)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            rend.GetPropertyBlock(block);

            // Standard shader uses _Color (albedo)
            block.SetColor("_Color", color);

            // Emission (Standard shader)
            block.SetColor("_EmissionColor", color * emissionIntensity);

            rend.SetPropertyBlock(block);
        }
    }

    void CleanupSegments()
    {
        // Cleanup segments (LEDs go with them automatically)
        for (int i = activeSegments.Count - 1; i >= 0; i--)
        {
            GameObject seg = activeSegments[i];

            if (player.position.z - seg.transform.position.z > destroyDistance)
            {
                Destroy(seg);
                activeSegments.RemoveAt(i);
            }
        }

        // Cleanup backdrops
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