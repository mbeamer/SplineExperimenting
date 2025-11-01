Usage instructions for Spline demo

Files of interest (in `Assets`):
- `Assets/Scripts/Spline.cs` — Catmull–Rom spline component. Exposes `controlPoints` (List<Transform>) and `loop`.
- `Assets/Scripts/SplineRendererGizmo.cs` — draws the spline in the Scene view and optionally uses a `LineRenderer` in the Game view. Exposes `useLineRenderer`, `lineWidth`, and `lineMaterial`.
- `Assets/Scripts/SplineBall.cs` — moves a ball along the spline. Exposes `speed`, `orientToSpline`, and `loop`.
- `Assets/Editor/CreateSplineSetup.cs` — editor menu helper that creates a ready-to-use `SplineRoot` with control points and a ball.

Quick start

1. Open the Unity project (root contains `Assets/`).
2. Let Unity compile scripts. If compilation errors appear, fix them before proceeding.
3. From the Unity Editor menu choose: `GameObject ->3D Object -> Create Spline Setup`.
 - This creates a `GameObject` named `SplineRoot` with a `Spline` and `SplineRendererGizmo` attached, four control point spheres, and a `SplineBall` already wired to the spline.
4. Select each control point in the Scene view and move it to shape the curve.
 - Control points are stored in the `Spline` component's `controlPoints` list. You can add or remove transforms from that list manually.
5. Configure the visual path:
 - To see the spline in the Scene view, keep the `SplineRendererGizmo` component (it draws Gizmos).
 - To see the spline in the Game view, enable `useLineRenderer` on `SplineRendererGizmo`. Set `lineWidth` and assign a `lineMaterial` if desired. A default `Sprites/Default` material is used if none is provided.
6. Configure the ball:
 - Select `SplineBall` to change `speed`, `orientToSpline`, and whether it `loop`s.
 - Ensure the `spline` field on `SplineBall` is assigned (the setup menu assigns it automatically).
7. Toggle looping consistently:
 - If you want the path to wrap, enable `loop` on the `Spline` component and enable `loop` on `SplineBall`.
8. Enter Play mode — the ball will traverse the spline.

Tips and troubleshooting

- If the `Create Spline Setup` menu item does not appear, ensure the `Assets/Editor/CreateSplineSetup.cs` file is inside an `Editor` folder and Unity finished compiling.
- If the `LineRenderer` is not visible in Game view:
 - Make sure `useLineRenderer` is enabled and `lineWidth` is large enough to see.
 - Assign a `lineMaterial` with an appropriate shader (for example `Sprites/Default` or an unlit shader).
 - Check camera near/far clip planes and any layers/culling settings.
- To add more control points at edit time:
 - Create new empty `GameObject`s or primitives, position them, then drag them into the `Spline.controlPoints` list.
- Orientation: `SplineBall` uses a numerical tangent to orient the ball. For sharper accuracy you can add an analytic derivative method to `Spline`.

Extending the demo

- Replace the gizmo-based scene visualization with runtime handles if you need to edit control points in Play mode.
- Add a `LineRenderer` color gradient or texture to visualize speed along the path.
- Add easing curves or a speed profile to `SplineBall` if non-uniform motion is required.

Contact

This README was generated to help you use the provided scripts. If you want, request a specific enhancement (e.g., handles, gradient, speed profile) and it will be added.