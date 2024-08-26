# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0-preview](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/3.0.0-preview) - 2024-08-26  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/7?closed=1)  
    

### Added

- Add UI Toolkit support ([#26](https://github.com/unity-game-framework/ugf-debugtools/issues/26))  
    - Update dependencies: `com.ugf.editortools` to `3.0.0-preview.13`, `com.ugf.runtimetools` to `3.0.0-preview.18`, add `com.unity.modules.uielements` of `1.0.0`, and remove of `com.unity.modules.imgui`.
    - Add _UI Toolkit_ implementation.
    - Remove _IMGUI_ implementation.

## [2.0.0-preview.1](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/2.0.0-preview.1) - 2024-07-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/6?closed=1)  
    

### Added

- Add disable UI event system when displayed ([#27](https://github.com/unity-game-framework/ugf-debugtools/issues/27))  
    - Add `DebugUIFunctionDrawer.DisplayMenuDisableEventSystem` property to determine whether to disable _UI Event System_ when menu is displayed, in cases when _UI_ package installed.
- Add console section ([#25](https://github.com/unity-game-framework/ugf-debugtools/issues/25))  
    - Add `DebugUISectionLog` and related classes as _Debug_ section to display recorded logs.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/2.0.0-preview) - 2024-02-13  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/5?closed=1)  
    

### Changed

- Update to latest Unity ([#23](https://github.com/unity-game-framework/ugf-debugtools/issues/23))  
    - Update dependencies: `com.ugf.editortools` to `3.0.0-preview.2`, `com.ugf.builder` to `2.0.2` and `com.ugf.initialize` to `2.9.0` versions, add `com.ugf.runtimetools` of `3.0.0-preview.7` version.
    - Update package _Unity_ version to `2023.2`.
    - Update package registry to _NPM Hub_.
    - Change _Debug UI_ and _Debug GL_ classes to the latest standards.

## [1.0.0-preview.4](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/1.0.0-preview.4) - 2021-10-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/4?closed=1)  
    

### Fixed

- Fix DebugUIWindowDrawer using editor code ([#21](https://github.com/unity-game-framework/ugf-debugtools/pull/21))  
    - Fix `DebugUIWindowDrawer` class to use runtime scroll view scope instead of editor one.

## [1.0.0-preview.3](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/1.0.0-preview.3) - 2021-09-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/3?closed=1)  
    

### Fixed

- Fix debug GL component clear commands does not executed ([#19](https://github.com/unity-game-framework/ugf-debugtools/pull/19))  
    - Fix `DebugGLComponent` class to starts clear command coroutine after drawer creation.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/1.0.0-preview.2) - 2021-09-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/2?closed=1)  
    

### Added

- Add text color for all styles in skin ([#16](https://github.com/unity-game-framework/ugf-debugtools/pull/16))  
    - Change _Dark_ and _Light_ skins to have text color for all style states.
- Add debug UI section full screen alignment ([#14](https://github.com/unity-game-framework/ugf-debugtools/pull/14))  
    - Add `DebugUISectionAlignment.Full` enumerable item to define _UI_ sections alignment on full screen.

### Fixed

- Fix double enable of drawers and panels ([#17](https://github.com/unity-game-framework/ugf-debugtools/pull/17))  
    - Change dependencies: add `com.ugf.initialize` of `2.6.0` version.
    - Add `DebugGLComponent.DefaultShapes` property to determine whether to register default shapes.
    - Change `DebugGLDrawer` and `DebugUIDrawer` classes to implement initialization pattern.
    - Change `DebugUISection` to inherit from `InitializeBase` class, and remove `Enable()` and `Disable()` methods.
    - Change `IDebugUIDrawer` to inherit from `IInitialize` interface, and remove `Enable()` and `Disable()` methods.
    - Change `DebugGL.Drawer` property to make it overridable using `DrawerSet()` and `DrawerClear()` methods.
    - Change `DebugUI.Drawer` property to make it overridable using `DrawerSet()` and `DrawerClear()` methods.
    - Change `DebugGLComponent` and `DebugUIComponent` to create drawers and register them in `DebugGL` and `DebugUI`.
    - Remove `DebugGL.DefaultMaterial` property and related methods to control it, use `Drawer.DefaultMaterial` instead.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/1.0.0-preview.1) - 2021-09-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-debugtools/milestone/1?closed=1)  
    

### Changed

- Change UI and GL events execution ([#11](https://github.com/unity-game-framework/ugf-debugtools/pull/11))  
    - Change dependencies: remove `com.ugf.customsettings` package.
    - Add _GL_ rendering support for multiple cameras.
    - Add `DebugUIComponent` and `DebugGLComponent` componets to initialize and execute _UI_ and _GL_ drawers.
    - Add `DebugGL.DefaultMaterial` property and `SetDefaultMaterial()` and `ClearDefaultMaterial()` methods to control default material used when creating _GL_ draw commands.
    - Add `DebugGLUtility.CreateDefaultMaterial()` method to create default material used with _GL_ draw commands.
    - Remove `DebugGL.GetDefaultMaterial()` method, use `DefaultMaterial` property instead.
    - Remove `DebugGLUtility.DefaultMaterial` property, use `DebugGL.DefaultMaterial` property instead.
    - Remove `DebugUISettings` and `DebugGLSettings` project settings, use components instead.
    - Remove `DebugUIEventComponent` component class.
- Change GUI executer object not to be hidden ([#7](https://github.com/unity-game-framework/ugf-debugtools/pull/7))  
    - Change `DebugUIEventComponent` class to be public.
    - Change `DebugUIEventComponent` gameobject created without hidden flags.

### Removed

- Remove unused GL assets from package data ([#10](https://github.com/unity-game-framework/ugf-debugtools/pull/10))  

### Fixed

- Fix frame highlight skin text color when hover ([#8](https://github.com/unity-game-framework/ugf-debugtools/pull/8))  
    - Update Dark and Light skins.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-debugtools/releases/tag/1.0.0-preview) - 2021-09-20  

### Release Notes

- No release notes.


