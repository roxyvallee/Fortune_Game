#### Auteur : VALLEE Roxane & VEYSSET Sarah

# Fortune Game

Projet de math en IMAC 2 :  Jeu Oracle où vous pouvez tirer des cartes pour connaître votre avenir à partir de différents paramètres.

Ce jeu est réalisé sur Unity.

## 1/ Présentation et descriptif du jeu

<a href="https://zupimages.net/viewer.php?id=20/12/ehzu.jpg"><img src="https://zupimages.net/up/20/12/ehzu.jpg" width="200" alt=""/></a>

Jeu de tarot divinatoire permettant de tirer les cartes et de connaître son avenir à partir de différents paramètres. Grâce à la voyante, vous aurez le choix entre découvrir votre avenir via l'horoscope ou découvrir votre avenir via un tirage de carte. Mais attention, à celle-ci qui pourrait influencer sur le tirage selon son humeur.

## 2/ Composantes aléatoires du jeu

### Composante numéro 1 : Le thème

Espace d’état : tous les thèmes disponibles : {amour, famille, travail}
Paramètre(s) : Plus ou moins de chance d’avoir le thème que l’on souhaite selon l’humeur de la voyante


### Composante numéro 2 : Carte choisie

<a href="https://zupimages.net/viewer.php?id=20/12/0yog.jpg"><img src="https://zupimages.net/up/20/12/0yog.jpg" alt="" width="400"/></a>

Espace d’état : tirage du tarot : 22 cartes
Paramètre(s) : Plus ou moins de chance d’avoir une bonne carte en fonction de l’humeur de la voyante


### Composante numéro 3 : À l’Envers ou à l’endroit

<a href="https://zupimages.net/viewer.php?id=20/12/8mt6.png"><img src="https://zupimages.net/up/20/12/8mt6.png" alt="" width="400"/></a>

Espace d’état : tirage du tarot : 22 cartes
Paramètre(s) : Plus ou moins de chance qu’elle soit à l’endroit (bonne chance) en fonction de l’humeur de la voyante. 


### Composante numéro 4 : Horoscope

<a href="https://zupimages.net/viewer.php?id=20/12/voxm.jpg"><img src="https://zupimages.net/up/20/12/voxm.jpg" alt="" width="400"/></a>

Espace d’état : Ensemble de textes de prédictions
Paramètre(s) : Plus ou moins de chance d’avoir certains textes en fonction de son signe astrologique


### Composante numéro 5 : La voyante

<a href="https://zupimages.net/viewer.php?id=20/12/v95z.jpg"><img src="https://zupimages.net/up/20/12/v95z.jpg" alt="" width="400"/></a>

Espace d’état : 3 type de voyantes
Paramètre(s)

Autres composantes aléatoires éventuelles

## 4/ Description de(s) structure(s) de corrélation

Le tirage de cartes dépend du thème choisi aléatoirement. En effet on choisira un paquet différent en fonction du thème sélectionné. 

Si la carte est à l’envers ou à l’endroit dépend de la carte tirée.   





