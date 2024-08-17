using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Delete_Prefab : MonoBehaviour
{
    public GameObject prefab;
    public Toggle Delete_mode;

   public void Delete_prefab()
   {
       if(Delete_mode.isOn)
       {
           Destroy (prefab);///これを使う
       }
   }
}
