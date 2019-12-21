using UnityEngine;
internal interface ISkaterHarness
{
    float jumpPower{
        get; set;
    }
    Transform transform{
        get; set;
    }
    int rotationValue{
        get; set;
    }
    void skateSpeedUp();
    void skateSpeedDown();
}