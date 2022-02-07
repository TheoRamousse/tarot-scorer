![bandeau](/Documentation/doc_images/bandeau.png "Tarot Scoreur")  

# MyTarotScorer

A sample project for my dear students. This project aims at developping a scorer for the famous cards game "Tarot".

# Overview

*Tarot Scoreur* est une application mobile qui vous permettra de compter les points et les scores de vos parties de tarot entre amis, entre ennemis et de vous mesurer aux joueurs de votre niveau ou, pour les plus téméraires, *à moi* :monkey:. Vous utilisiez des feuilles volantes que vous perdiez parfois ? Un tableau effacé par mégarde ? Vous voulez jouer mais n’avez pas de crayon ou de feuille ? Cette application est faite pour vous.  
Vous pouvez créer de nouveaux joueurs ou utiliser les joueurs créés précédemment. Vous pouvez ajouter les scores de vos parties à 3, 4 ou 5 joueurs et regrouper celles-ci par session, ou lancer des calculs de statistiques sur des périodes données pour découvrir qui est le meilleur joueur de tous les temps après moi. 

# Table of contents
[[_TOC_]]

# Documentation

# Getting started

## Requirements

Il faut **visualstudio(minimum VS2019*)* mais la version peut être plus récente
Il faut **git**

## Installation

Il faut **visualstudio** au minimum VS2019 : https://visualstudio.microsoft.com/fr/

Il vous faut **git**, pour récuperer le projet:

-créer un dossier personnel faites clic droit dessus puis cliquez sur "git bash here"
![Image text](/Documentation/doc_images/git.png "git bash here") 

Puis écrire la comande suivante:
```
git clone https://gitlab.iut-clermont.uca.fr/mabouvard2/tarot-scorer.git
```

Le projet est maintenant das le dossier que vous venez de créer.




Voici les **packages nugets** installés et leur version pour chaque solution du projet :

•	**Model** : *NETStandard.Library(2.0.3)*
***
•	**RestApi** : *Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer(5.0.0), NLog(4.7.13), NLog.Web.AspNetCore(4.14.0), Swashbuckle.AspNetCore(5.6.3)*
***
•	**StubLib** : *NETStandard.Library(2.0.3)*
***
•	**TarotDB** : *Microsoft.EntityFrameworkCore(3.1.22), Microsoft.EntityFrameworkCore.Sqlite(3.1.22), NETStandard.Library(2.0.3)*
***
•	**TarotDB2Model**: *NETStandard.Library(2.0.3)*
***
•	**Tests\DataManager_UT** : *coverlet.collector(3.0.2), xunit(2.4.1), xunit.runner.visualstudio(2.4.3), Microsoft.EntityFrameworkCore.Design(3.1.22), Microsoft.NET.Test.Sdk(16.9.4)*
***
•	**Tests\ManagerDBTests** : *Microsoft.EntityFrameworkCore.Design(3.1.22)*
***
•	**Tests\ManagerStubTests** :
***
•	**Tests\Model_UT** : *coverlet.collector(3.0.2), xunit(2.4.1), xunit.runner.visualstudio(2.4.3), Microsoft.NET.Test.Sdk(16.9.4)*
***
•	**Tests\TarotDB_Tests** : *Microsoft.EntityFrameworkCore.Design(3.1.22)*
***
•	**Tests\TarotDB_UT** : *coverlet.collector(3.0.2), xunit(2.4.1), xunit.runner.visualstudio(2.4.3), Microsoft.EntityFrameworkCore.Design(3.1.22), Microsoft.NET.Test.Sdk(16.9.4)*
***

## Sample app

# What we have now?

Actuellement, nous avons réalisé les tâches suivantes pour la réalisation du projet : 

- API Rest

- API Gateway

- API GraphQL


# Usage

### Pour l'API REST
***
### Pour l'API GraphQL
***
Nous avons la possibilité d'effectuer des requêtes personnalisées.
Voici ci dessous un **exemple de requête** que nous pouvons effectuer : 
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

