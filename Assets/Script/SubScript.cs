using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
// 수정중----------------------------------------------------------------------------------
public class SubScript : MonoBehaviour
{
    // 각 Scene별 배열들
    public Text textBox;
    public List<string> a = new List<string>();
    string[][] SumString = { };
    string[] Subtitle1_1 = {"G1_1",  "G1_2", "G1_3", "G1_4", "G1_5", "empty" };
    string[] Subtitle2_1 = {"H2_1_1", "PC2_1", "H2_1_2", "H2_1_3", "empty" };
    string[] Subtitle2_2 = {"PC2_2", "H2_2_1", "PC2_2", "H2_2_2", "empty" };
    string[] Subtitle2_3 = {"P2_1","PC2_3", "P2_2", "P2_3", "P2_4", "P2_5", "P2_6", "P2_7", "P2_8", "P2_9", "P2_10", "P2_11", "P2_12", "empty" };
    string[] Subtitle2_4 = {"P2_13", "P2_14", "P2_15", "P2_16", "empty" };
    string[] Subtitle2_5 = {"P2_17", "P2_18", "empty" };
    string[] Subtitle3_1 = {"G3_1", "G3_2", "G3_3", "G3_4", "empty" };
    string[] Subtitle4_1 = {"H4_1_1", "H4_2_1", "H4_2_2", "PC4_1", "H4_2_3", "empty" };
    string[] Subtitle4_2 = {"M4_1", "M4_2", "M4_3", "M4_4", "empty" };
    string[] Subtitle4_3 = {"M4_5", "M4_6", "M4_7", "M4_8", "empty" };
    string[] Subtitle4_4 = { "M4_9", "M4_10", "empty" };
    string[] Subtitle5_1 = {"G5_1", "G5_2", "G5_3", "empty" };
    string[] Subtitle6_1 = {"H6_1_1", "H6_2_1", "H6_1_2", "empty" };
    string[] Subtitle6_2 = {"N6_1", "P6_1", "N6_2", "N6_3", "N6_4", "N6_5", "N6_6", "N6_7", "empty" };
    string[] Subtitle6_3 = {"N6_8", "N6_9", "N6_10", "N6_11", "empty" };
    string[] Subtitle6_4 = { "N6_12", "N6_13", "empty" };
    string[] Subtitle7_1 = {"G7_1", "G7_2", "G7_3", "empty" };
    string[] Subtitle8_1 = {"H8_1_1", "PC8_1", "H8_1_2", "empty" };
    string[] Subtitle8_2 = {"H8_2_1", "H8_2_2", "PC8_2", "H8_2_3", "empty" };
    string[] Subtitle8_3 = { "PC8_3", "PC8_4", "S8_1", "S8_2", "S8_3", "S8_4", "S8_5", "S8_6", "S8_7", "S8_8", "empty" };
    string[] Subtitle8_4 = { "S8_9", "S8_10", "S8_11", "S8_12", "empty" };
    string[] Subtitle8_5 = { "S8_13", "S8_14", "empty" };
    string[] Subtitle9_1 = { "G9_1", "G9_2", "G9_3", "G9_4", "empty" };
    string[] D1_1 = { "G1_1", "G1_2", "G1_3", "G1_4", "G1_5", "empty" };
    string[] D2_1 = { "H2_1_1", "PC2_1", "H2_1_2", "H2_1_3", "empty" };
    string[] D2_2 = { "PC2_2", "H2_2_1", "PC2_2", "H2_2_2", "empty" };
    string[] D2_3 = { "P2_1", "PC2_3", "P2_2", "P2_3", "P2_4", "P2_5", "P2_6", "P2_7", "P2_8", "P2_9", "P2_10", "P2_11", "P2_12", "empty" };
    string[] D2_4 = { "P2_13", "P2_14", "P2_15", "P2_16", "empty" };
    string[] D2_5 = { "P2_17", "P2_18", "empty" };
    string[] D3_1 = { "G3_1", "G3_2", "G3_3", "G3_4", "empty" };
    string[] D4_1 = { "H4_1_1", "H4_2_1", "H4_2_2", "PC4_1", "H4_2_3", "empty" };
    string[] D4_2 = { "M4_1", "M4_2", "M4_3", "M4_4", "empty" };
    string[] D4_3 = { "M4_5", "M4_6", "M4_7", "M4_8", "empty" };
    string[] D4_4 = { "M4_9", "M4_10", "empty" };
    string[] D5_1 = { "G5_1", "G5_2", "G5_3", "empty" };
    string[] D6_1 = { "H6_1_1", "H6_2_1", "H6_1_2", "empty" };
    string[] D6_2 = { "N6_1", "P6_1", "N6_2", "N6_3", "N6_4", "N6_5", "N6_6", "N6_7", "empty" };
    string[] D6_3 = { "N6_8", "N6_9", "N6_10", "N6_11", "empty" };
    string[] D6_4 = { "N6_12", "N6_13", "empty" };
    string[] D7_1 = { "G7_1", "G7_2", "G7_3", "empty" };
    string[] D8_1 = { "H8_1_1", "PC8_1", "H8_1_2", "empty" };
    string[] D8_2 = { "H8_2_1", "H8_2_2", "PC8_2", "H8_2_3", "empty" };
    string[] D8_3 = { "PC8_3", "PC8_4", "S8_1", "S8_2", "S8_3", "S8_4", "S8_5", "S8_6", "S8_7", "S8_8", "empty" };
    string[] D8_4 = { "S8_9", "S8_10", "S8_11", "S8_12", "empty" };
    string[] D8_5 = { "S8_13", "S8_14", "empty" };
    string[] D9_1 = { "G9_1", "G9_2", "G9_3", "G9_4", "empty" };

