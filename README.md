# 🎮 Daddy Click

A Unity-based idle clicker game with cloud synchronization, remote configuration, and progression systems. Collect figures, unlock daddies, and maximize your clicking potential!

---

## 📖 About The Game

**Daddy Click** is an incremental clicker game where players earn currency through manual clicks and passive income generation. The game features a collection system with different "Figures" that generate clicks per second (CPS) and "Daddies" that provide multipliers to boost your earnings. Progress is automatically saved to the cloud, allowing players to continue their journey across devices.

---

## 🎯 Gameplay

### Core Mechanics
- **Manual Clicking**: Click the main button to earn currency with each tap
- **Passive Income**: Figures generate clicks per second (CPS) automatically
- **Multiplier System**: Unlock Daddies to multiply your earnings
- **Idle Earnings**: Continue earning while away from the game (with caps)
- **Progression**: Purchase more figures and unlock new daddies to increase your income exponentially

### Game Loop
1. Click to earn initial currency
2. Purchase figures to generate passive income
3. Unlock daddies to multiply all earnings
4. Collect idle rewards when returning to the game
5. Continue expanding your collection for bigger numbers!

---

## 🛠️ Core Systems

### 🔐 Authentication System
- **Unity Gaming Services Authentication**: Secure player identity management
- **Player Accounts Integration**: Sign-in with Unity Player Accounts
- **Persistent Login**: Automatic re-authentication using stored access tokens
- **Cross-Platform Support**: Play on any device with the same account

**Implementation**: `LoginCanvas.cs`, utilizing Unity Services Authentication and Player Accounts packages.

---

### ☁️ Cloud Save System
- **Unity Cloud Save**: All player progress automatically synced to the cloud
- **Auto-Save**: Progress saved every 60 seconds
- **Save on Quit**: Data saved when exiting the game or app pausing
- **Cross-Device Sync**: Continue your progress on any device

**Saved Data**:
- Player currency/clicks
- Owned figures and their quantities
- Unlocked daddies
- Selected skin
- Last played timestamp

**Implementation**: `PlayerSave.cs`, `SaveHandler.cs` with Unity Cloud Save Service.

---

### 🎛️ Remote Config System
- **Environment-Based Configuration**: 
  - Development environment for testing
  - Production environment for live builds
- **Dynamic Pricing**: Daddy costs fetched from remote config
- **Hot Updates**: Balance changes without app updates
- **A/B Testing Ready**: Different configurations for different player segments

**Implementation**: `RemoteConfig.cs` with Unity Remote Config Service.

---

### 💤 Idle Reward System
- **Offline Earnings**: Continue earning while away from the game
- **Capped Rewards**: Maximum idle earnings based on your CPS and multiplier
- **Welcome Back Bonus**: Popup showing accumulated idle rewards
- **Fair Calculation**: Idle earnings are 10% of active CPS to maintain balance

**Features**:
- Calculates time elapsed since last play
- Considers CPS and multiplier in calculations
- Displays rewards with progress bar
- Caps maximum idle earnings to prevent exploitation

**Implementation**: `CurrencyIdleReceiverManager.cs`, `IdleRewardUI.cs`

---

### 🎮 Click System
- **Manual Clicks**: Direct player interaction with multiplier applied
- **Auto Clicks**: Passive CPS generation every second
- **Visual Feedback**: Popup numbers showing click values
- **Object Pooling**: Efficient popup management for performance

**Implementation**: `ClickManager.cs`, `ClickButton.cs`, `ClickPopupManager.cs`

---

### 🏪 Shop System
- **Figure Shop**: Purchase figures that generate CPS
  - Each figure type provides different CPS amounts
  - Cost scaling as you buy more
  - Quantity tracking per figure type
  
- **Daddy Shop**: Unlock daddies for multipliers
  - Each daddy provides a unique multiplier
  - Remote config pricing
  - Skin selection system

**Implementation**: `BaseShopUI.cs`, `BaseShopItem.cs`, Shop UI components

---

### 🎨 Skin System
- **Unlockable Skins**: Daddies serve as both multipliers and cosmetic skins
- **Skin Selection**: Switch between unlocked daddy appearances
- **Persistent Selection**: Selected skin saved locally and in cloud

**Implementation**: `SkinsUI.cs`, `SkinButtonUI.cs`, `DaddyManager.cs`

