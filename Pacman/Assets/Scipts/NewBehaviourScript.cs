using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityScript;
public class NewBehaviourScript : MonoBehaviour {


    public GameObject text;
    private int i = 0;
    private int count = 0;
    StringBuilder sb = new StringBuilder();

    private string y = "100111000101010100100010011010110110000001001111001001110101100101111101011101101111010001110101000011001111111100011111011101011110010101100101111010110101111101010000010011100000000111111111\n\n\n Congratulations!";
    private List<char> temp = new List<char>();
    private string x = "王中秋";
    //private int y = int(x);
    // Use this for initialization
    public string ChineseToBinary(string s)
    {
        byte[] data = Encoding.Unicode.GetBytes(s);
        StringBuilder result = new StringBuilder(data.Length * 8);

        foreach (byte b in data)
        {
            result.Append(Convert.ToString(b,2).PadLeft(8, '0'));
        }
        return result.ToString();
    }

    private string BinaryToChinese(string input)
    {
        StringBuilder sb = new StringBuilder();
        int numOfBytes = input.Length / 8;
        byte[] bytes = new byte[numOfBytes];
        for (int i = 0; i<numOfBytes; ++i)
        {
            bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
        }
        return System.Text.Encoding.Unicode.GetString(bytes);

    }

    private void Start()
    {
        //UnityEditor.EditorUtility.DisplayDialog("标题", "提示内容", "确认", "取消");
       Debug.Log(ChineseToBinary(x));

        //Debug.Log(BinaryToChinese(y));
    }

    private void FixedUpdate()
    {
        if (i < 1)
        {

            i++;

        }
        else {

            if (count < y.Length) {
                i = 0;
                char xxx = y[count];
                //temp.Add(xxx);
                sb.Append(y[count]);


                text.GetComponent<Text>().text =sb.ToString() ;
                //text.GetComponent<GUIText>().cha
                count++;
            }
            i = 0;
            Debug.Log("count:" + count);
        }
    }


}
