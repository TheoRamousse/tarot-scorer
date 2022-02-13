![bandeau](/Documentation/doc_images/bandeau.png "Tarot Scoreur")  

# MyTarotScorer

A sample project for my dear students. This project aims at developping a scorer for the famous cards game "Tarot".

# Overview

*Tarot Scoreur* est une application mobile qui vous permettra de compter les points et les scores de vos parties de tarot entre amis, entre ennemis et de vous mesurer aux joueurs de votre niveau ou, pour les plus téméraires, *à moi* :monkey:. Vous utilisiez des feuilles volantes que vous perdiez parfois ? Un tableau effacé par mégarde ? Vous voulez jouer mais n’avez pas de crayon ou de feuille ? Cette application est faite pour vous.  
Vous pouvez créer de nouveaux joueurs ou utiliser les joueurs créés précédemment. Vous pouvez ajouter les scores de vos parties à 3, 4 ou 5 joueurs et regrouper celles-ci par session, ou lancer des calculs de statistiques sur des périodes données pour découvrir qui est le meilleur joueur de tous les temps après moi. 

# Table of contents
[[_TOC_]]

## Requirements

Il faut **visualstudio(minimum VS2019*)* mais la version peut être plus récente
Il faut **git**

## Installation

Il faut **visualstudio** au minimum VS2019 : https://visualstudio.microsoft.com/fr/

Il vous faut **git**, pour récuperer le projet:

-créer un dossier personnel faites clic droit dessus puis cliquez sur "git bash here"
<br/>
![Image text](/Documentation/doc_images/git.png "git bash here") 

Puis écrire la comande suivante:
```
git clone https://gitlab.iut-clermont.uca.fr/mabouvard2/tarot-scorer.git
```

Le projet est maintenant dans le dossier que vous venez de créer.

**Cette partie est inutile si le projet GraphQL contient déjà la base de données en .db :**
<br/>
<br/>
Ensuite il faut que vous installiez le nuget :  *Microsoft.EntityFrameworkCore.Sqlite* (en **version 3** la plus avancée sinon pas compatible) 
<br/>
![Image text](/Documentation/doc_images/nugetSql.png "Microsoft.EntityFrameworkCore.Sqlite")

Et le nuget : Microsoft.EntityFrameworkCore.Design
<br/>
![Image text](/Documentation/doc_images/nugetDesign.png "Microsoft.EntityFrameworkCore.Design")



Après cela rendez vous sur la console de gestionnaire de packages.
<br/>
![Image text](/Documentation/doc_images/gestionnaireConsole.png "Accès à la console de gestionnaire de package")

Une fois dedans rendez vous sur le projet TarotDB avec cette commande:
```
cd .\TarotDB
```
pour vérifier que vous êtes dans le bon dossier vous pouvez taper la commande:
```
ls
```

Vous êtes censé obtenir ceci:
<br/>
![Image text](/Documentation/doc_images/consoleNuget.png "Déplacement dans la bonne solution")


Une fois dans cette solution taper la commande :
```
dotnet ef database update -- connection "Data Source=../GraphQLApi/Tarot.db" --startup-project ../GraphQLApi/GraphQLApi.csproj
```

# What we have now?

Actuellement, nous avons réalisé les tâches suivantes pour la réalisation du projet : 

- API Rest

- API Gateway

- API GraphQL : Opérations CRUD pour les joueurs et les parties. CREATE, UPDATE et DELETE retournent un booléen qui indique si la requête a été effectuée en base


# Usage

### Pour l'API REST
***
Pour l'API REST nous avons la possibilité de requêter 3 DTO, respectivement game, player et session.
Pour chaque DTO nous avons accès à la pagination, demandant le nombre d'éléments retournés (count) à partir d'un index donné:
<br/>
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

De plus, nous pouvons réaliser l'ajout d'un élément en base:
<br/>
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

Mais encore compter le nombre d'éléments en base:
<br/>
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

Nous pouvons aussi éditer un DTO en fonction de son id :
<br/>
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

Et pour finir nous pouvons supprimer un DTO en fonction de son id:
<br/>
![Image text](/Documentation/doc_images/GraphQl/requete1.png "Requête pour les parties")

