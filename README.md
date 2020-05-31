#### Auteur : VALLEE Roxane & VEYSSET Sarah

# Fortune Game

Projet de math en IMAC 2 :  Jeu du solitaire sur le thème de la voyance. Ce projet utilise des « composantes aléatoires » maîtrisées par des paramètres.

Ce jeu est réalisé sur Unity avec le langage C#.

## 1/ Présentation et descriptif du jeu

<a href="https://zupimages.net/viewer.php?id=20/22/hgek.png"><img src="https://zupimages.net/up/20/22/hgek.png" alt="" /></a>

Le jeu que nous avons décidé d’implémenter est un solitaire. Le but du solitaire est de former 4 piles de cartes (chaque pile comprenant les cartes d’un même symbole) classées dans l’ordre croissant : As, 2, 3, 4, 5, 6, 7, 8, 9, 10, Valet, Dame, Roi. Ces 4 piles sont à réaliser à côté des 7 colonnes. Chaque pile doit commencer par un As. Au dessus du jeu du solitaire, une voyante vous accompagnera tout au long du jeu.

## 2/ Composantes aléatoires du jeu

### Composante numéro 1 : La couleur de la carte

Espace d’état : toutes les couleurs disponibles : {rouge, noire}
Paramètre(s) : Plus ou moins de chance d’avoir une carte rouge ou une carte noire


### Composante numéro 2 : La famille de la carte

Espace d’état : toutes les familles disponibles : {piques, carreaux, coeurs, trêfles}
Paramètre(s) : Plus ou moins de chance d’avoir une famille plutôt qu'une autre


### Composante numéro 3 : La valeur de la carte 

Espace d’état : tirage des cartes : 52 cartes
Paramètre(s) : Plus ou moins de chance d'avoir une carte plutôt qu'une autre


### Composante numéro 4 : Le dialogue de la voyante

Espace d’état : Ensemble de phrases de prédictions
Paramètre(s) : Plus ou moins de chance d’avoir des textes gentils ou des textes méchants


### Composante numéro 5 : L'humeur de la voyante

Espace d’état : les 2 humeurs (gentille ou méchante)
Paramètre(s) : Plus ou moins de chance d'avoir une voyante méchante ou gentille

## 4/ Description de(s) structure(s) de corrélation

La famille de la carte dépend de la couleur de la carte obtenu. En effet, lorsque le joueur obtient une carte rouge, la famille de la carte sera forcément coeurs ou carreaux. De même pour une carte noire, la famille de la carte sera forcément piques ou trèfles.





