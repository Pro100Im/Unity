using UnityEngine;

public class PlayerInput
{
    public bool ToJamp(bool jump)
    {
        if(Input.GetMouseButtonDown(0))
        {
            jump = true;   
        }

        return jump;
    }
}
