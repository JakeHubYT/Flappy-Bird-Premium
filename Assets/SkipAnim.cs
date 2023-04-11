using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipAnim : MonoBehaviour
{
    public Animator highScoreScreenAnim;
   public void Skip()
    {
        highScoreScreenAnim.SetTrigger("Skip");
    }
}
