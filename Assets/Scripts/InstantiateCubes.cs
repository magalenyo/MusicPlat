using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    private GameObject[] sampleCube = new GameObject[512];

    public float maxScale = 1.0f;

    private float defaultSize = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        float angle = 360.0f / 512.0f;
        for (int i = 0; i < 512; ++i)
        {
            GameObject instanceSampleCube = (GameObject) Instantiate(sampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "SampleCube" + i;

            this.transform.eulerAngles = new Vector3(0, -angle * i, 0);
            instanceSampleCube.transform.position = Vector3.forward * 100;  // 100 is distance

            sampleCube[i] = instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; ++i)
        {
            if (sampleCube != null)
            {
                sampleCube[i].transform.localScale = new Vector3(10, AudioPeer.samples[i] * maxScale + defaultSize, 10);
            }
        }
    }
}
