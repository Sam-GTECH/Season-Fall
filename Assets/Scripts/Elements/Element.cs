using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element
{
    Base _entity;
    public abstract void move();

    public virtual string getElem()
    {
        return "neutral";
    }
}
