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

