using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBoxManager : MonoBehaviour
{


    [SerializeField]
    private Canvas[] createBox;
    [SerializeField]
    private Canvas[] destoryBox;


    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void CreateBox()
    {
        for(int i =0; i <createBox.Length; i++)
        {
            Instantiate(createBox[i]);
        }
        
    }
    public void DestroyBox()
    {
        for(int i =0; i<destoryBox.Length; i++)
        {
            Destroy(destoryBox[i]);
        }
    }
}
