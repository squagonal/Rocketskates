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
    void gravswap();
    Vector3 gravity{
        get; set;
    }
    bool grounded{
        get; set;
    }
}