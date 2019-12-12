// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections;

// MonoBehaviour is the base class every script derives from.
public class MonoBehaviour
{
    GameObject go;

    public MonoBehaviour()
    {
    }

    public MonoBehaviour(GameObject _go) {
        go = _go;
    }

    void Start() {}

    void Update() {}
}
