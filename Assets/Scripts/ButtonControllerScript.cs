using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerScript : MonoBehaviour {
    GameObject cursor;
    Vector3 pos;
    float positionX;
    float positionY;
    float positionZ;
    GameControllerScript gameControllerScript;

    // Use this for initialization
    void Start () {
        cursor = GameObject.Find("Cursor");
        positionX = cursor.transform.position.x;
        positionY = cursor.transform.position.y;
        positionZ = cursor.transform.position.z;
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void PressButtonLeft()
    {
        positionX = Mathf.Clamp(--positionX, 0, 14);
        pos = new Vector3(positionX, positionY, positionZ);
        cursor.transform.position = pos;
    }

    public void PressButtonRight()
    {
        positionX = Mathf.Clamp(++positionX, 0, 14);
        pos = new Vector3(positionX, positionY, positionZ);
        cursor.transform.position = pos;
    }

    public void PressButtonUp()
    {
        positionZ = Mathf.Clamp(++positionZ, 0, 19);
        pos = new Vector3(positionX, positionY, positionZ);
        cursor.transform.position = pos;
    }

    public void PressButtonDown()
    {
        positionZ = Mathf.Clamp(--positionZ, 0, 19);
        pos = new Vector3(positionX, positionY, positionZ);
        cursor.transform.position = pos;
    }

    public void PressButtonPut()
    {
        if (gameControllerScript.board[(int)positionX, (int)positionZ] == 0)
        {
            gameControllerScript.board[(int)positionX, (int)positionZ] = 1;
        }
        else
        {
            gameControllerScript.board[(int)positionX, (int)positionZ] = 0;
        }
    }

    public void PressButtonPlay()
    {
        GameObject startButton = GameObject.Find("StartButton");
        if (gameControllerScript.isPlaying)
        {
            gameControllerScript.isPlaying = false;
            startButton.GetComponentInChildren<Text>().text = "START";

        }
        else
        {
            gameControllerScript.isPlaying = true;
            startButton.GetComponentInChildren<Text>().text = "STOP";
        }
    }

    public void PressButtonReset()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                gameControllerScript.board[j, (int)positionZ] = 0;
            }
        }
    }

}
