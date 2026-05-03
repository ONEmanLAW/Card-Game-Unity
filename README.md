# Card Game : Projet Unity
 
Projet Unity réalisé dans le cadre d'un cours à l'école. J'ai décidé de faire un **card game** simple, jouable contre un adversaire (IA).
 
## Le concept
 
Un jeu de cartes où chaque joueur commence avec **10 points de vie**. Le but est simple : faire descendre les PV de l'adversaire à **0**.
 
## Les cartes
 
Il y a 4 cartes dans le jeu :
 
| Carte | Sacrifice nécessaire |
|-------|----------------------|
| 🦋 Libellule | Aucun |
| 🐿️ Squirrel | Aucun |
| 🐺 Wolf | 1 sacrifice |
| 🐉 Dragon | 2 sacrifices |
 
Chaque carte a des **points de dégâts à gauche et à droite**.
 
## Les règles
 
- Le terrain se joue sur **4 rangées en vertical**.
- Quand une carte se trouve face à une autre carte (verticalement), les deux cartes s'affrontent.
- **L'attaque et la défense se font uniquement entre les deux cartes face à face** (pas de calcul global de défense).
- Si une carte a plus de dégâts que celle d'en face, elle la **détruit**.
- Si aucune carte ne se trouve en face, les dégâts vont **directement à l'adversaire**.
### Limites par tour
 
- Maximum **2 cartes posées** par tour
- Maximum **1 sacrifice** par tour
- Quand tu as fini ton tour, clique sur la **cloche** pour passer au tour suivant
## Esthétique et modélisation
 
Pour la partie visuelle, j'ai utilisé un pack d'assets que j'ai trouvé. La scène principale se trouve dans :
 
```
Daniel Misage > scenes
```
 
Vous y verrez tous les changements que j'ai apportés personnellement :
 
- Import d'un perso que j'ai animé pour qu'il bouge et serve d'adversaire
- Une main trouvée en ligne pour tenir les cartes
- Un modèle de carte qui m'a plu, que j'ai pas mal modifié sur **Photoshop** pour avoir des cartes à mon goût et plus simples
## Limitations / Ce qui n'est pas fini
 
Honnêtement, je n'ai pas eu le temps de tout finir :
 
- Je n'ai pas pu importer correctement tous les sons du jeu et les placer comme je voulais
- Je n'ai pas eu les compétences techniques pour améliorer l'IA de l'adversaire
## Comment lancer le projet

1. Install zip / or commit puis ouvir projet dans unityHub
2. Ouvrir le projet dans Unity
3. Boutton play
