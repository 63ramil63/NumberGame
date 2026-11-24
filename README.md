# 🔢 NumberGame — Procedural Logic Puzzle

A mobile logic puzzle game built with **Unity 6** and **C#**, inspired by the hacking minigame from GTA V. The project demonstrates procedural puzzle generation, custom UI mesh rendering, and HLSL shader programming without relying on third-party UI assets.

<!-- TODO: Replace with a 10-15 second gameplay GIF -->
> 🎥 *[Gameplay preview GIF coming soon]*

---

## 🎯 Core Mechanics & Math Logic

The player is presented with two columns of numbers (N = 3, 4, or 5). The objective is to pair every number on the left with a unique number on the right so that the sum of their products equals the target value:

```text
 3 ─┐        ┌─ 5
 2 ─┼────────┼─ 4        (3 × 4) + (2 × 5) + (7 × 1) = 29
 7 ─┘        └─ 1
```

* **Guaranteed Solvability:** Puzzles are generated backward from a valid solution state before shuffling.
* **Anti-Triviality Check:** The generator validates shuffled arrays to ensure the initial layout isn't accidentally pre-solved or overly trivial.

---

## 🛠 Engineering Highlights (Under the Hood)

Instead of using standard Unity UI workarounds (like stretched `Image` prefabs for lines), this project implements custom rendering and clean OOP patterns:

| System / Script | Engineering Implementation & Problem Solved |
| :--- | :--- |
| **`UIPolyLine` & `UIConnection`** | Inherits from `MaskableGraphic` to procedurally generate a **2D mesh** with clean miter/bevel joints. Dynamically calculates routing lanes so overlapping connection lines remain visually distinct. |
| **Custom HLSL Border Shader** | A custom UI shader that animates a smooth color wave along button edges. Uses **normalized perimeter distance** rather than polar angles (`atan2`), ensuring uniform animation speed across rectangular buttons of any aspect ratio. |
| **`Randomizer`** | Implements the **Fisher-Yates shuffle** algorithm to randomize generated pairs in $O(n)$ time while enforcing non-solved initial states. |
| **`GameButton` (OOP Hierarchy)** | Abstract base class encapsulating state management, selection highlighting, and pair-linking events. `FirstRowButton` and `SecondRowButton` inherit from it to handle column-specific constraints cleanly. |
| **Lifecycle & State Management** | Lightweight, scene-scoped managers (`GameCanvas`, `DataHolder`, `GameTimer`, `SoundManager`) with deterministic cleanup in `OnDestroy` to prevent memory leaks across scene reloads. |

---
The project uses no third-party plugins or asset packs. All gameplay code and UI components are written from scratch with Unity's built-in tools.

## 🎮 Features

* **3 Difficulty Tiers:** Dynamically scales grid generation for 3, 4, or 5 pairs.
* **Intuitive Re-pairing UX:** Tap any connected node to unlink, or tap an occupied node in the right column to seamlessly reassign pairs.
* **Adaptive Portrait Layout:** Configured with `CanvasScaler` to support varying mobile aspect ratios.
* **Native Mobile Controls:** Handles Android hardware Back navigation and round timers.

---

## 🚀 Getting Started

### 📱 Quick Play (Android)
Want to test the game right away without setting up Unity? 
1. Download the latest `.apk` file from the **[Releases page](https://github.com/63ramil63/NumberGame/releases/latest)**.
2. Install it on your Android device *(you may need to allow installation from unknown sources in your device settings)*.

### 🛠 Running from Source (Unity)
#### Prerequisites
* **Unity 6 (6000.3 LTS)** or newer
* **Android Build Support** module (optional, for building on mobile)

#### Local Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/63ramil63/NumberGame
   ```
2. Open the folder in **Unity Hub**.
3. Open the `Menu` scene located in `Assets/Scenes/` and press **Play**.

## Credits
* Game design and code: 63ramil63
* Parts of the custom UI component were developed with advice from Claude (Anthropic).
* Sounds and music: TODO
