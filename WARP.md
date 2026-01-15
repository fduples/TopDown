# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

TopDown is a Unity 2D game project built with Unity 6000.3.2f1, using the Universal Render Pipeline (URP) for 2D rendering and Unity's new Input System for handling player controls.

## Build and Development Commands

### Opening the Project
This is a Unity project that must be opened through the Unity Hub or Unity Editor:
```powershell
# Launch Unity Editor (if Unity is in PATH)
unity -projectPath "C:\Users\fade_\Proyectos\TopDown"
```

### Build
Unity projects are typically built through the Unity Editor:
- **Editor Menu**: File > Build Settings > Build
- **Command Line Build** (Windows):
```powershell
# Standalone Windows build
"C:\Program Files\Unity\Hub\Editor\6000.3.2f1\Editor\Unity.exe" -quit -batchmode -projectPath "C:\Users\fade_\Proyectos\TopDown" -buildWindows64Player ".\Builds\TopDown.exe"
```

### Testing
Unity projects use the Unity Test Framework:
- **Editor Menu**: Window > General > Test Runner
- **Command Line**:
```powershell
# Run EditMode tests
"C:\Program Files\Unity\Hub\Editor\6000.3.2f1\Editor\Unity.exe" -runTests -batchmode -projectPath "C:\Users\fade_\Proyectos\TopDown" -testResults ".\TestResults.xml" -testPlatform EditMode
```

## Architecture

### Rendering Architecture
- **Render Pipeline**: Universal Render Pipeline (URP) 17.3.0 configured for 2D
- **Renderer**: Custom 2D Renderer located at `Assets/Settings/Renderer2D.asset`
- **Global Settings**: `Assets/UniversalRenderPipelineGlobalSettings.asset`
- **Post-Processing**: Volume profile at `Assets/DefaultVolumeProfile.asset`
- **Resolution**: Default 1920x1080, with Linear color space

### Input System Architecture
Uses Unity's new Input System (1.17.0) instead of legacy Input Manager:
- **Input Actions Asset**: `Assets/InputSystem_Actions.inputactions`
- **Action Maps**:
  - **Player**: Core gameplay actions (Move, Look, Attack, Interact, Jump, Sprint, Crouch, Previous, Next)
  - **UI**: UI navigation and interaction actions
- **Control Schemes**: Keyboard&Mouse, Gamepad, Touch, Joystick, XR
- **Key Bindings**:
  - Movement: WASD/Arrow keys, Gamepad left stick
  - Look: Mouse delta, Gamepad right stick
  - Attack: Left Mouse Button, Enter, Gamepad West button
  - Jump: Space, Gamepad South button
  - Sprint: Left Shift, Gamepad left stick press
  - Interact: E (hold), Gamepad North button
  - Crouch: C, Gamepad East button

To generate C# classes from Input Actions:
1. Select `InputSystem_Actions.inputactions` in Unity Editor
2. Enable "Generate C# Class" in the Inspector
3. Click "Apply" to generate the wrapper class

### 2D Tooling and Assets
Key 2D packages installed:
- **2D Animation** (13.0.2): For skeletal animation
- **2D Aseprite** (3.0.1): Aseprite file import
- **2D PSD Importer** (12.0.1): Photoshop file import with layer support
- **2D Sprite Shape** (13.0.0): For creating organic 2D terrains
- **2D Tilemap** (1.0.0): Tilemap system for level design
- **2D Tilemap Extras** (6.0.1): Additional tilemap brushes and tools

### Scene Structure
- **Main Scene**: `Assets/Scenes/SampleScene.unity`
- **Scene Template**: `Assets/Settings/Scenes/URP2DSceneTemplate.unity` (for creating new URP 2D scenes)

### Project Structure
```
Assets/
├── Scenes/              # Unity scenes
├── Settings/            # Project settings and templates
│   ├── Renderer2D.asset        # 2D URP renderer configuration
│   ├── UniversalRP.asset       # URP render pipeline asset
│   └── Scenes/                 # Scene templates
├── InputSystem_Actions.inputactions  # Input action definitions
├── DefaultVolumeProfile.asset  # Post-processing effects
└── UniversalRenderPipelineGlobalSettings.asset

ProjectSettings/         # Unity project configuration
Packages/               # Package dependencies (manifest.json)
```

## Development Workflow

### Creating New Scripts
When creating C# scripts for Unity:
1. Scripts must be placed in `Assets/` or subdirectories
2. Class name must match filename
3. Scripts should inherit from `MonoBehaviour` for scene objects
4. Use Unity's namespace conventions (e.g., `UnityEngine`, `UnityEngine.InputSystem`)

### Working with Input System
To use Input System in scripts:
```csharp
using UnityEngine.InputSystem;

// Reference the auto-generated class
private InputSystem_Actions inputActions;

void Awake()
{
    inputActions = new InputSystem_Actions();
}

void OnEnable()
{
    inputActions.Player.Enable();
}

void OnDisable()
{
    inputActions.Player.Disable();
}
```

### 2D Physics and Collisions
- Use `Rigidbody2D` for physics-based 2D objects
- Use `Collider2D` components (BoxCollider2D, CircleCollider2D, PolygonCollider2D)
- Physics2D settings are in ProjectSettings/Physics2D

### Adding Assets
- Sprites: Import as Texture Type "Sprite (2D and UI)"
- Animations: Use 2D Animation package with Sprite Skin components
- Tilemaps: Create Grid GameObject with Tilemap component
- Aseprite files: Directly import .aseprite files (requires 2D Aseprite package)

## Important Unity Patterns

### Serialization
- Use `[SerializeField]` for private fields that should appear in Inspector
- Use `[HideInInspector]` for public fields that shouldn't appear in Inspector
- Unity serializes fields, not properties

### Lifecycle Methods Order
1. `Awake()` - Initialization before scene loads
2. `OnEnable()` - When object becomes active
3. `Start()` - First frame after object is enabled
4. `FixedUpdate()` - Fixed timestep (physics)
5. `Update()` - Every frame
6. `LateUpdate()` - After all Update calls
7. `OnDisable()` - When object becomes inactive
8. `OnDestroy()` - When object is destroyed

### Performance Considerations
- Avoid `GameObject.Find()` and `GetComponent()` in Update loops
- Cache component references in `Awake()` or `Start()`
- Use object pooling for frequently instantiated objects
- Use `CompareTag()` instead of `gameObject.tag == "TagName"`

## Version Control

### Git Configuration
The project uses Git with Unity-specific `.gitignore`:
- **Tracked**: Assets/, ProjectSettings/, Packages/manifest.json
- **Ignored**: Library/, Temp/, Logs/, UserSettings/, obj/, *.csproj, *.sln (except .slnx)

### Unity Collaboration
- Uses Unity Collab Proxy (2.10.2) for version control integration
- Scene and prefab merging can be handled with Unity's Smart Merge tool

## Platform Configuration

### Current Target Platform
- **Default**: Standalone Windows
- **Resolution**: 1920x1080
- **Application Identifier**: com.DefaultCompany.2D-URP
- **Company**: DefaultCompany
- **Product Name**: TopDown

### IDE Integration
Supported IDEs with packages installed:
- **JetBrains Rider** (3.0.38)
- **Visual Studio** (2.0.25)

To regenerate project files:
- Unity Editor: Edit > Preferences > External Tools > Regenerate project files
