# Compass System for Unity

A customizable and extendable compass system for Unity that displays directional and landmark icons in the game world. This system is ideal for adventure, RPG, and exploration-based games where navigation and world orientation are essential.

---

## Features

- 📍 **Landmark Icons**  
  Icons appear on the compass, pointing toward configured landmarks in the game world.

- 🔍 **Distance-Based Scaling & Fading**  
  Landmark icons automatically scale and change transparency based on the player's distance from them.

- 🧭 **Configurable Directional Markers**  
  Add only the directional icons you need — North, East, South, West, or any custom combination. You can also disable them entirely.

- 🎨 **Icon Selection & Sorting**  
  Includes a configurable icon list, making it easy to assign and sort icons for landmarks.

- 🧩 **Extendable Landmark Behavior**  
  Developers can override default behavior by inheriting from the `Landmark` class to create custom landmark interactions based on the player's position or game logic.

---

## Adding the Compass System to Your Unity Project

You can integrate the Compass system into your Unity project by either importing it as a Unity package or by downloading the full project as a ZIP file. The method you choose depends on how you want to access and use the Compass system (with or without demo content).

### Option 1: Importing the Unity Package

1. In this repository, navigate to the `UnityPackages` folder.
2. Choose one of the following packages:
   - `CompassSystem`: Contains only the core compass system.
   - `CompassSystem_WithDemo`: Includes the core system along with a demo scene.
3. Click on your preferred package, then click the three dots (`...`) on the right side.
4. Select **Download** from the dropdown menu.
5. Open your Unity project.
6. In the **Project** window, right-click and choose **Import Package > Custom Package...**.
7. Locate the `.unitypackage` file you just downloaded and click **Open** to import it.

> ℹ️ **Note:** If you imported the `CompassSystem_WithDemo` package and want to remove the demo content, you can delete the `LordBreakerX/CompassSystem/Demo` folder from your project's `Assets` directory.

### Option 2: Downloading the Full Project as a ZIP

1. From the root of the repository, click the green **Code** button.
2. Select **Download ZIP** from the dropdown menu.
3. Unzip the downloaded file.
4. You can now either:
   - Open Unity Hub and add the unzipped folder as a full Unity project.
   - Or, if you want to add the Compass system to an existing Unity project, drag the `LordBreakerX` folder into your project's **Assets** folder or directly into the **Project** window in Unity.

> ⚠️ **Note:** This option includes the demo content by default, located at `LordBreakerX/CompassSystem/Demo` folder from your project's `Assets` directory. If you prefer not to include the demo, you can simply delete this folder after importing.

---

## Documentation

For detailed usage instructions, examples, and advanced configuration options, refer to the [Compass System Wiki](https://github.com/YourUsername/YourRepository/wiki).