Comme vu ci dessus notre API REST nous permet de réaliser les opération CRUD (create / read / update / delete).

### Pour l'API GraphQL
***

Les requêtes GraphQL ont la particularité d'être **modulables** en fonction des informations que l'utilisateur souhaite récupérer. Sur l'exemple ci-dessous, la requête permet la **pagination** (nombre d'éléments par page / la page souhaitée). Ici, nous souhaitons afficher 15 éléments (numberOfElementsPerPage:**15**) et nous souhaitons récupérer la **première page** (pageNumber:**0**). Ansi, l'utilisateur pourra choisir à quelle page il veut débuter. Voici l’exemple permettant de liste les parties créées : 
<br/>
![Image text](/Documentation/doc_images/GraphQL/requeteGame.png "Requête pour les parties")
<br/>

Et voici le résultat obtenu :
<br/>
![Image text](/Documentation/doc_images/GraphQL/reponseRequeteGame.png "Réponse requête pour les parties")
<br/>

Seuls quelques parties ont été affichées car notre base de données n’en contenait pas plus.
Dans cette requête, j’ai affiché tous les champs possibles pour une partie mais libre à vous de choisir les champs que vous souhaitez récupérer ([**Voir les champs**](#champs)).

<a name="champs"></a>


Voici les champs disponibles:

-**Pour les joueurs:**
<br/>
![Image text](/Documentation/doc_images/GraphQl/champsPlayer.png "Champs pour les joueurs")
<br/>
Les informations disponibles pour les joueurs sont: son identifiant, son prénom, son nom et son pseudo.

-**Pour les parties:**
<br/>
![Image text](/Documentation/doc_images/GraphQl/champsGame.png "Champs pour les parties")
<br/>
Les informations disponibles pour les parties sont: son identifiant , la date de la partie, le TakerPoints , l'excuse , le vingt et un et la liste de joueurs avec leur bidding correspondant.

Pour cette liste nous avons de même la possibilité de choisir les champs voulus:
<br/>
![Image text](/Documentation/doc_images/GraphQl/champsPlayer&Bidding.png "Champs pour les joueurs et leur Bidding")
<br/>

Les informations disponibles ici sont un joueur(vu ci-dessus) et le bidding.

Grâce à ces différents champs et en suivant l'exemple, les utilisateurs pourrons requêter notre API GraphQL en ne prenant que les champs dont ils ont besoin.

Voici les autres requêtes correspondant aux opérations CRUD standards pour les parties et pour les joueurs (nous n'avons pas affiché les réponses aux requêtes DELETE, UPDATE et CREATE car elles retournent simplement un booléen qui indique si la requête a fonctionnée ou non): 

- Affichage des joueurs avec pagination
<br/>
![Image text](/Documentation/doc_images/GraphQL/requeteUser.png "Champs pour les parties")
<br/>
<br/>
![Image text](/Documentation/doc_images/GraphQL/reponseRequeteUser.png "Champs pour les parties")
<br/>
- Ajout d'un joueur
<br/>
![Image text](/Documentation/doc_images/GraphQL/addPlayer.png "Champs pour les parties")
<br/>
- Suppression d'un joueur (tous les champs doivent obligatoirement être renseignés mais seul l'ID est utilisé donc vous pouvez mettre ce que vous voulez dans les autres champs)
<br/>
![Image text](/Documentation/doc_images/GraphQL/deletePlayer.png "Champs pour les parties")
<br/>
- Mise à jour d'un joueur
<br/>
![Image text](/Documentation/doc_images/GraphQL/updatePlayer.png "Champs pour les parties")
<br/>
- Ajout d'une partie (tous les champs doivent obligatoirement être renseignés pour le joueur mais seul l'ID est utilisé donc vous pouvez mettre ce que vous voulez dans les autres champs)
<br/>
![Image text](/Documentation/doc_images/GraphQL/ajoutGame.png "Champs pour les parties")
<br/>
- Suppression d'une partie (tous les champs doivent obligatoirement être renseignés mais seul l'ID est utilisé donc vous pouvez mettre ce que vous voulez dans les autres champs)
<br/>
![Image text](/Documentation/doc_images/GraphQL/suppressionGame.png "Champs pour les parties")
<br/>
- Mise à jour d'une partie
<br/>
![Image text](/Documentation/doc_images/GraphQL/updateGame.png "Champs pour les parties")
<br/>