---

### 📊 Manager Architecture
- **GameManager**: Orchestrates core game loop and auto-clicking
- **ClickManager**: Handles all currency and click operations
- **FigureManager**: Manages figure ownership and data
- **DaddyManager**: Handles daddy unlocks and multipliers
- **SaveHandler**: Automatic save scheduling and lifecycle management

---

### 🎨 UI Systems
- **HUD Manager**: Displays current stats (currency, CPS, multiplier)
- **Loading UI**: Smooth transitions between game states
- **Shop Controllers**: Dynamic shop interface with live data
- **Shelf UI**: Visual display of owned figures
- **Popup System**: Floating click value indicators

---

### ⚡ Performance Optimizations
- **Object Pooling**: Reusable popup objects to reduce garbage collection
  - Generic pool system: `GenericPool.cs`
  - Poolable interface: `IPoolable.cs`
  - Efficient memory management for frequently spawned objects
  
- **Async Operations**: Non-blocking save/load operations
- **Event-Driven Architecture**: Decoupled systems using C# events

---

## 🏗️ Technical Stack

### Unity Features
- **Unity 2022.3+ (LTS)** - Game engine
- **Universal Render Pipeline (URP)** - Rendering
- **TextMesh Pro** - UI text rendering
- **Input System** - Modern input handling
- **Coroutines** - Time-based operations

### Unity Gaming Services
- **Authentication** - Player identity
- **Cloud Save** - Data persistence
- **Remote Config** - Dynamic configuration

### Third-Party Assets
- **Odin Inspector** - Enhanced Unity Inspector
- **Text Animator** - Animated text effects
- **VFolders/VHierarchy/VTabs** - Editor enhancements
- **Hot Reload** - Runtime code updates

---

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Game/
│   │   ├── Manager/
│   │   │   ├── ClickManager.cs
│   │   │   ├── FigureManager.cs
│   │   │   └── DaddyManager.cs
│   │   ├── ObjectPool/
│   │   ├── GameManager.cs
│   │   └── CurrencyIdleReceiverManager.cs
│   ├── RemoteConfig/
│   │   └── RemoteConfig.cs
│   ├── Save/
│   │   ├── PlayerSave.cs
│   │   └── SaveHandler.cs
│   ├── UI/
│   │   ├── LoginCanvas.cs
│   │   ├── IdleRewardUI.cs
│   │   ├── HUDUIManager.cs
│   │   ├── Shop/
│   │   ├── Popup/
│   │   └── Shelf/
│   ├── SOs/ (ScriptableObjects)
│   └── Utils/
├── Scenes/
├── Prefabs/
├── Sprites/
└── Settings/
```

---

## 🚀 Getting Started

### Prerequisites
- Unity 2022.3 LTS or later
- Unity Gaming Services account
- Unity project linked to Unity Cloud

### Setup
1. Clone the repository
2. Open project in Unity
3. Configure Unity Gaming Services:
   - Link project to Unity Cloud
   - Enable Authentication
   - Enable Cloud Save
   - Enable Remote Config
4. Configure Remote Config:
   - Set up development and production environments
   - Add daddy cost keys (e.g., "DaddyNameCost")
5. Build and run!

---

## 🎲 Game Design Features

### Progression Curve
- **Early Game**: Manual clicking focus
- **Mid Game**: Figure purchasing for passive income
- **Late Game**: Daddy unlocking for exponential growth
- **End Game**: Optimization and collection completion

### Retention Mechanics
- Idle rewards encourage return visits
- Daily progression goals
- Collection completion satisfaction
- Incremental power growth

### Monetization Ready
- Remote config enables dynamic pricing
- Cloud save prevents cheat exploitation
- Framework ready for IAP integration

---

## 🤝 Contributing

This is a personal/commercial project. For inquiries, please reach out to the project owner.

---

## 📄 License

All rights reserved. This project is proprietary software.

---

## 🎮 Credits

Developed using Unity Engine and Unity Gaming Services.

Special thanks to third-party asset creators:
- Sirenix (Odin Inspector)
- Febucci (Text Animator)
- VHierarchy/VFolders/VTabs

---

## 📧 Contact

For questions or support, please contact the development team.

---

**Happy Clicking! 🖱️✨**


