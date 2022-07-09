using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class record2 : MonoBehaviour
{

    string filename = "";
    [System.Serializable]
    public class Player
    {
        public int left;
        public int right;
        public int jump;
    }

    [System.Serializable]
    public class PlayerList
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();

    private void Start()
    {
         
       filename = Application.dataPath + "/Level_2_Stats.txt";

        if(Input.GetKeyDown(KeyCode.P))
        {
            WriteTXT();
            trackMovement();
        }

       
        
    }

    private void Update()
    {
        trackMovement();
        WriteTXT();
        string content = "-------------- \n" + "Last game play: " + System.DateTime.Now + "\n";
        File.AppendAllText(filename, content);

      
    }

    public void trackMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) ||Input.GetKeyDown(KeyCode.LeftArrow))
            myPlayerList.player[0].left += 1;
        if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
            myPlayerList.player[0].right += 1;
        if (Input.GetKeyDown(KeyCode.Space) )
            myPlayerList.player[0].jump += 1;
    }

    public void WriteTXT()
    {
        if(myPlayerList.player.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Left, Right, Jump");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for(int i =0; i < myPlayerList.player.Length; i++)
            {
                tw.WriteLine(myPlayerList.player[i].left + "," + myPlayerList.player[i].right + "," +
                    myPlayerList.player[i].jump);
            }
            tw.Close();
        }
    }
}