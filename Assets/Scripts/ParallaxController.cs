using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put this on the active camera
public class ParallaxController : MonoBehaviour
{
    [SerializeField] private float nearBkgrndMoveSpdMult = 0.8f;
    [SerializeField] private float midBkgrndMoveSpdMult = 0.6f;
    [SerializeField] private float farBkgrndMoveSpdMult = 0.4f;
    [SerializeField] private float skyBkgrndMoveSpdMult = 0.2f;
    [SerializeField] private Transform[] nearParallaxBkgrnds;
    [SerializeField] private Transform[] midParallaxBkgrnds;
    [SerializeField] private Transform[] farParallaxBkgrnds;
    [SerializeField] private Transform[] skyParallaxBkgrnds;
    
    private float lastFrameX;

	private void Start()
	{
        lastFrameX = transform.position.x;
    }

	// Update is called once per frame
	void LateUpdate()
    {
        float moveDelta = transform.position.x - lastFrameX;
        for(int i = 0; i < nearParallaxBkgrnds.Length; i++)
        {
            nearParallaxBkgrnds[i].position = new Vector3(nearParallaxBkgrnds[i].position.x + (moveDelta * nearBkgrndMoveSpdMult), nearParallaxBkgrnds[i].position.y, nearParallaxBkgrnds[i].position.z);
            //print("Near Called: " + i);
        }

        for (int i = 0; i < midParallaxBkgrnds.Length; i++)
        {
            midParallaxBkgrnds[i].position = new Vector3(midParallaxBkgrnds[i].position.x + (moveDelta * midBkgrndMoveSpdMult), midParallaxBkgrnds[i].position.y, midParallaxBkgrnds[i].position.z);
            //print("Mid Called: " + i);
        }

        for (int i = 0; i < farParallaxBkgrnds.Length; i++)
        {
            farParallaxBkgrnds[i].position = new Vector3(farParallaxBkgrnds[i].position.x + (moveDelta * farBkgrndMoveSpdMult), farParallaxBkgrnds[i].position.y, farParallaxBkgrnds[i].position.z);
            //print("Far Called: " + i);
        }

        for (int i = 0; i < skyParallaxBkgrnds.Length; i++)
        {
            skyParallaxBkgrnds[i].position = new Vector3(skyParallaxBkgrnds[i].position.x + (moveDelta * skyBkgrndMoveSpdMult), skyParallaxBkgrnds[i].position.y, skyParallaxBkgrnds[i].position.z);
            //print("Sky Called: " + i);
        }

        //print("Delta: " + moveDelta + "     Frame: " + Time.frameCount);
        //print("--------------------------------");
        lastFrameX = transform.position.x;
    }
}
