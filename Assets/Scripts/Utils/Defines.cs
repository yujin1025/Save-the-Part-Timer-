using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{

    public enum Scene
    {
        Unknown,
        Start,
        Lobby,
        Main,
        Game,
        BreakTime,
        Ending,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag
    }
}
