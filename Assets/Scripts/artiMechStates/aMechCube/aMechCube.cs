using UnityEngine;
using System.Collections;

namespace artiMech
{
    public class aMechCube : stateMachineBase
    {

        new void Awake()
        {
            base.Awake();
            CreateStates();
        }

        // Use this for initialization
        new void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
        }

        new void FixedUpdate()
        {
            base.FixedUpdate();
        }

        /// <summary>
        /// Autogenerated state are created here...
        /// </summary>
        void CreateStates()
        {

        }
    }
}