using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrageable
{
    // Start is called before the first frame update
    Boolean OnDropObject(Item droppedItem);
}
