using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI liveText;

    // Update is called once per frame
    void Update()
    {  
        if (PlayerStats.Lives <0)
        {
            return;
        }
        else
        {
            liveText.text = PlayerStats.Lives.ToString() + "Lives";
        }
    }
}
