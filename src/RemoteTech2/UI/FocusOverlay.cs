using System;
using System.Linq;
using UnityEngine;

namespace RemoteTech
{
    public class FocusOverlay : IFragment, IDisposable
    {
        private readonly GUIStyle mManeuverNodeButton;
        private static class Texture
        {
            public static readonly Texture2D Satellite;

            static Texture()
            {
                RTUtil.LoadImage(out Satellite, "texSatellite.png");
            }
        }

        private static class Style
        {
            public static readonly GUIStyle Button;

            static Style()
            {
                Button = GUITextureButtonFactory.CreateFromFilename("texKnowledgeNormal.png", "texKnowledgeHover.png", "texKnowledgeActive.png", "texKnowledgeHover.png");
            }
        }

        private FocusFragment mFocus = new FocusFragment();
        private bool mEnabled;

        private Rect PositionButton
        {
            get
            {
                if (!KnowledgeBase.Instance) return new Rect(0, 0, 0, 0);
                var position = KnowledgeBase.Instance.KnowledgeContainer.transform.position;
                var position2 = UIManager.instance.rayCamera.WorldToScreenPoint(position);
                var rect = new Rect(position2.x + 154,
                                250 + 2 * 31,
                                Texture.Satellite.width,
                                Texture.Satellite.height);
                return rect;
            }
        }

        private Rect PositionFrame
        {
            get
            {
                var rect = new Rect(0, 0, 250, 500);
                rect.y = PositionButton.y + PositionButton.height / 2 - rect.height / 2;
                rect.x = PositionButton.x - 5 - rect.width;
                return rect;
            }
        }

        public FocusOverlay()
        {
            mManeuverNodeButton = GUITextureButtonFactory.CreateFromFilename("maneuverAddBtn.png", "maneuverAddBtn.png", "maneuverAddBtn.png", "maneuverAddBtn.png");
            mManeuverNodeButton.fixedHeight = mManeuverNodeButton.fixedWidth = 0;
            mManeuverNodeButton.stretchHeight = mManeuverNodeButton.stretchWidth = true;

            MapView.OnEnterMapView += OnEnterMapView;
            MapView.OnExitMapView += OnExitMapView;
        }

        public void Dispose()
        {
            MapView.OnEnterMapView -= OnEnterMapView;
            MapView.OnExitMapView -= OnExitMapView;
        }

        public void OnEnterMapView()
        {
            RTCore.Instance.OnGuiUpdate += Draw;
        }

        public void OnExitMapView()
        {
            RTCore.Instance.OnGuiUpdate -= Draw;
        }

        public void Draw()
        {
            MapView Map = MapView.fetch;
            if (Map != null && FlightGlobals.ActiveVessel != null)
            {
                PatchedConicSolver pCS = FlightGlobals.ActiveVessel.patchedConicSolver;
                if (pCS.maneuverNodes.Count > 0)
                {
                    for (var i = 0; i < pCS.maneuverNodes.Count; i++)
                    {
                        ManeuverNode node = pCS.maneuverNodes[i];

                        if (node.attachedGizmo == null) continue;

                        ManeuverGizmo gizmo = node.attachedGizmo;
                        ScreenSafeUIButton gizmoDeleteBtn = gizmo.deleteBtn;

                        // We are on the right gizmo but no buttons are visible so skip the rest
                        if (!gizmoDeleteBtn.renderer.isVisible)
                            continue;

                        var satellite = RTCore.Instance.Satellites[FlightGlobals.ActiveVessel];
                        if (satellite == null || satellite.SignalProcessor.FlightComputer == null) return;
                        var flightComputer = satellite.SignalProcessor.FlightComputer;

                        var cmd = ManeuverCommand.WithNode(node, flightComputer);

                        Vector3 screenCoord = gizmo.camera.WorldToScreenPoint(gizmo.transform.position);
                        Vector3 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
                        double dist = Math.Sqrt(Math.Pow(screenCenter.x - screenCoord.x, 2.0) + Math.Pow(screenCenter.y - screenCoord.y, 2.0));

                        double btnDim = 18.0f + (8.0f * ((1.2f / screenCenter.magnitude) * Math.Abs(dist)));

                        //btnDim = btnDim * lossyScale;
                        Rect screenPos = new Rect(screenCoord.x - (float)btnDim - 7.0f,
                                                  Screen.height - screenCoord.y - (float)btnDim - 3.0f,
                                                  (float)btnDim,
                                                  (float)btnDim);

                        if (GUI.Button(screenPos, "", mManeuverNodeButton))
                        {
                            flightComputer.Enqueue(cmd, false, false, true);
                        }
                    }
                }
            }

            GUI.depth = 0;
            GUI.skin = HighLogic.Skin;

            GUILayout.BeginArea(PositionButton);
            {
                mEnabled = GUILayout.Toggle(mEnabled, Texture.Satellite, Style.Button);
            }
            GUILayout.EndArea();

            if (mEnabled)
            {
                GUILayout.BeginArea(PositionFrame);
                {
                    mFocus.Draw();
                }
                GUILayout.EndArea();
            }
        }
    }
}