# Running the tests

## Global tests
***
Dans l'état actuel de notre projet, nous avons seulement des classes de test et une petite application console qui est simplement là pour montrer que notre programme compile. Pour pouvoir lancer les tests, il faut simplement ouvrir le projet dans VS2019, il faut ensuite faire un clique droit (soit sur la solution générale soit sur un test_UT) et séléctionner "Exécuter les tests". 
![Image text](/Documentation/doc_images/tests/generation.png "Exécution des tests")  
Lorsque l'explorateur de tests est ouvert vous pouvez alors séléctionner vous même le test à éxécuter.
Si le test retourne le résultat attendu alors il affiche un cercle vert sinon il affiche un cercle rouge.
<br/>
![Image text](/Documentation/doc_images/tests/selection.png "Selection de tests")  

***
## GraphQL tests
***
<a name="GraphQLRequest"></a>
Pour effectuer des requêtes de tests il faut tout d'abord **lancer la solution GraphQLAPI**.

Ensuite **il faut vous rendre sur le lien** : http://localhost:5003/ui/playground .

**Taper vos requêtes à gauche** (comme vu ci dessus avec l'exemple de requête) et cliquez ensuite sur le bouton **"play"**, si votre requête est bien écrite alors vous verrez une **réponse cohérente à droite du site**.

Nous n'avons pas eu le temps d'implémenter les tests unitaires pour l'API GraphQL car l'implémentation des opérations CRUD ont été difficiles.

# Why we choose these API?

### GraphQL :
Lors des différentes requêtes que nos utilisateur vont effectuer, nous n'avons pas la certitude des données que ces derniers veulent récupérer.
En effet **GraphQL** est une **API** permettant de pallier ce problème grâce aux multiples avantages qu'il nous offre :

- **On obtient toujours le résultat qu’on attend**, autrement dit il aide à prévoir les données reçues ainsi que la structure de ces dernières. Cela simplifie le traitement des données que le serveur renvoie et permet de les faire consommer par l’application à laquelle on les envoies.

- **Le serveur sait exactement quelles données on veut voir**, en ayant une structure claire des objets et des champs possibles qui peuvent être interrogés, grace à cela le serveur saura toujours ce qu’il est censé renvoyer. S’il y a une erreur dans la requête, elle ne sera pas validée et l’utilisateur sera averti que sa requête est incorrecte.

Comme spécifié ci-dessus **GraphQL** nous permet de faire des **requêtes très spécialisées** (seulement ce que l'utilisateur veut savoir), ce qui engendre un **volume de transfert de données plus faible** qui signifie également une **connexion plus rapide** et de **meilleurs temps de chargement** pour les utilisateurs.
C'est bénéfique pour l'utilisateur et pour l'éfficacité de notre application d'avoir de meilleures performances.

 
### REST :

Pour rappel REST Representational State Transfer (ou transfert d’état de représentation, en français), constitue un ensemble de normes, ou de lignes directrices architecturales qui structurent la façon de communiquer les données entre votre application et le reste du monde, ou entre différents composants de votre application. L'API  REST se base sur le protocole HTTP pour transférer les informations.



# Features

Tarot Scoreur permet maintenant la gestion de 2 API (une API REST avec gateway et une API GraphQL).
Sur ces API sont disponibles les opérations CRUD (create, read, update, delete).



# Nugets

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
•	**GraphQLApi** : *AutoMapper(11.0.0), AutoMapper.Extensions.Microsoft.DependencyInjection(11.0.0), coverlet.collector(3.0.2), GraphQL(4.7.1), GraphQL.Server.Ui.Playground(5.2.0), HotChocolate.AspNetCore(12.4.1), Microsoft.EntityFrameworkCore(3.1.22), Microsoft.EntityFrameworkCore.Design(3.1.22), Microsoft.EntityFrameworkCore.Sqlite(3.1.22), Microsoft.NET.Test.Sdk(16.9.4), NETStandard.Library(2.0.3), Swashbuckle.AspNetCore(5.6.3), xunit(2.4.1), xunit.runner.visualstudio(2.4.3)*
***


•	**AutoMapper**: c'est une petite bibliothèque simple conçue pour résoudre un problème d'une complexité trompeuse, il permet de se débarrasser du code qui mappait un objet à un autre.
***
•	**GraphQL**: c'est une bibliothèque qui permet de réaliser les requêtes personnalisées.
***
•	**GraphQL.Server.Ui.Playground**: c'est ce qui permet de réaliser le lien entre notre application et le site web préciser dans les tests GraphQL.
***
•	**HotChocolate.AspNetCore**:c'est une plate-forme GraphQL qui peut nous aider à créer une couche GraphQL sur votre infrastructure existante
***

# Client Blazor

## Contexte

Le nom du projet Blazor est « APIGateway », nous avons essayé de le changer mais cela faisait crash toute l’application. Nous avons donc laissé ce nom par défaut.

Le but de ce client Razor est de manipuler les informations sur les différents joueurs de Tarot avec toutes les opérations CRUD classiques : 
-	**Ajout d’un joueur**
-	**Supppression d’un joueur**
-	**Modification du nom/prénom/pseudo du joueur**
-	**Listing de tous les joueurs de l’application**

Lorsque l’on arrive sur la page principale de l’application, un tableau apparaît avec tous les joueurs de l’application. Ce tableau à la particularité d’être entièrement dynamique et paramétrable ce qui signifie que vous pouvez le récupérer en l’état et l’utiliser dans une autre application (nous autorisons le vol du composant à condition d’être cité dans la page 😉 ). Son fonctionnement est détaillé par la suite.

Sur ce composant tableau, plusieurs opérations sont possibles : 
-	**La suppression d’un joueur** lors d’un appuie sur le bouton « Supprimer »
-	**La modification d’un joueur** lors d’un appuie sur le bouton « Editer », ce bouton vous redirigera sur une autre page de formulaire et vous pourrez modifier les informations du joueur
-	**La consultation des parties d’un joueur** au cours du temps détaillé par la suite.
-	**L’ajout d’un nouveau joueur** avec le bouton situé en haut à droite du tableau, vous serez alors redirigé sur la même page que pour modifier un joueur

**Ajout d'un joueur :**
<br/>
![Image text](/Documentation/doc_images/Blazor/ajout.png "Ajout d'un joueur")  
<br/>
**Modification d'un joueur :**
<br/>
![Image text](/Documentation/doc_images/Blazor/edit.png "Modification d'un joueur")  
<br/>

Comme dit précédemment, l’utilisateur du site peut consulter les parties des différentes joueurs. La page permettant de réaliser cette action contient un graphe qui affiche pour chaque partie le score du preneur. Si vous cliquez sur une partie, vous aurez les informations associées à celle-ci qui apparaîtront en-dessous. Pour réaliser ce graphique, nous avons utilisé **HighChart** qui permet de créer un graphique facilement grâce à du code javascript. La conception du composant est détaillée plus bas.

## Composant tableau "DynamicTable"

Ce composant a été fait-maison sans l’aide d’un tuto ou autre. Pour le réaliser, nous avons créé un composant Blazor en HTML, CSS et Bootstrap. Le code-behind du composant a été réalisé en C# Blazor.

Le composants attends de nombreux paramètres lors de sa création : 
-	**IsViewable (Booléen) :** Défini l’affichage ou non du bouton « Voir »
-	**IsEditable (Booléen) :** Défini l’affichage ou non du bouton « Editer »
-	**IsDeletable (Booléen) :** Défini l’affichage ou non du bouton « Supprimer »
-	**OnView (String) :** Lors du clique sur le bouton « Voir », l’URL de redirection est baseURL/OnView/idDeL’élément
-	**OnEdit (String) :** Lors du clique sur le bouton « Editer », l’URL de redirection est baseURL/OnEdit/idDeL’élément
-	**Delete (Func<int, Task>) :** Méthode passée comme lambda en paramètre qui est appelée lors de l’appuie sur le bouton « Supprimer »
-	**GetTotalNumberOfData (Func<Task<int>>) :** Méthode qui est utilisé par la pagination, elle retourne un Integer correspondant au nombre d’éléments à afficher
-	**NumberElementsPerPage (Integer) :** Correspond au nombre d’éléments à afficher par page
-	**FetchDataWithNumberOfElementsAsync (Func<int, int, Task<List<Data>>>) :** Méthode retournant la donnée consommée par notre composant. Attends 2 Integers en paramètre qui correspondent à la page souhaitée et au nombre d’éléments par page. Elle retourne une liste de Data.
<br/>
![Image text](/Documentation/doc_images/Blazor/main.png "Tableau des joueurs")  
<br/>
Pour réaliser ce composant, vous aurez besoin de la classe Data associée qui n’est ni plus ni moins qu’un alias d’un **Dictionary<string, object>**
Les deux seules règles à respecter lors de l’ajout d’une Data est que la **Data** doit contenir une clé **Id**.
Si vous souhaitez passer un champ qui ne doit pas être affiché dans le tableau, ce champ portera la clé « **_ignoreThisPartOfDataOrConsequences** »

## Composant Graphique "CustomChartStatistic"

Ce composant est également légèrement paramétrable mais beaucoup moins que le tableau, il ne pourra pas être repris dans un autre projet sans subir des modifications.

Le composant attend les paramètres suivants :
-	**Title (String) :** Titre du graphique
-	**Subtitle (String) :** Sous-titre du graphique
-	**YAxisName (String :** Label affiché sur l’axe des Y
-	**XAxisName (String :** Label affiché sur l’axe des X
-	**Data (Tableau de GameEntity) :** Données consommées par le composant

Le graphique affiche les points du preneur pour chaque partie passée en paramètre, si vous cliquez sur un point du graphique, une card s’affiche en dessous avec les éléments de la partie sélectionnée : 
-	PetitResult
-	Poignée
-	Chelem
-	TakerPoints
-	NbPlayers
-	Id de la partie
-	Date de la partie
-	TwentyOne
-	Excuse
Si vous êtes un amateur de Tarot, ces éléments doivent vous parler. Personnellement, nous n’y comprenons absolument rien !
<br/>
![Image text](/Documentation/doc_images/Blazor/see.png "Graphique plié")  
<br/>
![Image text](/Documentation/doc_images/Blazor/see2.png "Graphique déplié") 
<br/>


## Etat d’avancé du projet

Pour le client Blazor, nous avons implémenté tout ce que nous souhaitions.
Malheureusement, **nous avons pris beaucoup de retard sur l’API REST**, nous n’avons donc pas de liaison avec celle-ci et nous utilisons un STUB à la place, ce **STUB** récupère les données au format **JSON**.
Nous avons tout de même pensé l’architecture afin d’implémenter facilement la liaison avec l’API. Pour cela, il suffira de créer une nouvelle classe qui implémente l’interface « **IDataService** » et de changer StubService par cette nouvelle classe dans le « **Program.cs** ».

Voici le schéma explicatif de la persistance : 
<br/>
![Image text](/Documentation/doc_images/Blazor/schema.png "Graphique déplié") 
<br/>




# Participation
Pour le projet le groupe s'est séparé en 2 sous-groupes, Théo et Mathieu sur le coté GraphQL et Raphaël et Maël sur l'API REST et gateway.
Les commits peuvent donc être différents sur les membres du groupe car le travail s'est souvent effectué sur un PC après la mise en commun (une seule personne commit le travail de 2).
La documentation à été répartie dans l'ensemble du groupe de manière plutôt égale.


# Authors
Maël Bouvard
Théo Ramousse 
Mathieu Albiero
Raphaël Hacques

# Acknowledgments
- Nicolas Raymond
- Thomas Bellembois

# documentation à faire
Théo:
Description du fonctionnement la solution globale
Dire ce qu'on a codé sur la partie serveur 
Quelles sont les grandes étapes pour mettre en place cela (avec screen)

Mathieu: 
expliquer la structure : pourquoi comme ça
comment est il découper
schéma

Raphaël:
Api gateway
que va t'elle faire
comment route elle les elements

Mael:
faire séparation du readme en plusieurs documents
création wiki
dire ou sont les autres documents dans readme
