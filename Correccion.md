# Corrección, Trabajo Práctico N°07 (Side Scroller 2D)

- **Alumno/a:** Tournon
- **Entrega:** `TP07_C1_Tournon_Bully`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP07:** 4


## 1. Checklist de requisitos

| #   | Requisito | Estado | Evidencia |
|-----|-----------|--------|-----------|
| R1  | Repositorio nuevo con nombre `TP07_C1_Apellido_Titulo` | ✓ | La carpeta y el remoto `origin` son `TP07_C1_Tournon_Bully` |
| R2  | Proyecto de Unity con el mismo nombre | Parcial | `ProjectSettings/ProjectSettings.asset:16` dice `productName: TP07_Tournon_Bully` (falta `C1`) |
| R3  | El personaje va a derecha e izquierda | ✓ | `Assets/Scripts/Player/Playermovement.cs:100-149` |
| R4  | El personaje puede disparar | ✓ | `Assets/Scripts/Player/Playermovement.cs:152-162`, `Assets/Scripts/Player/Bullet.cs:17` |
| R5  | El personaje puede saltar | ✓ | `Assets/Scripts/Player/Playermovement.cs:86-88` con `FloorDetect.IsGrounded` |
| R6  | Enemigos u obstáculos que quitan vida al jugador | ✓ | `Assets/Scripts/Player/Playermovement.cs:169-188` (`enemyMask` es la capa `enemy`) |
| R7  | Se eliminan con más de 2 disparos, con vida y barra visible arriba (Canvas World) | ✓ | `Assets/Scripts/Enemies/Enemies.cs:66-80` (0.25 por bala, 4 disparos), Canvas en World Space en `Game.unity` |
| R8  | ParticleSystem al destruir al enemigo | ✓ | `Assets/Scripts/Enemies/Enemies.cs:56`, `Assets/Prefabs/Cacty death.prefab` |
| R9  | Pickables | ✓ | `Assets/Scripts/Pickeables/Money.cs`, `Health.cs`, `PowerUP.cs`, `Invincibility.cs` |
| R10 | Sonido en todas las acciones | Parcial | Música (`BackgroundMusic` con `night-detective-226857.mp3` en loop), salto (`Playermovement.cs:90`), disparo (`Playermovement.cs:158`), coleccionable (`Money.cs:18`), muerte de enemigo (`Enemies.cs:60`). Te falta el sonido al perder, porque no hay condición de derrota |
| R11 | Volumen configurable desde Settings conectado a un AudioMixer | ✓ | `Assets/Scripts/Audio/Audio.cs:36-55`, `Panel Options` en `Game.unity`, 4 parámetros expuestos en `SFX.mixer` |
| R12 | Usa ScriptableObject para datos o configuraciones | ✗ | Ninguna clase de `Assets/Scripts` hereda de `ScriptableObject`. Velocidad, salto, daño y duración de la invencibilidad están hardcodeados o en el inspector |
| R13 | Power ups o modificación de jugabilidad | ✓ | `Assets/Scripts/Pickeables/PowerUP.cs:17` (daño doble), `Invincibility.cs:16` (10 s invencible) |
| R14 | Personaje animado | ✓ | `Assets/Animations/Player/Player.controller` con `idle.anim`, `Walk.anim`, `shot.anim`, `Hit.anim` |
| R15 | Escena de menú principal y otra de Gameplay | ✓ | Están `Assets/Scenes/Title Screen.unity` y `Assets/Scenes/Game.unity`, y `Main Screen.cs:32` carga `"Game"` |

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repositorio público | ✓ | El repositorio es público |
| P3 | Nombre del repo | ✓ | `TP07_C1_Tournon_Bully` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, escenas y capas en inglés. Los textos de `[Header]` ("Parametros", "Movimiento") y los comentarios están en español, y eso está permitido |
| P5 | Un repo por TP, sin branches | ✓ | 5 commits en `master`, sin contenido de otros TPs |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Assets/Scenes/` | ✓ | |
| `Assets/Scripts/` | ✓ | Subcarpetas propias (`Player`, `Enemies`, `Pickeables`, `UI`, `Audio`, `Camera`, `Main Screen`) en lugar de `Game/`, `Ui/`, `Data/`. Está bien así |
| `Assets/Prefabs/` | ✓ | |
| `Assets/Art/Sprites/` | ✗ | Los sprites están en `Assets/Art/1 Main Characters/`, `4 Enemies/`, `5 GUI/`, etc. |
| `Assets/Art/Animations/` | ✗ | Las animaciones están en `Assets/Animations/` (raíz de Assets) |
| `Assets/Art/Audio/` | ✗ | Los audios están en `Assets/SFX/sounds/` |
| `Assets/Data/` | No aplica | No hay ScriptableObjects |

Archivos mal ubicados:

- `Assets/Art/Palletes/Level 1.prefab`: es un prefab, va en `Assets/Prefabs/`.

Cosas que sobran en el repo: `Assets/TextMesh Pro/Examples & Extras/` importado completo y la carpeta `.idea/` versionada.

## 4. Qué le falta

- Renombrar `Assets/Scripts/Main Screen/Main Screen.cs` a `MainScreen.cs` para que coincida con su clase.
- Un `ScriptableObject` con los datos del jugador o de los enemigos (velocidad, fuerza de salto, daño, vida, duración de los power ups). Es requisito de la consigna.
- Condición de derrota y sonido al perder cuando `playerLifebar.fillAmount` llega a 0 (`Playermovement.cs:183`).
- Que `productName` en Player Settings sea `TP07_C1_Tournon_Bully`.
- Pasar la física de `Update` a `FixedUpdate` en `Playermovement`, `Enemies` y `Cacty`.
- Unificar `Enemies.cs` y `Cacty.cs`, que son la misma clase con otra velocidad.
- Corregir el `AddListener` de `UIManager.cs:149` dentro de `OnDestroy`.

## 5. Hallazgos de código

### Error (3)
- `Assets/Scripts/UI/UIManager.cs:120` No se puede volver a jugar: no hay derrota (la vida no se chequea en 0, `Playermovement.cs:183`) y el único botón del panel de victoria (`OnWinExitButtonClicked`) cierra la aplicación con `Application.Quit()`. No hay Retry ni vuelta al menú.
- `Assets/Scripts/Player/Playermovement.cs:183` La vida baja pero nunca chequeás si llegó a 0: no hay derrota ni sonido al perder (R10).
- `Assets/Scripts/Main Screen/Main Screen.cs:7` La clase `MainScreen` está en un archivo que se llama distinto. Unity lo compila pero tira un warning: renombrá el archivo o la clase para que coincidan.

### Bug (4)
- `Assets/Scripts/Player/Playermovement.cs:112` `SetTrigger("walk")` y `SetTrigger("Idle")` se disparan todos los frames, y en el mismo frame, desde los dos bloques de input.
- `Assets/Scripts/Player/Playermovement.cs:122` Al soltar la tecla ponés `linearVelocity` en cero, incluida la Y, y el personaje se frena en el aire.
- `Assets/Scripts/Player/Playermovement.cs:152` Se puede disparar con el juego en pausa (`Time.timeScale = 0`).
- `Assets/Scripts/UI/UIManager.cs:149` En `OnDestroy` agregás un listener en vez de quitarlo.

### Warning (18)
- `Assets/Scripts/Player/Playermovement.cs:12` `jumpForce` es public.
- `Assets/Scripts/Player/Playermovement.cs:16` `powerUp` e `invincible` son public y los cambian otras clases.
- `Assets/Scripts/Player/Playermovement.cs:50` `sr` es public y no se usa.
- `Assets/Scripts/Player/Playermovement.cs:105` `AddForce` desde `Update`.
- `Assets/Scripts/Player/Playermovement.cs:138` No hay límite de velocidad horizontal (`SpeedLimit()` está comentado).
- `Assets/Scripts/Player/Bullet.cs:17` La bala no tiene tiempo de vida.
- `Assets/Scripts/Player/Bullet.cs:27` La bala se destruye con cualquier trigger; `enemyMask` no se usa.
- `Assets/Scripts/Player/FloorDetect.cs:9` `[SerializeField] public`.
- `Assets/Scripts/Enemies/Enemies.cs:37` `linearVelocity` en `Update`.
- `Assets/Scripts/Enemies/Enemies.cs:59` `Play()` sobre el prefab, no sobre la instancia.
- `Assets/Scripts/Enemies/Cacty.cs:37` `linearVelocity` en `Update`.
- `Assets/Scripts/Enemies/Cacty.cs:59` `Play()` sobre el prefab.
- `Assets/Scripts/Audio/Audio.cs:8` Campos `[SerializeField] public`.
- `Assets/Scripts/Audio/Audio.cs:21` El valor inicial de los sliders (-30) no se aplica al mixer, y el máximo es +20 dB.
- `Assets/Scripts/Camera/CameraScript.cs:10` `myPos` y `myPlay` son public.
- `Assets/Scripts/UI/UIManager.cs:18` Botones `[SerializeField] public`.
- `Assets/Scripts/UI/UIManager.cs:33` `moneyCounter` es public y lo incrementa `Money.cs:17`.
- `Assets/Scripts/UI/UIManager.cs:53` El texto del contador se actualiza todos los frames.

### Suggestion (17)
- `Assets/Scripts/Player/Playermovement.cs:33` El campo `flip` no se usa y lo tapan variables locales.
- `Assets/Scripts/Player/Playermovement.cs:40` `playerMask` sin uso.
- `Assets/Scripts/Player/Playermovement.cs:76` Duración de invencibilidad `10` hardcodeada.
- `Assets/Scripts/Player/Playermovement.cs:174` `if (invincible)` con el cuerpo vacío.
- `Assets/Scripts/Player/FloorDetect.cs:2` `using` sin uso.
- `Assets/Scripts/Enemies/Enemies.cs:14` Cada enemigo depende de `Playermovement` para leer `powerUp`.
- `Assets/Scripts/Enemies/Enemies.cs:53` Chequeo de muerte en cada frame.
- `Assets/Scripts/Enemies/Cacty.cs:7` Clase duplicada de `Enemies`.
- `Assets/Scripts/Audio/Audio.cs:53` El parámetro `"Music"` tiene otro naming, y hay strings mágicos.
- `Assets/Scripts/Camera/CameraScript.cs:14` Seguimiento de cámara en `Update` en vez de `LateUpdate`.
- `Assets/Scripts/Main Screen/Main Screen.cs:20` `Update` vacío.
- `Assets/Scripts/Main Screen/Main Screen.cs:32` Nombre de escena como string mágico.
- `Assets/Scripts/Pickeables/Health.cs:25` Curación `0.25f` hardcodeada.
- `Assets/Scripts/Pickeables/PowerUP.cs:17` El power up no expira.
- `Assets/Scripts/UI/UIManager.cs:63` El código del sonido de botón se repite en seis handlers.
- `Assets/Scripts/UI/UIManager.cs:69` `OnMenuButtonClicked` sin uso.
- `Assets/Scripts/UI/UIManager.cs:133` `setMoneyCount` está en camelCase y `lifeText` muestra monedas.

## 6. Recomendaciones

1. Hacé que el juego se pueda volver a jugar: agregá la condición de derrota en `Playermovement.cs:183` (si la vida llega a 0, panel de Game Over) y poné en los paneles de victoria y derrota un botón Retry que recargue la escena y otro que vuelva al menú, en lugar de que el único botón cierre la aplicación (`UIManager.cs:115-122`).
2. Renombrá `Assets/Scripts/Main Screen/Main Screen.cs` a `MainScreen.cs` para que coincida con su clase. Hacelo desde el editor de Unity, así se conserva el `.meta`.
3. Creá un `ScriptableObject` (por ejemplo `PlayerData` con `speed`, `jumpForce`, `damageTaken` e `invincibilityDuration`, y `EnemyData` con `speed` y `damagePerBullet`) y leelo desde `Playermovement`, `Enemies` y `Cacty` (R12).
4. Agregá la condición de derrota en `Playermovement.cs:183`: panel de Game Over, clip de derrota por el mixer y pausa (R10).
5. Corregí `productName` a `TP07_C1_Tournon_Bully` en Player Settings (R2).
6. Pasá la física a `FixedUpdate`, dejá de anular la Y al soltar la tecla y reemplazá los triggers `walk`/`Idle` por un parámetro `Speed` en el Animator.
7. Cambiá los campos `public` y `[SerializeField] public` por `[SerializeField] private`, con properties o métodos donde otra clase los necesite.
8. Unificá `Enemies` y `Cacty`, dale tiempo de vida a la bala y filtrá sus colisiones con `enemyMask`.
9. Corregí `UIManager.cs:149` (`RemoveAllListeners` en vez de `AddListener`) y bloqueá el disparo durante la pausa.
10. Reordená `Assets/` según la estructura pedida (`Art/Sprites`, `Art/Animations`, `Art/Audio`) y mové `Level 1.prefab` a `Prefabs/`.
