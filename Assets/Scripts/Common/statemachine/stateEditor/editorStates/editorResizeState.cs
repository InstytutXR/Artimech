/// Artimech
/// 
/// Copyright � <2017-2018> <George A Lancaster>
/// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
/// and associated documentation files (the "Software"), to deal in the Software without restriction, 
/// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
/// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
/// is furnished to do so, subject to the following conditions:
/// The above copyright notice and this permission notice shall be included in all copies 
/// or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
/// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
/// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
/// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
/// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR 
/// OTHER DEALINGS IN THE SOFTWARE.
/// 

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#region XML_DATA

#if ARTIMECH_META_DATA
<!-- Atrimech metadata for positioning and other info using the visual editor.  -->
<!-- The format is XML. -->
<!-- __________________________________________________________________________ -->
<!-- Note: Never make ARTIMECH_META_DATA true since this is just metadata       -->
<!-- Note: for the visual editor to work.                                       -->

<stateMetaData>
  <State>
    <name>nada</name>
    <posX>20</posX>
    <posY>40</posY>
    <sizeX>150</sizeX>
    <sizeY>80</sizeY>
  </State>
</stateMetaData>

#endif

#endregion

/// <summary>
/// This looks for the up click and then a condition is added and
/// the state is returned to 'Display Windows'
/// </summary>
namespace Artimech
{
    public class editorResizeState : editorBaseState
    {
        stateWindowsNode m_WindowsSelectedNode = null;

        bool m_ActionConfirmed = false;
        
        #region Accessors

        /// <summary>  Returns true if the action is confirmed. </summary>
        public bool ActionConfirmed { get { return m_ActionConfirmed; } }

        #endregion

        /// <summary>
        /// State constructor.
        /// </summary>
        /// <param name="gameobject"></param>
        public editorResizeState(GameObject gameobject) : base(gameobject)
        {
            //<ArtiMechConditions>
            m_ConditionalList.Add(new editor_Resize_To_Display("Display Windows"));
        }

        /// <summary>
        /// Updates from the game object.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Fixed Update for physics and such from the game object.
        /// </summary>
        public override void FixedUpdate()
        {

        }

        /// <summary>
        /// For updateing the unity gui.
        /// </summary>
        public override void UpdateEditorGUI()
        {
            base.UpdateEditorGUI();

            Event ev = Event.current;
            stateEditorUtils.MousePos = ev.mousePosition;


            Rect rect = m_WindowsSelectedNode.WinRect;

            Vector2 mousePosTrans = new Vector2();
            mousePosTrans = stateEditorUtils.TranslationMtx.UnTransform(ev.mousePosition);

            rect.width = mousePosTrans.x - m_WindowsSelectedNode.WinRect.x;
            rect.height = mousePosTrans.y - m_WindowsSelectedNode.WinRect.y;

            rect.width = Mathf.Clamp(rect.width, 32, 1024);
            rect.height = Mathf.Clamp(rect.height, 32, 1024);

            m_WindowsSelectedNode.WinRect = rect;

            if (ev.type == EventType.MouseUp)
                m_ActionConfirmed = true;

            stateEditorUtils.Repaint();
        }

        /// <summary>
        /// When the state becomes active Enter() is called once.
        /// </summary>
        public override void Enter()
        {
            m_WindowsSelectedNode = stateEditorUtils.SelectedNode;
            m_ActionConfirmed = false;
            stateEditorUtils.Repaint();
        }

        /// <summary>
        /// When the state becomes inactive Exit() is called once.
        /// </summary>
        public override void Exit()
        {

        }
    }
}
#endif