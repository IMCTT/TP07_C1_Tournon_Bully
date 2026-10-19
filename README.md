[README_BullyTheCannon.md](https://github.com/user-attachments/files/33689139/README_BullyTheCannon.md)
# Bully the Cannon

Sidescroller en el que controlás a Bully, un cañón con mucho carácter. Avanzá por el nivel, saltá los obstáculos y disparale a todo lo que se cruce en tu camino.

🎮 **Jugalo en Itch.io:** [link al juego](#) <!-- TODO: reemplazar por el link real -->

---

## 🎮 Controles

| Acción | Tecla |
|---|---|
| Mover izquierda | A |
| Mover derecha | D |
| Mover arriba | W |
| Mover abajo | S |
| Saltar | Espacio |
| Disparar | R |

---

## 🧱 Arquitectura del proyecto

```
Assets/
├── Scripts/
│   ├── PlayerController.cs     # Movimiento (WASD) y salto
│   ├── PlayerShooting.cs       # Disparo (R)
│   ├── Projectile.cs           # Comportamiento de los proyectiles
│   ├── GameManager.cs          # Estado general de la partida
│   └── UI/                     # Menús y HUD
└── Scenes/
    └── MainScene.unity
```

<!-- TODO: ajustar los nombres de scripts a los reales de tu proyecto -->

---

## 🚀 Cómo correrlo localmente

1. Cloná el repositorio:
   ```bash
   git clone https://github.com/<usuario>/BullyTheCannon.git
   ```
2. Abrí el proyecto con **Unity <versión utilizada>** <!-- TODO: completar versión de Unity -->.
3. Abrí la escena principal: `Assets/Scenes/MainScene.unity`.
4. Play.

---

## 👤 Créditos

- **Desarrollo**: Tomas
- **Assets utilizados**: <!-- TODO: listar fuentes de sprites/audio -->

---

## 📌 Estado del proyecto

- [x] Movimiento con WASD
- [x] Salto con Espacio
- [x] Disparo con R
- [ ] Enemigos
- [ ] Sistema de vidas / puntaje
- [ ] Pantalla de Game Over
