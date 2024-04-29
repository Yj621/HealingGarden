using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakuMove : MonoBehaviour
{
    public GameObject cube1;    // 거점 1
    public GameObject cube2;    // 거점 2
    public float speed = 1f;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        journeyLength = Vector3.Distance(cube1.transform.position, cube2.transform.position);
        startTime = Time.time;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = Mathf.PingPong(distCovered, journeyLength) / journeyLength;

        transform.position = Vector3.Lerp(cube1.transform.position, cube2.transform.position, fracJourney);
    }
}