Les requêtes vont être **modulables** en fonctions des envies de nos différents utilisateurs, sur cette exemple la **pagination** (nombre d'éléments par page) est fixé sur 1 (numberOfElementsPerPage:**1**) et nous débutons au **premier élément** de nos données (pageNumber:**0**) mais l'utilisateur pourra choisir à quelle page il veut débuter.

De plus les champs présent ici dans la requête ne sont pas fixes, en effet on peut les enlever ou bien en ajouter dans les champs disponibles ([**Voir les champs**](#champs)).

Voici la **réponse obtenue avec la requête ci dessus** :
![Image text](/Documentation/doc_images/GraphQl/reponse1.png "Reponse pour les parties")

<a name="champs"></a>
Comme préciser ci dessus, l'utilisateur va pouvoir séléctionner les champs dont il a besoin.
Voici les champs disponibles:

-**Pour les joueurs:**
![Image text](/Documentation/doc_images/GraphQl/champsPlayer.png "Champs pour les joueurs")

Les informations disponibles sur les joueurs sont: son identifiant, son prénom, son nom et son surnom.

-**Pour les parties:**
![Image text](/Documentation/doc_images/GraphQl/champsGame.png "Champs pour les parties")

Les informations disponibles sur les parties sont: son identifiant , la date de la partie, le TakerPoints , l'excuse , le vingt et un et la liste de joueurs avec leur bidding correspondant.
Pour cette liste nous avons de même la possibilité de choisir les champs voulus:

![Image text](/Documentation/doc_images/GraphQl/champsPlayer&Bidding.png "Champs pour les joueurs et leur Bidding")

Les informations disponibles ici sont un joueur(vu ci dessus) et le bidding.


Grâce à ces différents champs et en suivant l'exemple les utilisateurs pourrons requêter notre API GraphQL en ne prenant que les champs dont ils ont besoin.

[**Des tests à faire ?**](#GraphQLRequest)


# Running the tests

## Global tests
***
Dans l'état actuel de notre projet, nous avons seulement des classes de test et une petite application console qui est simplement là pour montrer que notre programme compile. Pour pouvoir lancer les tests, il faut simplement ouvrir le projet dans VS2019, il faut ensuite faire un clique droit (soit sur la solution générale soit sur un test_UT) et séléctionner "Exécuter les tests". 
![Image text](/Documentation/doc_images/tests/generation.png "Exécution des tests")  
Lorsque l'explorateur de tests est ouvert vous pouvez alors séléctionner vous même le test à éxécuter.
Si le test retourne le résultat attendu alors il affiche un cercle vert sinon il affiche un cercle rouge.

![Image text](/Documentation/doc_images/tests/selection.png "Selection de tests")  

***
## GraphQL tests
***
<a name="GraphQLRequest"></a>
Pour effectuer des requêtes de tests il faut tout d'abord **lancer la solution GraphQLAPI**.

Ensuite **il faut vous rendre sur le lien** : http://localhost:5003/ui/playground .

**Taper vos requêtes à gauche** (comme vu ci dessus avec l'exemple de requête) et cliquez ensuite sur le bouton **"play"**, si votre requête est bien écrite alors vous verrez une **réponse cohérente à droite du site**.

# Roadmap

# Why we choose GraphQL API?

Lors des différentes requêtes que nos utilisateur vont effectuer, nous n'avons pas la certitude des données que ces derniers veulent récupérer.
En effet **GraphQL** est une **API** permettant de pallier ce problème grace aux multiples avantages qu'il nous offre :

- **On obtient toujours le résultat qu’on attend**, autrement dit il aide à prévoir les données reçues ainsi que la structure de ces dernières. Cela simplifie le traitement des données que le serveur renvoie et permet de les faire consommer par l’application à laquelle on les envoies.

- **Le serveur sait exactement quelles données on veut voir**, en ayant une structure claire des objets et des champs possibles qui peuvent être interrogés, grace à cela le serveur saura toujours ce qu’il est censé renvoyer. S’il y a une erreur dans la requête, elle ne sera pas validée et l’utilisateur sera invité à envoyer un message d’erreur descriptif qui permettra un dépannage rapide.


Comme spécifié ci-dessus **GraphQL** nous permet de faire des **requêtes très spécialisées** (seulement ce que l'utilisateur veut savoir), ce qui engendre un **volume de transfert de données plus faible** signifie également une **connexion plus rapide** et de **meilleurs temps de chargement** pour les utilisateurs.
C'est bénéfique pour l'utilisateur et pour l'éfficacité de notre application d'avoir de meilleures performances.
 

# Known issues and limitations

# Built with

# Authors
Maël Bouvard
Théo Ramousse 
Mathieu Albiero
Raphaël Hacques

# Acknowledgments
- Nicolas Raymond
- Thomas Bellembois
