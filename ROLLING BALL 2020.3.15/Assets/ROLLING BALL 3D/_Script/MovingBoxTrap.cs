using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoxTrap : MonoBehaviour {
    public float posPointA = 1;
    public float posPointB = 4;

    public float randomAxisMin = 0;
    public float randomAxisMax = 0;

    public bool move2A = false;
    public float movingSpeed = 3;

    public enum AXIS { X,Y,Z}
    public AXIS axis = AXIS.Z;
    public Space space = Space.Self;
    Vector3 oriPos;
    // Use this for initialization
    void Start () {
        if(posPointA > posPointB)
        {
            Debug.LogError("posPointA must larger than posPointB");
            enabled = false;
            return;
        }

        if (space == Space.World)
        {
            oriPos = transform.position;
            switch (axis)
            {
                case AXIS.X:
                    oriPos.x = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                case AXIS.Y:
                    oriPos.y = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                case AXIS.Z:
                    oriPos.z = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                default:break;
            }
            
            transform.position = oriPos;
        }
        else
        {
            oriPos = transform.localPosition;
            switch (axis)
            {
                case AXIS.X:
                    oriPos.x = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                case AXIS.Y:
                    oriPos.y = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                case AXIS.Z:
                    oriPos.z = Random.Range(randomAxisMin, randomAxisMax);
                    break;
                default: break;
            }
            transform.localPosition = oriPos;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (move2A)
        {
            switch (axis)
            {
                case AXIS.X:
                    oriPos.x -= Time.deltaTime * movingSpeed;
                    if (oriPos.x < posPointA)
                        move2A = !move2A;
                    break;
                case AXIS.Y:
                    oriPos.y -= Time.deltaTime * movingSpeed;
                    if (oriPos.y < posPointA)
                        move2A = !move2A;
                    break;
                case AXIS.Z:
                    oriPos.z -= Time.deltaTime * movingSpeed;
                    if (oriPos.z < posPointA)
                        move2A = !move2A;
                    break;
                default: break;
            }
           
            
        }
        else
        {
            switch (axis)
            {
                case AXIS.X:
                    oriPos.x += Time.deltaTime * movingSpeed;
                    if (oriPos.x > posPointB)
                        move2A = !move2A;
                    break;
                case AXIS.Y:
                    oriPos.y += Time.deltaTime * movingSpeed;
                    if (oriPos.y > posPointB)
                        move2A = !move2A;
                    break;
                case AXIS.Z:
                    oriPos.z += Time.deltaTime * movingSpeed;
                    if (oriPos.z > posPointB)
                        move2A = !move2A;
                    break;
                default: break;
            }
            
        }

        if (space == Space.World)
        {
            transform.position = oriPos;
        }
        else
        {
            transform.localPosition = oriPos;
        }
    }
}
