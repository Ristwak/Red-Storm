
# 🌌 Red Storm – A VR Survival Experience  

**Red Storm** is a fast-paced VR survival challenge set on the harsh Martian surface.  
Caught in a raging sandstorm, the player must answer quick quizzes, follow their compass, manage time, and reach the safety of the base before the storm overtakes them.  

---

## 🎮 Gameplay Overview  

- At the start of the mission, the player answers a set of **quiz questions** about Mars.  
- A **wristwatch** on the **left hand** displays the countdown timer.  
- A **compass** on the **right hand** points toward the base.  
- A massive **sandstorm wall** constantly chases the player.  
- The goal: **Reach the base before the timer runs out or the storm catches you.**  

---

## 🕹️ Features  

- ⏱️ **Wristwatch Timer** – Displays the time left for survival.  
- 🧭 **Compass Guidance** – Always points toward the base.  
- 🌪️ **Dynamic Sandstorm Wall** – Moves with variable speed, chasing the player.  
- ❓ **Quiz System** – Randomized questions loaded from JSON, appear at mission start.  
- ⚡ **Win/Lose Conditions** – Player either reaches the base safely or loses if the storm catches them / time runs out.  
- 🎧 **Immersive VR Support** – Designed for XR Interaction Toolkit.  

---

## 📂 Project Structure  

Assets/
│── Scripts/
│ ├── GameManager.cs
│ ├── QuizLoader.cs
│ ├── CompassController.cs
│ ├── SandWallMover.cs
│ ├── BaseTrigger.cs
│ └── CanvasFollower.cs
│
│── Resources/
│ └── mars_quiz.json
│
│── Prefabs/
│ ├── Compass.prefab
│ ├── Wristwatch.prefab
│ └── SandstormWall.prefab

---

## 🚀 How to Play  

1. Start the game – quiz panel appears with randomized questions.  
2. Answer all questions to begin the mission.  
3. Use the **compass (right hand)** to navigate.  
4. Watch your **timer (left hand wristwatch)**.  
5. Reach the **base** before the sandstorm collides with you or time runs out.  

---

## ⚙️ Setup Instructions  

1. Clone this repository:  
   ```bash
   git clone https://github.com/Ristwak/red-storm.git
2. Open the project in Unity 2021.3 LTS (or newer).
3. Ensure you have:
   XR Interaction Toolkit
   TextMeshPro
   URP or HDRP (recommended for better visuals)
4. Place the mars_quiz.json file inside:
   Assets/Resources/
5. Assign references in the Unity inspector:
   Compass → Player & Needle
   SandWall → Player target
   GameManager → Panels & Locomotion script
6. Build for VR platform (Oculus / OpenXR).

---

## 🏆 Win & Lose Conditions

✅ Win: Player reaches the base within time.

❌ Lose: Timer hits 0 or the sandstorm catches the player.

---

## 📜 License

This project is licensed under the MIT License – see the LICENSE file for details.
