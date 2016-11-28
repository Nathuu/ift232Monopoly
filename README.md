# ift239Monopoly
School work project; A monopoly game implemented in c# using WPF

Monopoly
 
<<<<<<< HEAD
Charles Lapierre 11097555
Bernard Cloutier 12101995
Samuel Whittom               11065303
Fanny Salvail-Bérard 16018131
Nathan Painchaud 14021471
Jonathan Nadeau  15089039
Sara Ait Bouziaren 15112860
Marc Dupuis  08 387 432
Frédéric Nadeau 16 100 247
Alexandre Fortin 15083160
 
Description:
                Jeu conventionnel de monopoly. Tour par tour. Multijoueur. Planche de jeu. Vider le porte feuille de son adversaire.
                Ajout au jeu conventionnel. Planche de jeu modifiable(mode édition mode jeu).
Fonctionnalités:
 
---Implémentation de base---
1.Planche de jeu par défaut (avant édition)(dans un fichier texte contenant un pack (version du jeu))
2.Afficher la planche de jeu
3.Piger des cartes
4.Afficher les joueurs sur la planche de jeu
5.Afficher le propriétaire de la case
6.Assigner une case à un joueur (suite à l'achat par un joueur de la case)
7.Certaine case activera la distribution d'une carte conséquence
8.Bouger un joueur  (changement de pointeur ou changement d'attribut)
9.Brasser un dé
9b. Brasser n dés
10.Taxe muicipale 10% de la somme de Brassée deux dés * X$
11.Modifier le compte en banque de chaque joueur
12.Transaction entre deux joueurs (Jonatan arrive sur Local D3-2037 qui appartient à Alex)
13.Additionner argent au porte feuille d'un joueur (fct appelé par la précédente)
14.Éliminer un joueur (Faillite)
15.Déterminer un gagnant et fin du jeu
16.Statistique de jeu
17.Trigger (exemple prener 1000$ en passant GO)
18.Mouvement automatique (aller en prison, Piger une carte qui dit visiter le Manoir de Fortin,...)
19.Lors du tour d'un ordinateur, simuler le temps qui passe (voir chacune des actions sur plusieurs secondes)
(au lieu d'un programme qui compile toute la partie en 1 ou 2 secondes)
20.Ajouter un mode édition pour implanter de nouvelles versions du jeu Monopoly
21.Créer un IA (adversaire(s) virtuel(s))
22.Envoi de messages taquins provenant de l'ordinateur (Banque de message) (Message circonstatiel)
23.Ajouter effet sonore
 
---Mode édition---
1.Créer des paire de cases (dans une liste) (Avec un minimum de case à respecter exemple (4x3, soit 10 cases minimum)(Influe sur le nombre de dé)
2.Supprimer une paire de case
3.Choisir les dimensions Nb cases vertical horizontal. Par défaut l'ajout d'une paire incrémente la hauteur
=======
Charles Lapierre 11 097 555

Bernard Cloutier 12 101 995

Samuel Whittom    11 065 303

Fanny Salvail-Bérard 16 018 131

Nathan Painchaud 14 021 471

Jonathan Nadeau  15 089 039

Sara Ait Bouziaren 15 112 860

Marc Dupuis  08 387 432

Frédéric Nadeau 16 100 247

Alexandre Fortin 15 083 160
 
===Description===

Jeu conventionnel de monopoly. Tour par tour. Multijoueur. Planche de jeu. Vider le porte feuille de son adversaire.
Ajout au jeu conventionnel. Planche de jeu modifiable(mode édition mode jeu).

===Fonctionnalités===
 
---Implémentation de base---

1.Planche de jeu par défaut (avant édition)(dans un fichier texte contenant un pack (version du jeu))

2.Afficher la planche de jeu

3.Piger des cartes

4.Afficher les joueurs sur la planche de jeu

5.Afficher le propriétaire de la case

6.Assigner une case à un joueur (suite à l'achat par un joueur de la case)

7.Certaine case activera la distribution d'une carte conséquence

8.Bouger un joueur  (changement de pointeur ou changement d'attribut)

9.Brasser un dé

  9b. Brasser n dés

10.Taxe muicipale 10% de la somme de Brassée deux dés * X$

11.Modifier le compte en banque de chaque joueur

12.Transaction entre deux joueurs (Jonatan arrive sur Local D3-2037 qui appartient à Alex)

13.Additionner argent au porte feuille d'un joueur (fct appelé par la précédente)

14.Éliminer un joueur (Faillite)

15.Déterminer un gagnant et fin du jeu

16.Statistique de jeu

17.Trigger (exemple prener 1000$ en passant GO)

18.Mouvement automatique (aller en prison, Piger une carte qui dit visiter le Manoir de Fortin,...)

19.Lors du tour d'un ordinateur, simuler le temps qui passe (voir chacune des actions sur plusieurs secondes)
(au lieu d'un programme qui compile toute la partie en 1 ou 2 secondes)

20.Ajouter un mode édition pour implanter de nouvelles versions du jeu Monopoly

21.Créer un IA (adversaire(s) virtuel(s))

22.Envoi de messages taquins provenant de l'ordinateur (Banque de message) (Message circonstatiel)

23.Ajouter effet sonore

---Mode édition---

1.Créer des paire de cases (dans une liste) (Avec un minimum de case à respecter exemple (4x3, soit 10 cases minimum)(Influe sur le nombre de dé)

2.Supprimer une paire de case

3.Choisir les dimensions Nb cases vertical horizontal. Par défaut l'ajout d'une paire incrémente la hauteur

>>>>>>> 5cc1b3f45a9fc6ead5d3b890f2c0aa29b74e55d3
                xxxx      Exemple 4x3
                x  x
                xxxx
Ou bien un nombre fixe (40 cases)?
 
<<<<<<< HEAD
3b.ou offrir un choix de dimension possible (limiter l'affichage horizontale car scroller sur l'horizontale est désagréable)
4.Vérification de la validité des dimension (2h+2(v-2))==nb total)
5.Éditer une case
6.Sauvegarde du pack créé
7. Création de carte pigé (Avec format fourni)
                Exemple de format: visité tel case, payé une amende car un moldu vous a vu utilisé un sort,...)
8.Choisir la base d'argent exemple(total sur 100 000 ou en 1000)
                le prix d'achat d'une case, le prix de passage sur une case serait déterminer par une fraction de ce prix
9.Affichage de l'argent($ ou euro ou piece d'or)
                Exemple harry potter :
                L'argent des sorciers est composé de trois types de pièces : les Gallions d'or, les Mornilles d'argent et les Noises de bronze.
                1 Gallion = 17 Mornilles et 1 Mornille = 29 Noises
                Donc 1 Gallion = 493 Noises
 
---Mode de jeu---
1.Choisir un pack de jeu (Monopoly Canada, Monopoly Harry Potter,...)
2.Choisir son pion (liste de caractere dispo)
3.Choisir Nombre de joueur (Minimum 2)(Maximum 4)(Inclure 1,2,3 ordi)(Faire une partie avec 4 ordi seulement)
4.Début tour d'un joueur (Affichage du porte feuille)(Brassée  deux dé) (Active mouvement(condition si prison))
5.Choisir d'acheter ou non une propriété
6.Proposer échange
7.Utiliser une carte en sa possession (sortie de prison, Certificat cadeau de laisser passé pour la propriété de son choix,...)
8.Hypothèquer une propriété
9.Achat de maison - d'hotel
 
Environnement proposé : Console c# interface WPF
=======
  3b.ou offrir un choix de dimension possible (limiter l'affichage horizontale car scroller sur l'horizontale est désagréable)

4.Vérification de la validité des dimension (2h+2(v-2))==nb total)

5.Éditer une case

6.Sauvegarde du pack créé

7. Création de carte pigé (Avec format fourni)
                Exemple de format: visité tel case, payé une amende car un moldu vous a vu utilisé un sort,...)

8.Choisir la base d'argent exemple(total sur 100 000 ou en 1000)
                le prix d'achat d'une case, le prix de passage sur une case serait déterminer par une fraction de ce prix

9.Affichage de l'argent($ ou euro ou piece d'or)
```
Exemple harry potter :
L'argent des sorciers est composé de trois types de pièces : les Gallions d'or, les Mornilles d'argent et les  Noises de bronze.
1 Gallion = 17 Mornilles et 1 Mornille = 29 Noises
Donc 1 Gallion = 493 Noises
```
---Mode de jeu---

1.Choisir un pack de jeu (Monopoly Canada, Monopoly Harry Potter,...)

2.Choisir son pion (liste de caractere dispo)

3.Choisir Nombre de joueur (Minimum 2)(Maximum 4)(Inclure 1,2,3 ordi)(Faire une partie avec 4 ordi seulement)

4.Début tour d'un joueur (Affichage du porte feuille)(Brassée  deux dé) (Active mouvement(condition si prison))

5.Choisir d'acheter ou non une propriété

6.Proposer échange

7.Utiliser une carte en sa possession (sortie de prison, Certificat cadeau de laisser passé pour la propriété de son choix,...)

8.Hypothèquer une propriété

9.Achat de maison - d'hotel
 
===Environnement proposé===

Console c# interface WPF
>>>>>>> 5cc1b3f45a9fc6ead5d3b890f2c0aa29b74e55d3
