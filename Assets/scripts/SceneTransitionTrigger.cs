using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTriggerValue", menuName = "TriggerValue")]
public class SceneTransitionTrigger : ScriptableObject
{
    public int nextSceneOrCurrentScene;
}
