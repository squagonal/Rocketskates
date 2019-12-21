using UnityEngine;
internal interface ISkaterHarness
{
    float jumpPower{
        get; set;
    }
    Transform transform{
        get;
    }
    int rotationValue{
        get; set;
    }
    void skateSpeedUp();
    void skateSpeedDown();
}