    public void S1_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle1_1);
    }
    public void S2_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle2_1);
    }
    public void S2_2Start()
    {
        a.Clear();
        a.AddRange(Subtitle2_2);
    }
    public void S2_3Start()
    {
        a.Clear();
        a.AddRange(Subtitle2_3);
    }
    public void S2_4Start()
    {
        a.Clear();
        a.AddRange(Subtitle2_4);
    }
    public void S3_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle3_1);
    }
    public void S4_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle4_1);
    }
    public void S4_2Start()
    {
        a.Clear();
        a.AddRange(Subtitle4_2);
    }
    public void S4_3Start()
    {
        a.Clear();
        a.AddRange(Subtitle4_3);
    }
    public void S5_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle5_1);
    }
    public void S6_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle6_1);
    }
    public void S6_2Start()
    {
        a.Clear();
        a.AddRange(Subtitle6_2);
    }
    public void S6_3Start()
    {
        a.Clear();
        a.AddRange(Subtitle6_3);
    }
    public void S7_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle7_1);
    }
    public void S8_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle8_1);
    }
    public void S8_2Start()
    {
        a.Clear();
        a.AddRange(Subtitle8_2);
    }
    public void S8_3Start()
    {
        a.Clear();
        a.AddRange(Subtitle8_3);
    }
    public void S8_4Start()
    {
        a.Clear();
        a.AddRange(Subtitle8_4);
    }
    public void S9_1Start()
    {
        a.Clear();
        a.AddRange(Subtitle9_1);
    }
    public void DS1_1Start()
    {
        a.Clear();
        a.AddRange(D1_1);
    }
    public void DS2_1Start()
    {
        a.Clear();
        a.AddRange(D2_1);
    }
    public void DS2_2Start()
    {
        a.Clear();
        a.AddRange(D2_2);
    }
    public void DS2_3Start()
    {
        a.Clear();
        a.AddRange(D2_3);
    }
    public void DS2_4Start()
    {
        a.Clear();
        a.AddRange(D2_4);
    }
    public void DS3_1Start()
    {
        a.Clear();
        a.AddRange(D3_1);
    }
    public void DS4_1Start()
    {
        a.Clear();
        a.AddRange(D4_1);
    }
    public void DS4_2Start()
    {
        a.Clear();
        a.AddRange(D4_2);
    }
    public void DS4_3Start()
    {
        a.Clear();
        a.AddRange(D4_3);
    }
    public void DS5_1Start()
    {
        a.Clear();
        a.AddRange(D5_1);
    }
    public void DS6_1Start()
    {
        a.Clear();
        a.AddRange(D6_1);
    }
    public void DS6_2Start()
    {
        a.Clear();
        a.AddRange(D6_2);
    }
    public void DS6_3Start()
    {
        a.Clear();
        a.AddRange(D6_3);
    }
    public void DS7_1Start()
    {
        a.Clear();
        a.AddRange(D7_1);
    }
    public void DS8_1Start()
    {
        a.Clear();
        a.AddRange(D8_1);
    }
    public void DS8_2Start()
    {
        a.Clear();
        a.AddRange(D8_2);
    }
    public void DS8_3Start()
    {
        a.Clear();
        a.AddRange(D8_3);
    }
    public void DS8_4Start()
    {
        a.Clear();
        a.AddRange(D8_4);
    }
    public void DS9_1Start()
    {
        a.Clear();
        a.AddRange(D9_1);
    }
}

