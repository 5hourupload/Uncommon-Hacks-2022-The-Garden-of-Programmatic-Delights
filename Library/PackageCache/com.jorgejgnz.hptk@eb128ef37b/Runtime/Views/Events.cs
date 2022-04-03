﻿using HPTK.Models.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static HPTK.Views.Handlers.ProxyHandHandler;

namespace HPTK.Views.Events
{
    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [Serializable]
    public class RigidbodyEvent : UnityEvent<Rigidbody> { }

    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }

    [Serializable]
    public class InteractionEvent : UnityEvent<InteractionModel> { }

    [Serializable]
    public class HandStateEvent : UnityEvent<HandViewModel> { }

    [Serializable]
    public class FingerStateEvent : UnityEvent<FingerViewModel> { }
}